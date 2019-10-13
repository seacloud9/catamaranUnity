using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Catamaran;
using Catamaran.Utils;
using Catamaran.ThirdParty;
namespace Catamaran.UI
{
	

	public class uislider : MonoBehaviour
	{
		public Sprite[] slides = new Sprite[1];
		public RectTransform uiRect;
		public float changeTime = 5.0f;
		public Image _image;
		private int currentSlide = 0;
		private float timeSinceLast = 1.0f;
		public float speed = 0.04f;
		private Vector3 startPosition = Vector3.zero;
		private Vector3 endPosition = Vector3.zero;
		private string _direction = "left";
		public float _positionBeforeTweenOffset = 2f;
		public float scaleAspectOffset = 0.8f;
		private EventTrigger clickEventTrigger;
		public int SlideTrigger = 3;
		private EventTrigger.Entry _triggerEntry;
		public Swipe _swipe;
		float touchDuration;
		Touch touch;

		void Awake ()
		{
			Application.targetFrameRate = 140;
		}

		void Start ()
		{
			_positionBeforeTweenOffset = (Screen.width * _positionBeforeTweenOffset);
			uiAspectFix ();
			EventTrigger clickEventTrigger = GetComponentInParent<EventTrigger> ();
			_triggerEntry = new EventTrigger.Entry ();
			_triggerEntry.eventID = EventTriggerType.PointerDown;
			_triggerEntry.callback.AddListener (( data) => {
				OnPointerDownDelegate ((PointerEventData)data);
			});
			clickEventTrigger.triggers.Add (_triggerEntry);
			_swipe.swipeRight = moveSlide;
			_swipe.swipeLeft = moveSlide;
		}

		public void OnPointerDownDelegate (PointerEventData data)
		{
			if (currentSlide == SlideTrigger) {
				Debug.Log ("OnPointerDownDelegate called.");
					OnLevelChange ();
			}

		}



		void flipSideBeforeTween ()
		{
			//todo write a check for when it is tweening
			if (uiRect.anchoredPosition3D.x != 0 && (uiRect.anchoredPosition3D.x == _positionBeforeTweenOffset || uiRect.anchoredPosition3D.x == -_positionBeforeTweenOffset)) {
				SpriteRenderer _spRender = GetComponent<SpriteRenderer> ();
				_spRender.sprite = slides [currentSlide];
				_spRender.enabled = false;
				if (_direction == "left") {
					uiRect.anchoredPosition3D = new Vector3 (-_positionBeforeTweenOffset, 0f, 0f);
				} else {
					uiRect.anchoredPosition3D = new Vector3 (_positionBeforeTweenOffset, 0f, 0f);
				}
				_spRender.enabled = true;
			}
		}

		void uiAspectFix ()
		{
			Sprite currentSprite = slides [currentSlide];
			int screenAspectRatio = (Screen.width / Screen.height);
			int textureAspectRatio = (currentSprite.texture.width / currentSprite.texture.height);
			float scaledWidth = 0.0f;
			float scaledHeight = 0.0f;
			if (textureAspectRatio <= screenAspectRatio) {
				try {
					scaledWidth = ((Screen.height * scaleAspectOffset) * textureAspectRatio);
					float diff = (float)((float)scaledWidth / (float)currentSprite.texture.width);
					scaledHeight = (int)(diff * currentSprite.texture.height);
				} catch (System.Exception e) {
					Debug.Log (e);
				}
			} else {
				scaledHeight = ((Screen.width * scaleAspectOffset) / textureAspectRatio);
				float diff = (float)((float)scaledHeight / (float)currentSprite.texture.height);
				scaledWidth = (int)(diff * currentSprite.texture.width);
			}
			uiRect.sizeDelta = new Vector2 (scaledWidth, scaledHeight);
		}

		void updateSlideandReset ()
		{
			if (_direction == "left") {
				currentSlide++;
			} else {
				currentSlide--;
			}
			currentSliderReset ();
			_image.sprite = slides [currentSlide];
			uiAspectFix ();
			flipSideBeforeTween ();
			doTween (new Vector3 (0f, 0f, 0f), false);
		}

		void currentSliderReset ()
		{
			if (currentSlide == slides.Length) {
				currentSlide = 0;
			}
			if (currentSlide == -1) {
				currentSlide = slides.Length - 1;
			}
		}

		void doTween (Vector3 _positionToTweenTo, bool _reset = true)
		{
			timeSinceLast = 0.0f;
			if (_reset) {
				Catamaran.ThirdParty.LeanTween.move (uiRect, _positionToTweenTo, speed).setOnComplete (updateSlideandReset).setEase (LeanTweenType.easeInOutSine).setDelay (0.01f);
			} else {
				Catamaran.ThirdParty.LeanTween.move (uiRect, _positionToTweenTo, speed).setEase (LeanTweenType.easeInOutSine).setDelay (0.01f);
			}

		}

		public void OnLevelChange ()
		{

			SceneManager.LoadScene ("lvl0");
		}

		public void moveSlide (string __direction)
		{
			flipSideBeforeTween ();
			if (__direction == "left") {
				_direction = __direction; 
				doTween (new Vector3 (_positionBeforeTweenOffset, 0f, 0f));
			} else {
				_direction = __direction; 
				doTween (new Vector3 (-_positionBeforeTweenOffset, 0f, 0f));
			}
		}

		void Update ()
		{
			if (currentSlide == SlideTrigger) {
				if (Input.touchCount > 0) {
					OnLevelChange ();
				}
			}

			if (timeSinceLast > changeTime) {
				currentSliderReset ();
				if (_direction == "left") {
					doTween (new Vector3 (_positionBeforeTweenOffset, 0f, 0f));
				} else {
					doTween (new Vector3 (-_positionBeforeTweenOffset, 0f, 0f));
				}
			}
			timeSinceLast += Time.deltaTime;
			if (currentSlide == SlideTrigger) {
				if (Input.touchCount > 0) { //if there is any touch
					touchDuration += Time.deltaTime;
					touch = Input.GetTouch (0);
					if (touch.phase == TouchPhase.Ended && touchDuration < 0.2f) //making sure it only check the touch once && it was a short touch/tap and not a dragging.
					StartCoroutine ("singleOrDouble");
				} else {
					touchDuration = 0.0f;
				}
			}
			
		
		}

		IEnumerator singleOrDouble ()
		{
			yield return new WaitForSeconds (0.3f);
			if (touch.tapCount == 1) {
			} else if (touch.tapCount == 2) {
				StopCoroutine ("singleOrDouble");
				OnLevelChange ();
			}
		}
		

	}
}
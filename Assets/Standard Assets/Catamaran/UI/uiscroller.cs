using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Catamaran.UI
{
	
	public class uiscroller : MonoBehaviour
	{
	
		public GameObject itemPrefab;
		public GameObject itemPrefabContainer;
		public int rowCount = 0;
		public int defaultImgHeight = 165;




		// Use this for initialization
		void Start ()
		{
	
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void addSpritesToScroller (Texture2D _image)
		{
			rowCount++;
			RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform> ();
			Image _imageContainer = itemPrefab.GetComponent<Image> ();
			RectTransform containerRectTransform = gameObject.GetComponent<RectTransform> ();

			//calculate the width and height of each child item.
			float width = containerRectTransform.rect.width;
			///float ratio = width / rowRectTransform.rect.width;
			float height = defaultImgHeight;

			//adjust the height of the container so that it will just barely fit all its children
			float scrollHeight = (height * rowCount) / 2;
			containerRectTransform.offsetMin = new Vector2 (containerRectTransform.offsetMin.x, -scrollHeight);
			containerRectTransform.offsetMax = new Vector2 (containerRectTransform.offsetMax.x, (scrollHeight));

			//rowRectTransform.offsetMin = new Vector2 (100, 100);
			//rowRectTransform.offsetMax = new Vector2 (-100, -100);

			//create a new item, name it, and set the parent
			GameObject newItem = Instantiate (itemPrefab) as GameObject;

			newItem.transform.SetParent(gameObject.transform);

			SpriteRenderer renderer = newItem.GetComponent<SpriteRenderer> ();

			_imageContainer.sprite  = Sprite.Create (_image, new Rect (0, 0, _image.width, _image.height), new Vector2 (0f, 0f));
			renderer.sprite = _imageContainer.sprite;
		
			//move and size the new item
			RectTransform rectTransform = newItem.GetComponent<RectTransform> ();
	

			float y = (containerRectTransform.rect.height)  / 4 - (height * rowCount);
			rectTransform.offsetMin = new Vector2 (10, y);
			y = rectTransform.offsetMin.y + height;
			rectTransform.offsetMax = new Vector2 (-10, y);
			containerRectTransform.anchoredPosition = new Vector2(0,0);
			containerRectTransform.sizeDelta = new Vector2(0, Mathf.Abs(y) + 135);
		}

	}
}
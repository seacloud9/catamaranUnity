using UnityEngine;
using System.Collections;

namespace Catamaran.ThirdParty
{

	[RequireComponent (typeof(Renderer))]
	public class CTweenColor : CBaseTweener
	{
	
		public Color toColor = Color.white;

		private Color fromColor;

		protected override void Awake ()
		{
			base.Awake ();
		}

		public override void initFromValue ()
		{
			if (GetComponent<Renderer> () is SpriteRenderer)
				fromColor = (GetComponent<Renderer> () as SpriteRenderer).color;
			else
				fromColor = GetComponent<Renderer> ().sharedMaterial.color;
		}

		public override void playForward ()
		{
			initConfigs ();
			if (tweenController == null)
				initFromValue ();
			if (IsPlaying)
				stop ();

			if (GetComponent<Renderer> () is SpriteRenderer)
				(GetComponent<Renderer> () as SpriteRenderer).color = fromColor;
			else
				GetComponent<Renderer> ().sharedMaterial.color = fromColor;
		
			tweenController = CoolestTween.colorTo (transform, toColor, duration, config);
		}

		public override void playReverse ()
		{
			initConfigs ();
			if (tweenController == null)
				initFromValue ();
			if (IsPlaying)
				stop ();

			if (GetComponent<Renderer> () is SpriteRenderer)
				(GetComponent<Renderer> () as SpriteRenderer).color = toColor;
			else
				GetComponent<Renderer> ().sharedMaterial.color = toColor;
		
			tweenController = CoolestTween.colorTo (transform, fromColor, duration, config);
		}

		public override void resetValues ()
		{
			if (tweenController != null) {
				if (GetComponent<Renderer> () is SpriteRenderer)
					(GetComponent<Renderer> () as SpriteRenderer).color = fromColor;
				else
					GetComponent<Renderer> ().sharedMaterial.color = fromColor;
			}
		}
	}
}
using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture;	// the texture that will overlay the screen. This can be a black image or a loading graphic
	public float fadeSpeed = 0.8f;		// the fading speed

	private int drawDepth = -1000;		// the texture's order in the draw hierarchy: a low number means it renders on top
	private float alpha = 1.0f;			// the texture's alpha value between 0 and 1
	private int fadeDir = -1;			// the direction to fade: in = -1 or out = 1

	void OnGUI()
	{
		// fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to seconds
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		// force (clamp) the number to be between 0 and 1 because GUI.color uses Alpha values between 0 and 1
		alpha = Mathf.Clamp01(alpha);

		// set color of our GUI (in this case our texture). All color values remain the same & the Alpha is set to the alpha variable
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;	
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);	
		//todo make it take a screenshot then fade from that
		//StartCoroutine(TakeSnapshot(Screen.width,Screen.height));// make the black texture render on top (drawn last)
	}

	// sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
	public float BeginFade (int direction)
	{
		fadeDir = direction;
		return (fadeSpeed);
	}

	// OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int) as a parameter so you can limit the fade in to certain scenes.
	void OnLevelWasLoaded()
	{
		// alpha = 1;		// use this if the alpha is not set to 1 by default
		BeginFade(-1);		// call the fade in function
	}

	public IEnumerator TakeSnapshot(int width, int height) {
		yield return new WaitForEndOfFrame();
		Texture2D texture = new Texture2D (width, height, TextureFormat.RGB24, true); 
		texture.ReadPixels(new Rect(0, 0, width, height), 0, 0); 
		texture.Apply(); 
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);	
	} 
}

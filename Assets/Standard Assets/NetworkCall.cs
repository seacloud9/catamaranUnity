using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
//using UnityEngine.Experimental.Networking;
using System.Collections.Generic;


public class NetworkCall : MonoBehaviour {
	//public NavModel _data;
	public Texture2D _defaultImage;
	public string apiClass = "";
	public RectTransform _uiscrollerContainer;


	void Start () {
		//StartCoroutine(GetJSON());
	}

	// Update is called once per frame
	void Update () {

	}
	/*
	IEnumerator GetJSON() {
		UnityEngine.Networking.UnityWebRequest www = UnityWebRequest.Get(apiClass);
		yield return www.Send();
		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			this._helper = this.gameObject.AddComponent<ModelHelper>();
			this._helper._defaultImage = _defaultImage;
			this._helper._uiscrollerContainer = _uiscrollerContainer;
			//_data = new NavModel(www.downloadHandler.text, this);
		}
	}*/
}

using UnityEngine;
using System.Collections;

public class panoScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject.Find("GameManager").GetComponent<VRManager>().makeSphereUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

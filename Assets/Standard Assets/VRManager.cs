using UnityEngine;
using System.Collections;

public class VRManager : MonoBehaviour {

	public GameObject _userCamera;

	void Awake() {
		//Application.targetFrameRate = 300;
	}

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void makeSphereUI(){
		float l = 50;
		for ( float i = 0;  i < l; i ++ ) {
			GameObject planeGUI = CreatePlane();
			CreateSpehreUI(planeGUI, i, l);
		}
	}

	Mesh CreateMesh(float width, float height)
	{
		Mesh m = new Mesh();
		m.name = "ScriptedMesh";
		m.vertices = new Vector3[] {
			new Vector3(-width, -height, 0.01f),
			new Vector3(width, -height, 0.01f),
			new Vector3(width, height, 0.01f),
			new Vector3(-width, height, 0.01f)
		};
		m.uv = new Vector2[] {
			new Vector2 (0, 0),
			new Vector2 (0, 1),
			new Vector2(1, 1),
			new Vector2 (1, 0)
		};
		m.triangles = new int[] { 0, 1, 2, 0, 2, 3};
		m.RecalculateNormals();
		return m;
	}

	GameObject CreatePlane(){
		GameObject plane = new GameObject("Plane");
		MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
		meshFilter.mesh = CreateMesh(3.0f, 1.4f);
		MeshRenderer renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
		renderer.material.shader = Shader.Find ("Particles/Additive");
		Texture2D tex = new Texture2D(1, 1);
		tex.SetPixel(0, 0, Color.green);
		tex.Apply();
		renderer.material.mainTexture = tex;
		renderer.material.color = Color.green;
		return plane;
	}

	void CreateSpehreUI(GameObject meshObj, float i, float l){
		float phi = Mathf.Acos( -1 + ( 2 * i ) / l );
		float theta = Mathf.Sqrt( l * Mathf.PI ) * phi;
		meshObj.transform.position = new Vector3 ((26F * Mathf.Cos( theta ) * Mathf.Sin( phi )), (26F * Mathf.Sin( theta ) * Mathf.Sin( phi )), (26F * Mathf.Cos( phi )));
		Vector3 look = Vector3.Scale(new Vector3(_userCamera.transform.position.x, _userCamera.transform.position.y, _userCamera.transform.position.z), new Vector3(2.0F,2.0F,2.0F));
		meshObj.transform.LookAt(look);
	}


}

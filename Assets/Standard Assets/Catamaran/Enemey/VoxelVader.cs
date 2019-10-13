using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Catamaran.ThirdParty;

namespace Catamaran.Enemey
{

	public class VaderMat {
		public Shader shader;
		private Texture texture;
		private Color color;
		private Color emissionColor;
		private Renderer rend;

		public VaderMat(Renderer _rend, Color _color, Texture _texture, Color _emissionColor){
			rend = _rend;
			color = _color;
			texture = _texture;
			emissionColor = _emissionColor;
			Start ();
		}

		void Start() {
			rend.material = new Material(Shader.Find("Standard"));
			rend.material.color = color;
			rend.material.SetColor ("_EmissionColor", emissionColor);
			rend.material.EnableKeyword ("_EMISSION");
		}
	}

	public class VaderRow
	{
		public GameObject vaderBG;
		public GameObject vaderMid;
		public GameObject vaderFront;
		public GameObject _Parent;
		private Color VaderMatColorBG;
		private Color VaderMatColorMid;
		private Color VaderMatColorFront;
		private Texture VaderTexture;

		public VaderRow(GameObject _parent, Color _VaderMatColorBG, Color _VaderMatColorMid, Color _VaderMatColorFront, Texture _VaderTexture){
			VaderMatColorBG = _VaderMatColorBG;
			VaderMatColorMid = _VaderMatColorMid;
			VaderMatColorFront = _VaderMatColorFront;
			_Parent = _parent;
			VaderTexture = _VaderTexture;
			vaderBG = GameObject.CreatePrimitive (PrimitiveType.Cube);
			vaderBG.transform.parent = _Parent.transform;
			vaderBG.transform.localPosition = Vector3.zero;
			VaderMat _VaderMatBg = new VaderMat (vaderBG.GetComponent<Renderer> (), VaderMatColorBG, VaderTexture, VaderMatColorMid);
			vaderMid = GameObject.CreatePrimitive (PrimitiveType.Cube);
			VaderMat _VaderMatMid = new VaderMat (vaderMid.GetComponent<Renderer> (), VaderMatColorMid, VaderTexture, VaderMatColorBG);
			vaderMid.transform.parent = _Parent.transform;
			vaderMid.transform.localPosition = Vector3.zero;
			vaderFront = GameObject.CreatePrimitive (PrimitiveType.Cube);
			VaderMat _VaderMatFront = new VaderMat (vaderFront.GetComponent<Renderer> (), VaderMatColorFront, VaderTexture, VaderMatColorMid);
			vaderFront.transform.parent = _Parent.transform;
			vaderFront.transform.localPosition = Vector3.zero;
		}

	}

	public class VoxelVader : MonoBehaviour
	{
		public int size = 5;
		private int step;
		private int padding;
		public int points = 10;
		public int health = 2;
		public Texture _vaderTexture;
		private Color _bgColor;
		private Color _midColor;
		private Color _frontColor;
		public GameObject _SpawnObj;
		public Color _color1 = new Color();
		public Color _color2 = new Color();
		public Color _color3 = new Color();
		public Color _color4 = new Color();
		public Color _color5 = new Color();
		public Color _color6 = new Color();
		public List<Color> _colorPool = new List<Color>();
		// Use this for initialization
		void Start ()
		{
			step = (int) this.size / 5;
			padding = (int) this.size / 2;
			buildColorPool ();
			this.transform.Translate(_SpawnObj.transform.position.x - 20, _SpawnObj.transform.position.y - 50, _SpawnObj.transform.position.z - 55);
			buildVader();
			LeanTween.moveLocalZ (this.gameObject, _SpawnObj.gameObject.transform.position.z, 3f);
		}

		void buildColorPool(){
			_colorPool.Add (_color1);
			_colorPool.Add (_color2);
			_colorPool.Add (_color3);
			_colorPool.Add (_color4);
			_colorPool.Add (_color5);
			_colorPool.Add (_color6);
			int _bgInt = UnityEngine.Random.Range(0, _colorPool.Count);
			int _midInt = UnityEngine.Random.Range(0, _colorPool.Count);
			int _frontInt = UnityEngine.Random.Range(0, _colorPool.Count);
			_bgColor = _colorPool[_bgInt];
			_midColor = _colorPool[_midInt];
			_frontColor = _colorPool[_frontInt];
		}

		void buildVader(){
			Dictionary<int, Hashtable> col = new Dictionary<int, Hashtable>();
			for (int j = 0; j < this.size; j += this.step) {
				int m = 1;
				col[j] = new Hashtable();
				for (int i = 0; i < this.size / 2; i += this.step) {
					bool c = (UnityEngine.Random.value > .5) ? false : true;
					col[j][i] = c;
					col[j][i + (this.size - this.step) / m] = c;
					m++;
				}
			}


			for (int j = 0; j < this.size; j += this.step) {
				for (int i = 0; i < this.size; i += this.step) {
					VaderRow _vader = new VaderRow(this.gameObject, _bgColor, _midColor, _frontColor, _vaderTexture);
					bool isVisible = col[j][i] == null ? false : (bool)col[j][i];
					_vader.vaderBG.transform.Translate(i, j, 4);
					_vader.vaderBG.SetActiveRecursively(isVisible);
					_vader.vaderMid.transform.Translate(i, j, 5);
					_vader.vaderMid.SetActiveRecursively(isVisible);
					_vader.vaderFront.transform.Translate(i, j, 6);
					_vader.vaderFront.SetActiveRecursively(isVisible);
				}
			}

		}
	
		// Update is called once per frame
		void Update ()
		{
			
			float distance = Vector3.Distance( _SpawnObj.gameObject.transform.position, this.gameObject.transform.position);

			if ( distance < 100  )
			{
				Debug.Log ("player is close");

				Vector3 delta = _SpawnObj.gameObject.transform.position - this.gameObject.transform.position;
				delta.Normalize();

				float moveSpeed = 5f * Time.deltaTime;

				this.gameObject.transform.position = this.gameObject.transform.position + (delta * moveSpeed);
			}
			else
			{
				Debug.Log("not close yet " + distance);
			}


		}
	}
}

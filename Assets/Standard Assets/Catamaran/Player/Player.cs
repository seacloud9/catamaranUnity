using UnityEngine;
using System.Collections;

namespace Catamaran.Enemey
{
	public class Player : MonoBehaviour
	{
		public float _acceleration;
		public float _maxSpeed;
		public float _health;
		public float _zAcceleration;
		public float _yAcceleration;
		public float _xAcceleration;
		public Rigidbody _rigidBody;

		// Use this for initialization
		void Start ()
		{
			_rigidBody.AddForce(_xAcceleration, _yAcceleration, _zAcceleration);
		}
	
		// Update is called once per frame
		void Update ()
		{
			//this.transform.Translate(this.transform.position.x, this.transform.position.y, this.transform.position.z + acceleration * Time.deltaTime);
			if (_acceleration < _maxSpeed) {
				_acceleration += 1;
			}
		}
	}
}

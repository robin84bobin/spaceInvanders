using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour 
{
	public float XSpeed;
	public float YSpeed;
	public float ZSpeed;

	private Vector3 _rotVector;
	void Start()
	{
		_rotVector = new Vector3 (XSpeed, YSpeed, ZSpeed);
	}
	
	void Update () 
	{
		//this.transform.rotation = Quaternion.Euler (rotateVector);
		transform.Rotate (_rotVector);
	}
}

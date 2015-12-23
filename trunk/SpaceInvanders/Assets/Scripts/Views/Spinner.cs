using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour 
{
	public float XSpeed;
	public float YSpeed;
	public float ZSpeed;

	private Vector3 rotateVector;
	void Start()
	{
		rotateVector = new Vector3 (XSpeed, YSpeed, ZSpeed);
	}
	
	void Update () 
	{
		//this.transform.rotation = Quaternion.Euler (rotateVector);
		transform.Rotate (rotateVector);
	}
}

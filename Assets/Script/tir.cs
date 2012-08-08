using UnityEngine;
using System.Collections;

public class tir : MonoBehaviour {
	float t;
	// Use this for initialization
	void Start () {
		t=Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward*0.1f); 
		if(Time.time-t>3)
		{
			Destroy(gameObject); 
		}
	}
}

using UnityEngine;
using System.Collections;

public class tir : MonoBehaviour {
	float t;
	// Use this for initialization
	void Start () {
		t=Time.time;
	}
	
	
	void OnTriggerEnter(Collider other) 
	{
		//Debug.Log(other.name);
		if(other.name!="First Person Controller")Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward*Time.deltaTime*3); 
		
	}
}

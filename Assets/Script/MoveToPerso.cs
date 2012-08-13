using UnityEngine;
using System.Collections;

public class MoveToPerso : MonoBehaviour {
	
	
	//public GameObject Go;
	//private GameObject Head;
	// Use this for initialization
	//public float x,y,z=0;
	void Start () {
		//transform.LookAt(GameObject.Find("First Person Controller").transform.position);
		transform.Rotate(20,-90,0);
		transform.Translate(0.03f,-0.23f,0.25f);
	}
	
	// Update is called once per frame 
	void Update () {
		transform.Translate(Vector3.forward*0.1f); 
		
	}
	
	void OnTriggerEnter(Collider other) 
	{
		Destroy(gameObject);
	}
}

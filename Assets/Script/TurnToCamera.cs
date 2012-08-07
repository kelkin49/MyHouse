using UnityEngine;
using System.Collections;

public class TurnToCamera : MonoBehaviour {

 public Camera cameraToLookAt;
	private Vector3 v;
	void Start()
	{
		//transform.Rotate(180,90,0);
	}
	
    void Update()
    {
        v.y = cameraToLookAt.transform.position.y-90;
		v.x = cameraToLookAt.transform.position.x;
		v.z = cameraToLookAt.transform.position.z;
        transform.LookAt(v);
    }
}

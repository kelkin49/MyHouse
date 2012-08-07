using UnityEngine;
using System.Collections;

public class activelightroom : MonoBehaviour {
	
	public GameObject Tname;
	public bool setactive;
	
	 void OnTriggerEnter(Collider other) {
        Tname.SetActiveRecursively(setactive);
		
		//Debug.Log(Tname+" "+setactive);
    }
}

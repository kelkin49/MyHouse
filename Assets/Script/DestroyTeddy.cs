using UnityEngine;
using System.Collections;

public class DestroyTeddy : MonoBehaviour {
	
	private Color col=new Color(1,1,1);
	void OnTriggerEnter(Collider other) 
	{
		col.r-=0.2f;
		col.g-=0.2f;
		col.b-=0.2f;
		GameObject.Find("teddyzsphere_unfold3d").renderer.materials[0].SetColor("_Color", col);
	}
}

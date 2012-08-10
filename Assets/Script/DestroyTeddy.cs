using UnityEngine;
using System.Collections;

public class DestroyTeddy : MonoBehaviour {
	
	private Color col=new Color(1,1,1);
	public float nbPtsVie=5;
	
	void Update () {
		
		if(col.r>0)
		{
			Screen.showCursor=true;
			Screen.lockCursor=true;
			Screen.showCursor=true;
			
		}
		else
		{
			Screen.lockCursor=false;
			Screen.showCursor=true;
		}
	}
	
	
	void OnTriggerEnter(Collider other) 
	{
		
			col.r-=1/nbPtsVie;
			col.g-=1/nbPtsVie;
			col.b-=1/nbPtsVie;
			GameObject.Find("teddyzsphere_unfold3d").renderer.materials[0].SetColor("_Color", col);
		
	}
}

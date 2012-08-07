using UnityEngine;
using System.Collections;

public class Webtexture : MonoBehaviour {

	public MovieTexture m;
	RaycastHit hit;
	private bool vrai=false;
	public Material Texvid;
	
	
	
	void Start () {
		
	}
	
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		    if (Physics.Raycast(ray, out hit)) 
			{
			    if (hit.collider.gameObject.tag == "video") 
				{
					if (!vrai)
					{
						renderer.material.mainTexture=m;
						audio.clip=m.audioClip;
						m.Play();
						audio.Play();
						vrai=true;
						
					}
					else
					{
						m.Stop();
						audio.Stop();
						renderer.material.mainTexture = Texvid.mainTexture;
						vrai=false;
					}
				} 
			}			
	    }		
	}
}
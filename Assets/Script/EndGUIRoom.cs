using UnityEngine;
using System.Collections;

public class EndGUIRoom : MonoBehaviour {
	
	
	public GameObject[] teddy= new GameObject[4];
	public GameObject fps;
	public GameObject gun;
	public GameObject cube;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.name==fps.name)
		{
			if(fps.GetComponent<GUIRoom>().fin)
			{
				GameObject.Find("TeddyBear1/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].SetColor("_Color", new Color(1,1,1,1));
				GameObject.Find("TeddyBear2/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].SetColor("_Color", new Color(1,1,1,1));
				GameObject.Find("TeddyBear3/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].SetColor("_Color", new Color(1,1,1,1));
				GameObject.Find("TeddyBear4/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].SetColor("_Color", new Color(1,1,1,1));
				GameObject.Find("TeddyBear1").GetComponent<DestroyTeddy>().col=new Color(1,1,1,1);
				GameObject.Find("TeddyBear2").GetComponent<DestroyTeddy>().col=new Color(1,1,1,1);
				GameObject.Find("TeddyBear3").GetComponent<DestroyTeddy>().col=new Color(1,1,1,1);
				GameObject.Find("TeddyBear4").GetComponent<DestroyTeddy>().col=new Color(1,1,1,1);
				fps.GetComponent<GUIRoom>().fin=false;
			}
			gun.SetActiveRecursively(false);
			fps.GetComponent<GUIRoom>().enabled=false;
			teddy[0].GetComponent<DestroyTeddy>().enabled=false;
			teddy[1].GetComponent<DestroyTeddy>().enabled=false;
			teddy[2].GetComponent<DestroyTeddy>().enabled=false;
			teddy[3].GetComponent<DestroyTeddy>().enabled=false;
			cube.GetComponent<StartGUIRoom>().coroutine=true;
			Screen.lockCursor=false;
			
		}
	}
}

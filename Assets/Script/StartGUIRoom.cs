using UnityEngine;
using System.Collections;

public class StartGUIRoom : MonoBehaviour {

	public GameObject[] teddy= new GameObject[4];
	public GameObject fps;
	public GameObject gun;
	private bool coroutine =true;
	private string decompte="";
	public GameObject GuiDepart;
	public GUISkin guiskin;
	private bool guidecompte=false;
	
	void Start () {
	
	}
	
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.name==fps.name && coroutine)
		{
			coroutine=false;
			guidecompte=true;
			StartCoroutine("guicoroutine");
			
			gun.SetActiveRecursively(true);
			
		}
	}
	
	void OnGUI()
	{
		if(guidecompte==true)
		{
			GUI.skin=guiskin;
			GUI.Label(new Rect(0.4f*Screen.width,0.4f*Screen.height,0.2f*Screen.width,0.2f*Screen.height),decompte,"Decompte");
		}
	}
	
	IEnumerator guicoroutine()
	{
		//GuiDepart.SetActiveRecursively(true);
		decompte="3";
		yield return new WaitForSeconds(1f);
		decompte="2";
		yield return new WaitForSeconds(1f);
		decompte="1";
		yield return new WaitForSeconds(1f);
		decompte="";
		guidecompte=false;
		//GameObject.Find("GUIDepart").SetActiveRecursively(false);
		fps.GetComponent<GUIRoom>().enabled=true;
		teddy[0].GetComponent<DestroyTeddy>().enabled=true;
		teddy[1].GetComponent<DestroyTeddy>().enabled=true;
		teddy[2].GetComponent<DestroyTeddy>().enabled=true;
		teddy[3].GetComponent<DestroyTeddy>().enabled=true;
	}
}

using UnityEngine;
using System.Collections;

public class GUIRoom : MonoBehaviour {
	
	private float r,g,b;
	public Rigidbody projectile2;
	
	public GUISkin oGuiSkin;
	private float tps=0;
	public Texture2D[] Tex=new Texture2D[4];
	private string[] Cible=new string[4];
	public Texture vie;
	public Texture viseur;
	private float restevie=0.6f;
	private float fvie=0.6f;
	private float t;
	private bool cTrue=false;
	RaycastHit hit;
	public int ptsvie;
	private float tempsdepartgui;
	private string chrono="";
	
	// Use this for initialization
	void Start () {
		
		for (int i=0;i<Cible.Length;i++)
		{
			Cible[i]="Valide";
		}
		Screen.lockCursor=true;
		tempsdepartgui=tps=Time.time;
		r=1.0f;
		g=b=0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		chrono=((int)((tps-tempsdepartgui)/60)).ToString()+"m"+((int)((tps-tempsdepartgui)%60)).ToString()+"s";
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    if (Physics.Raycast(ray, out hit)) 
		{
		    if (hit.collider.gameObject != null && hit.collider.gameObject.name != "Laser2(Clone)" && hit.collider.gameObject.name != "Laser(Clone)") 
			{
				//GameObject.Find("Group22").transform.LookAt(new Vector3(hit.point.x,hit.point.y,hit.point.z));
				if (Input.GetMouseButtonDown(0)) 
				{
					//Debug.Log(hit.point);
					Rigidbody clone;
            		clone = Instantiate(projectile2,GameObject.Find("Group22").transform.position , Quaternion.LookRotation(hit.point-GameObject.Find("Group22").transform.position)) as Rigidbody;
				}
			}	
		}
		
		tps+=Time.deltaTime;
		if(cTrue)
		{
			restevie=Mathf.Lerp(restevie,fvie,(tps-t)/2);
			if(restevie==fvie)
			{
				cTrue=false;	
			}
		}
		changecolor();
	}
	
	void changecolor()
	{
		if (r==1 && g==0 && b<1)
		{
			b+=Time.deltaTime;
			if (b>1) b=1;
		}
		else if (r>0 && g==0 && b==1)
		{
			r-=Time.deltaTime;
			if (r<0) r=0;
		}
		else if (r==0 && g<1 && b==1)
		{
			g+=Time.deltaTime;
			if (g>1) g=1;
		}
		else if (r==0 && g==1 && b>0)
		{
			b-=Time.deltaTime;
			if (b<0) b=0;
		}
		else if (r<1 && g==1 && b==0)
		{
			r+=Time.deltaTime;
			if (r>1) r=1;
		}
		else if (r==1 && g>0 && b==0)
		{
			g-=Time.deltaTime;
			if (g<0) g=0;
		}
		
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.name=="Laser(Clone)")
		{
			Destroy(other.gameObject);
			if(restevie>=0)
			{
				fvie-=0.6f/ptsvie;
				t=Time.time;
				cTrue=true;
			}
		}
		
    }
	
	void OnGUI()
	{ 
		GUI.skin=oGuiSkin;
		GUI.color=new Color(r,g,b,0.1f);
		GUI.Label(new Rect(0,0,Screen.width,Screen.height),"");
		//GUI.color=new Color(1,1,1,1);
		
		GUI.color=new Color(1-(restevie-0.3f)/0.33f,1-3.33f*(0.3f-restevie),0,1);
		
		GUI.BeginGroup( new Rect(Screen.width*0.2f,0.9f*Screen.height, restevie*Screen.width, 0.1f*Screen.height));
		GUI.DrawTexture( new Rect(0,0,0.6f*Screen.width,0.1f*Screen.height),vie);
		GUI.EndGroup();
		//GUI.Label(new Rect(0.2f*Screen.width,0.9f*Screen.height,restevie*Screen.width,0.1f*Screen.height),"","vie");
		
		GUI.color=new Color(1,1,1,1);
		GUI.Label(new Rect(0.195f*Screen.width,0.9f*Screen.height,0.61f*Screen.width,0.1f*Screen.height),"","vie");
		GUI.Label(new Rect((0.85f+Mathf.PingPong(Time.time/20f,0.02f))*Screen.width,(0f+Mathf.PingPong(Time.time/20f,0.02f))*Screen.height,(0.15f-2*Mathf.PingPong(Time.time/20f,0.02f))*Screen.width,(0.1f-2*Mathf.PingPong(Time.time/20f,0.02f))*Screen.height),chrono,"Timer");
		if(GameObject.Find("TeddyBear1/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].color.r>0)
		{
			GUI.Label(new Rect(0f*Screen.width,0f*Screen.height,0.1f*Screen.width,0.1f*Screen.width),"","Valide");
		}
		else
		{
			GUI.Label(new Rect(0f*Screen.width,0f*Screen.height,0.1f*Screen.width,0.1f*Screen.width),"","Invalide");
		}
		
		if(GameObject.Find("TeddyBear2/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].color.r>0)
		{
			GUI.Label(new Rect(0f*Screen.width,0.1f*Screen.width,0.1f*Screen.width,0.1f*Screen.width),"","Valide");
		}
		else
		{
			GUI.Label(new Rect(0f*Screen.width,0.1f*Screen.width,0.1f*Screen.width,0.1f*Screen.width),"","Invalide");
		}
		
		if(GameObject.Find("TeddyBear3/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].color.r>0)
		{
			GUI.Label(new Rect(0f*Screen.width,0.2f*Screen.width,0.1f*Screen.width,0.1f*Screen.width),"","Valide");
		}
		else
		{
			GUI.Label(new Rect(0f*Screen.width,0.2f*Screen.width,0.1f*Screen.width,0.1f*Screen.width),"","Invalide");
		}
		
		if(GameObject.Find("TeddyBear4/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].color.r>0)
		{
			GUI.Label(new Rect(0f*Screen.width,0.3f*Screen.width,0.1f*Screen.width,0.1f*Screen.width),"","Valide");
		}
		else
		{
			GUI.Label(new Rect(0f*Screen.width,0.3f*Screen.width,0.1f*Screen.width,0.1f*Screen.width),"","Invalide");
		}
		//GUI.Label(new Rect(0f*Screen.width,0.1f*Screen.height,0.1f*Screen.width,0.1f*Screen.height),"",Cible[1]);
		//GUI.Label(new Rect(0f*Screen.width,0.2f*Screen.height,0.1f*Screen.width,0.1f*Screen.height),"",Cible[2]);
		//GUI.Label(new Rect(0f*Screen.width,0.3f*Screen.height,0.1f*Screen.width,0.1f*Screen.height),"",Cible[3]);
		
		GUI.BeginGroup( new Rect(0.48f*Screen.width,0.45f*Screen.height,0.05f*Screen.width,0.06f*Screen.height));
		GUI.DrawTexture( new Rect(0,0,0.05f*Screen.width,0.06f*Screen.height),viseur);
		GUI.EndGroup();
	}
}
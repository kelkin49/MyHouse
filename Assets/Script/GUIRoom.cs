using UnityEngine;
using System.Collections;

public class GUIRoom : MonoBehaviour {
	
	private float r,g,b;
	private float xHead,yHead,zHead=0f;
	private float xArm,yArm,zArm=0f;
	public Rigidbody projectile;
	public Rigidbody projectile2;
	
	public GUISkin oGuiSkin;
	private float tps=0;
	public Texture2D[] Tex=new Texture2D[4];
	private string[] Cible=new string[4];
	public Texture vie;
	//public Texture viebarcontour;
	private float restevie=0.6f;
	private float fvie=0.6f;
	private float t;
	private bool cTrue=false;
	private bool tire;
	private Quaternion rot;
	RaycastHit hit;
	// Use this for initialization
	void Start () {
		for (int i=0;i<Cible.Length;i++)
		{
			Cible[i]="Valide";
		}
		yHead=zHead=-90f;
		xArm=-180f;
		yArm=90f;
		zArm=-20f;
		r=1f; 
		g=b=0f;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    if (Physics.Raycast(ray, out hit)) 
		{
		    if (hit.collider.gameObject != null) 
			{
			//GameObject.Find("Group2").transform.LookAt((hit.point.x,hit.point.y+90,hit.point.z);
				if (Input.GetMouseButtonDown(0)) 
				{
			
					Rigidbody clone;
            		clone = Instantiate(projectile2,GameObject.Find("Group2").transform.position , Quaternion.LookRotation(hit.point-GameObject.Find("Group2").transform.position)) as Rigidbody;
					
				}
			}
				
		}
		
		
		GameObject.Find("Bip01 Head").transform.LookAt(transform.position);
		GameObject.Find("Bip01 Head").transform.Rotate(xHead,yHead,zHead);
		if (tps%4<3)
		{
			GameObject.Find("Bip01 R UpperArm").transform.LookAt(transform.position);
			
			GameObject.Find("Bip01 R UpperArm").transform.Rotate(xArm,yArm,zArm);
			tire=true;
		}
		if (tps%4>3.5 && tire == true)
		{
			rot=GameObject.Find("Bip01 R UpperArm").transform.rotation;
			Rigidbody clone;
            clone = Instantiate(projectile, GameObject.Find("Bip01 R Hand").transform.position, rot) as Rigidbody;
			tire=false;
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
		Destroy(other.gameObject);
		if(restevie>=0)
		{
			fvie-=0.12f;
			t=Time.time;
			cTrue=true;
		}
    }
	
	void OnGUI()
	{
		GUI.skin=oGuiSkin;
		GUI.color=new Color(r,g,b,0.2f);
		GUI.Label(new Rect(0,0,Screen.width,Screen.height),"");
		//GUI.color=new Color(1,1,1,1);
		
		GUI.color=new Color(1-(restevie-0.3f)/0.33f,1-3.33f*(0.3f-restevie),0,1);
		
		GUI.BeginGroup( new Rect(Screen.width*0.2f,0.9f*Screen.height, restevie*Screen.width, 0.1f*Screen.height));
		GUI.DrawTexture( new Rect(0,0,0.6f*Screen.width,0.1f*Screen.height),vie);
		GUI.EndGroup();
		//GUI.Label(new Rect(0.2f*Screen.width,0.9f*Screen.height,restevie*Screen.width,0.1f*Screen.height),"","vie");
		
		GUI.color=new Color(1,1,1,1);
		GUI.Label(new Rect(0.2f*Screen.width,0.9f*Screen.height,0.6f*Screen.width,0.1f*Screen.height),"","vie");
		GUI.Label(new Rect(0.85f*Screen.width,0f*Screen.height,0.15f*Screen.width,0.1f*Screen.height),tps.ToString("0.00"));
		
		GUI.Label(new Rect(0f*Screen.width,0f*Screen.height,0.1f*Screen.width,0.1f*Screen.height),Tex[0]);
		GUI.Label(new Rect(0f*Screen.width,0.1f*Screen.height,0.1f*Screen.width,0.1f*Screen.height),Tex[1]);
		GUI.Label(new Rect(0f*Screen.width,0.2f*Screen.height,0.1f*Screen.width,0.1f*Screen.height),Tex[2]);
		GUI.Label(new Rect(0f*Screen.width,0.3f*Screen.height,0.1f*Screen.width,0.1f*Screen.height),Tex[3]);
	}
}

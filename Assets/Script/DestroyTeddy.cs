using UnityEngine;
using System.Collections;

public class DestroyTeddy : MonoBehaviour {
	
	public Color col=new Color(1,1,1);
	public float nbPtsVie=5;
	private float yHead,zHead=0f;
	private float xArm,yArm,zArm=0f;
	private float tps=0;
	private bool tire;
	private Quaternion rot;
	public Rigidbody projectile;
	public int nb;
	private GameObject teddygun;
	private GameObject teddyhead;
	private GameObject teddyarm;
	private GameObject teddyorient;
	public float y=0;
	
	void Start () 
	{
		teddygun=GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Group1/Group4/Mesh3");
		teddyhead=GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 Head");
		teddyarm=GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm");
		teddyorient=GameObject.Find("First Person Controller");
		yHead=zHead=-90f;
		xArm=-180f;
		yArm=90f;
		zArm=-20f;
	}
	
	void Update () {
		tps+=Time.deltaTime;
		if(col.r>0 && !teddyorient.GetComponent<GUIRoom>().fin)
		{
			teddyhead.transform.LookAt(teddyorient.transform.position);
			teddyhead.transform.Rotate(0,yHead,zHead);
			
			teddyarm.transform.LookAt(teddyorient.transform.position);
			teddyarm.transform.Rotate(xArm,yArm,zArm);
			if (tps%4<3.5f)
			{
				tire=true;
			}
			if (tps%4>3.5f && tire)
			{
				rot=teddyarm.transform.rotation;
				Rigidbody clone;
	            clone = Instantiate(projectile, new Vector3(teddygun.transform.position.x,teddygun.transform.position.y-y,teddygun.transform.position.z), rot) as Rigidbody;
				tire=false;
			}
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if(other.name=="Laser2(Clone)")
		{
			col.r-=1/nbPtsVie;
			col.g-=1/nbPtsVie;
			col.b-=1/nbPtsVie;
			GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].SetColor("_Color", col);
		}
		
	}
}
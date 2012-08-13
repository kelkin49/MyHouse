using UnityEngine;
using System.Collections;

public class DestroyTeddy : MonoBehaviour {
	
	private Color col=new Color(1,1,1);
	public float nbPtsVie=5;
	private float yHead,zHead=0f;
	private float xArm,yArm,zArm=0f;
	private float tps=0;
	private bool tire;
	private Quaternion rot;
	public Rigidbody projectile;
	public int nb;
	
	void Start () 
	{
		yHead=zHead=-90f;
		xArm=-180f;
		yArm=90f;
		zArm=-20f;
	}
	
	void Update () {
		tps+=Time.deltaTime;
		if(col.r>0)
		{
			
			
			GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 Head").transform.LookAt(GameObject.Find("First Person Controller").transform.position);
			GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 Head").transform.Rotate(0,yHead,zHead);
			
			GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm").transform.LookAt(GameObject.Find("First Person Controller").transform.position);
			GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm").transform.Rotate(xArm,yArm,zArm);
			if (tps%4<3.5f)
			{
				tire=true;
			}
			if (tps%4>3.5f && tire)
			{
				rot=GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm").transform.rotation;
				Rigidbody clone;
	            clone = Instantiate(projectile, GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Group1/Group4").transform.position, rot) as Rigidbody;
				tire=false;
			}
		}
		else
		{
			
			//Screen.lockCursor=false;
			//Screen.showCursor=true;
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		
		//Debug.Log(other.name);
		if(other.name=="Laser2(Clone)")
		{
			col.r-=1/nbPtsVie;
			col.g-=1/nbPtsVie;
			col.b-=1/nbPtsVie;
			GameObject.Find("TeddyBear"+nb+"/teddy/Bip01/teddyzsphere_unfold3d").renderer.materials[0].SetColor("_Color", col);
		}
		
	}
}
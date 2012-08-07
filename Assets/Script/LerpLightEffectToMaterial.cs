using UnityEngine;
using System.Collections;

public class LerpLightEffectToMaterial : MonoBehaviour {
	
	private float Lerp;
	private bool ascendant=true;
	private bool pause=false;
	private float rc,gc,bc;
	
	// Use this for initialization
	void Start () {
		Lerp=1;
		rc=gc=1f;
		bc=0.5f;
	}
	
	// Update is called once per frame
	void Update () { 
		
		
		
		if (Lerp>0 && Lerp<0.75f)
		{
			gc=1-(0.6f*(Lerp/0.75f));
			bc=0.5f-(0.5f*(Lerp/0.75f));
		}
		else if(Lerp>=0.75f && Lerp<=1f)
		{ 
			rc=1-((Lerp-0.75f)/0.25f);
			gc=0.4f-(0.15f*((Lerp-0.75f)/0.25f));
			bc=((Lerp-0.75f)/0.25f);
		}
		
		
		if (ascendant)
		{
			Lerp+=Time.deltaTime/5f;
			if (Lerp>1f)
			{
				ascendant=false;
				pause=true;
			}
		}
		if (pause)
		{
			StartCoroutine("Pause");
		}
		if (!ascendant && !pause)
		{
			Lerp-=Time.deltaTime/5f;
			if (Lerp<0)
			{
				ascendant=true;	
			}
		}
		GameObject.Find("DayNightEffect").renderer.materials[0].SetFloat("_Blend",Lerp);
		gameObject.light.color = new Color(rc,gc,bc);
	}
	
	IEnumerator Pause()
	{
		yield return new WaitForSeconds(3f);
		pause=false;
	}
}

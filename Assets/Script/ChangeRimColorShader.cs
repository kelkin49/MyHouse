using UnityEngine;
using System.Collections;

public class ChangeRimColorShader : MonoBehaviour {
	
	private float r,g,b;
	public string ColorName;
	public bool material;
	public bool Colorlight;
	public bool strombolight;
	private float rateX;
	private float rateZ;
	
	// Use this for initialization
	void Start () {
		r=1f;
		g=b=0f;
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Colorlight)
		{
			gameObject.light.color = new Color(r,g,b);
		}
		
		
		if (strombolight)
		{
			
			rateX=GameObject.Find("First Person Controller").transform.position.x-transform.position.x;
			rateZ=GameObject.Find("First Person Controller").transform.position.z-transform.position.z;
			
			if (rateX<0 && rateX>-5f && rateZ<0.6f && rateZ>-0.35f)
			{
				if(Time.time%(0.5f)>0 &&  Time.time%(0.5f)<0.05f)
				{
					gameObject.light.intensity=5f;
				}
				else
				{
					gameObject.light.intensity=2f;
				}
			}
		}
		
		if (material)
		{
			renderer.materials[0].SetColor(ColorName, new Color(r,g,b));
		}
		
		
		
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
}

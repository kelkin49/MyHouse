using UnityEngine;
using System.Collections;

public class cameraTexture : MonoBehaviour {

	//public MovieTexture m;
	RaycastHit hit;
	private bool vraicamtex=false;
	private bool vraimp3=false;
	private bool vraiwebcam=false;
	private bool vraivideo=false;
	public Material Texcam;
	private WebCamTexture webc;
	public Material Texwebcam;
	private Texture2D texSnapshot;
	public RenderTexture renderTexture;
	public MovieTexture movie;
	public Material Texvid;
	private bool g=false;
	public GUISkin oGuiskin;
	private string textedescription="";
	
	
	void Start() 
	{
		RenderTexture.active=null;	
	}
	
	void Update () {
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    if (Physics.Raycast(ray, out hit)) 
		{
		    if (hit.collider.gameObject.tag != null)
			{
				
				switch(hit.collider.gameObject.tag)
				{
					case "camtex":
						g=true;
						textedescription="camera texture: render camera to texture2D";
						if (Input.GetMouseButtonDown(0)) 
						{
							if (!vraicamtex)
							{
								RenderTexture.active=renderTexture;	
								GameObject.FindGameObjectWithTag("camtex").renderer.material.mainTexture=renderTexture;
								vraicamtex=true;
							
							
								vraimp3=false;
								GameObject.FindGameObjectWithTag("musique").audio.Stop();
								vraiwebcam=false;
								if(webc!=null)webc.Stop();
								GameObject.FindGameObjectWithTag("webcam").renderer.material.mainTexture = Texwebcam.mainTexture;
								movie.Stop();
								GameObject.FindGameObjectWithTag("video").audio.Stop();
								GameObject.FindGameObjectWithTag("video").renderer.material.mainTexture = Texvid.mainTexture;
								vraivideo=false;
							}
							else
							{
								GameObject.FindGameObjectWithTag("camtex").renderer.material.mainTexture = Texcam.mainTexture;
								RenderTexture.active=null;	
								vraicamtex=false;
							}
						} 
					break;
					
					case "musique":
						g=true;
						textedescription="mp3:play sound\nenable:click (Raycast)";
						if (Input.GetMouseButtonDown(0)) 
						{
							if (!vraimp3)
							{
								GameObject.FindGameObjectWithTag("musique").audio.Play();
								//audio.Play();
								vraimp3=true;
							
							
								vraicamtex=false;
								RenderTexture.active=null;
								GameObject.FindGameObjectWithTag("camtex").renderer.material.mainTexture = Texcam.mainTexture;
								vraiwebcam=false;
								if(webc!=null)webc.Stop();
								GameObject.FindGameObjectWithTag("webcam").renderer.material.mainTexture = Texwebcam.mainTexture;
								movie.Stop();
								GameObject.FindGameObjectWithTag("video").audio.Stop();
								GameObject.FindGameObjectWithTag("video").renderer.material.mainTexture = Texvid.mainTexture;
								vraivideo=false;
							}
							else
							{
								GameObject.FindGameObjectWithTag("musique").audio.Stop();
								//audio.Stop();
								vraimp3=false;
							}
						} 
					break;
					
					case "video":
						g=true;
						textedescription="WebcamTexture\nenable:click (Raycast)";
						if (Input.GetMouseButtonDown(0)) 
						{
							if (!vraivideo)
							{
								GameObject.FindGameObjectWithTag("video").renderer.material.mainTexture=movie;
								GameObject.FindGameObjectWithTag("video").audio.clip=movie.audioClip;
								movie.Play();
								GameObject.FindGameObjectWithTag("video").audio.Play();
								vraivideo=true;
							
								
								vraicamtex=false;
								RenderTexture.active=null;
								GameObject.FindGameObjectWithTag("camtex").renderer.material.mainTexture = Texcam.mainTexture;
								vraimp3=false;
								GameObject.FindGameObjectWithTag("musique").audio.Stop();
								vraiwebcam=false;
								if(webc!=null)webc.Stop();
								GameObject.FindGameObjectWithTag("webcam").renderer.material.mainTexture = Texwebcam.mainTexture;
								
							}
							else
							{
								movie.Stop();
								GameObject.FindGameObjectWithTag("video").audio.Stop();
								GameObject.FindGameObjectWithTag("video").renderer.material.mainTexture = Texvid.mainTexture;
								vraivideo=false;
							}
						} 
					break;
					
					case "webcam":
						g=true;
						textedescription="WebcamTexture\nenable:click (Raycast)";
						if (Input.GetMouseButtonDown(0)) 
						{
							if (!vraiwebcam)
							{
								webc=new WebCamTexture();
								GameObject.FindGameObjectWithTag("webcam").renderer.material.mainTexture = webc;
								webc.Play();
								vraiwebcam=true;
							
								
								vraicamtex=false;
								RenderTexture.active=null;	
								GameObject.FindGameObjectWithTag("camtex").renderer.material.mainTexture = Texcam.mainTexture;
								vraimp3=false;
								GameObject.FindGameObjectWithTag("musique").audio.Stop();
								movie.Stop();
								GameObject.FindGameObjectWithTag("video").audio.Stop();
								GameObject.FindGameObjectWithTag("video").renderer.material.mainTexture = Texvid.mainTexture;
								vraivideo=false;
							}
							else
							{
								vraiwebcam=false;
								webc.Stop();
								GameObject.FindGameObjectWithTag("webcam").renderer.material.mainTexture = Texwebcam.mainTexture;
							}
						} 
					break;
					
					
					default:
						g=false;
					break;
				}
			}
			//else
			//{
				
			//}
	    }
	}
	
	void OnGUI (){
		if(g)
		{
			GUI.skin=oGuiskin;
			GUI.color=new Color(1,1,1,0.5f);
			GUI.Label(new Rect(0.03f*Screen.width,0.75f*Screen.height,0.94f*Screen.width,0.22f*Screen.height),textedescription);
		}
	}
}
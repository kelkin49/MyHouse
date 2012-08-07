using UnityEngine;
using System.Collections;

public class RadioPlay : MonoBehaviour {
	
	private bool vrai=false;
	private string vlcexe="\"C:\\Program Files\\VideoLAN\\VLC\\vlc.exe\"";
	//private string cmdexe="cmd.exe";
	//mms://vipmms9.yacast.net/bfm_bfmtv 
	//private string convert2="-I dummy \"mms://vipmms9.yacast.net/bfm_bfmtv\" --sout=#transcode{scale=1,deinterlace,vcodec=theo,vb=1500,acodec=vorb,ab=128,channels=2,samplerate=44100}:std{access=file,mux=ogg,dst=C:\\stream.ogg}";
	private string convert="-I dummy \"http://streaming.radio.rtl.fr/rtl-1-44-96\" --sout=#transcode{scale=1,deinterlace,vcodec=theo,vb=1500,acodec=vorb,ab=128,channels=2,samplerate=44100}:std{access=file,mux=ogg,dst=C:\\stream.ogg}";
	private string del="/c del C:\\stream.ogg";
	RaycastHit hit;
	
	public string createfolder="/c echo. > C:\\stream.ogg";
	private AudioClip m;
	WWW wwwData;
	public string url="file://C:/stream.ogg";
	private bool a = false;
	// Use this for initialization
	void Start () {
		
		CommandLine("cmd.exe",del);
		CommandLine("cmd.exe",createfolder);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(a) Debug.Log(audio.clip.isReadyToPlay +" " +audio.isPlaying);

		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		    if (Physics.Raycast(ray, out hit)) 
			{
			    if (hit.collider.gameObject.tag == "radio") 
				{
					if (!vrai)
					{
						CommandLine("cmd.exe",createfolder);
						vrai=true;
						StartCoroutine("playradio");
						
					}
					else
					{
						
						audio.Stop();
						vrai=false;
						CommandLine("cmd.exe","/c taskkill /IM vlc.exe /F");
						
						CommandLine("cmd.exe","/c taskkill /IM cmd.exe /F");
						StartCoroutine("pause");
					}
					
				} 
			}			
	    }
	
	}
	
	IEnumerator playradio()
	{ 
		Debug.Log("rrr");
		
		yield return new WaitForSeconds(0.1f);
		Debug.Log("rrr1");
		CommandLine(vlcexe, convert);
		yield return new WaitForSeconds(3f);
		Debug.Log("rrr2");
		wwwData = new WWW(url);
		audio.clip = wwwData.GetAudioClip(true,true);
		a=true;
		while(!audio.clip.isReadyToPlay){yield return null;}
		
		Debug.Log("rrr4");
		
		audio.Play();
		//yield return StartCoroutine("retour");
    }
	
	/*IEnumerator retour()
	{ 
		if(audio.clip.isReadyToPlay){yield return null;}
		if(!audio.clip.isReadyToPlay){audio.Stop();}
		if (!audio.isPlaying)
		{
			audio.Play();
		}
		yield return StartCoroutine("retour");
    }*/
	
	IEnumerator pause()
	{ 
		yield return new WaitForSeconds(3f);
		CommandLine("cmd.exe",del);
		yield return new WaitForSeconds(0.1f);
    }
	
	void CommandLine(string exe, string cmdline)
	{
		//Debug.Log(cmdline);
		System.Diagnostics.Process process = new System.Diagnostics.Process();
		System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
		startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
		startInfo.FileName = exe;
		startInfo.Arguments = cmdline;
		process.StartInfo = startInfo;
		process.Start();
		
		
	}
}

using UnityEngine;
using System.Collections;

public class DayNightMaterial : MonoBehaviour {

	public Material material1;
    public Material material2;
    public float duration = 2.0F;
	private float lerp;
    void Start() {
        renderer.material = material1;
    }
    void Update() {
        lerp += (Time.deltaTime/10f);
        renderer.material.Lerp(material1, material2, lerp);
		Debug.Log(lerp);
    }
}

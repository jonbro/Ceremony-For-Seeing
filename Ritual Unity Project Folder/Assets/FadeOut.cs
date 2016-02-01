using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FadeOut : MonoBehaviour {
	List<Material> mats = new List<Material>();
	public Shader finalShader;
	// Use this for initialization
	float age = 0;
	public float fadeSpeed = 1;
	bool fadeComplete;
	void Start () {
		mats.Add(GetComponent<MeshRenderer>().material);
		mats[0].SetFloat("_RejectBelow", 1);
		age = 1;
	}

	// Update is called once per frame
	void Update () {
		if(fadeComplete){
			return;
		}
		age -= Time.deltaTime*fadeSpeed;
		foreach(Material mat in mats){
			mat.SetFloat("_RejectBelow", age);
		}
		if(age < 0.1){
			GetComponent<MeshCollider>().enabled = false;
		}
		if(age < 0){
			GetComponent<MeshRenderer>().enabled = false;
			fadeComplete = true;
		}
	}
}

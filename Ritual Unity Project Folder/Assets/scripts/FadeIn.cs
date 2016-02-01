using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FadeIn : MonoBehaviour {
	List<Material> mats = new List<Material>();
	public Shader finalShader;
	// Use this for initialization
	float age = 0;
	bool fadeComplete;
	void Start () {
		foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>()){
			mats.Add(mr.material);
			mr.material.SetFloat("_RejectBelow", 0);
		}
		age = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(fadeComplete)
			return;
		age += Time.deltaTime;
		foreach(Material mat in mats){
			mat.SetFloat("_RejectBelow", age);
		}
		if(age > 10){
			foreach(Material mat in mats){
				mat.shader = finalShader;
			}
			fadeComplete = true;
		}
	}
}

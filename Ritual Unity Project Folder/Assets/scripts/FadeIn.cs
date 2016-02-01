using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FadeIn : MonoBehaviour {
	List<Material> mats = new List<Material>();
	// Use this for initialization
	float age = 0;

	void Start () {
		foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>()){
			mats.Add(mr.material);
			mr.material.SetFloat("_RejectBelow", 0);
		}
		age = 0;
	}
	
	// Update is called once per frame
	void Update () {
		age += Time.deltaTime;
		foreach(Material mat in mats){
			mat.SetFloat("_RejectBelow", age);
		}
	}
}

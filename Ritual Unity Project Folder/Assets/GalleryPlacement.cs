﻿using UnityEngine;
using System.Collections;

public class GalleryPlacement : MonoBehaviour {
	bool hasPlayer, hasObject;
	public GameObject objectPosition;
	void Update(){
		if(hasPlayer 
			&& Input.GetMouseButtonDown(0) 
			&& GameController.instance.holdingObject.holdingObject != null
			&& GameController.instance.holdingObject.holdingObject.GetComponent<Ritualized>() != null
			&& !hasObject)
		{
			GameObject holdObject = GameController.instance.holdingObject.holdingObject;
			holdObject.transform.position = objectPosition.transform.position;
			holdObject.transform.SetParent(objectPosition.transform);
			GameController.instance.holdingObject.RemoveHoldingObject();
		}
	}
	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			hasPlayer = true;
		}
	}
	void OnTriggerExit(Collider other) {
		if(other.tag == "Player"){
			hasPlayer = false;
		}
	}
}

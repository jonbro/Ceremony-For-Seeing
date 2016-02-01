using UnityEngine;
using System.Collections;

public class StonePickup : MonoBehaviour {
	bool hasPlayer;
	public FadeOut fadeOutOnpickup;
	void Update(){
		if(hasPlayer && Input.GetMouseButtonDown(0)){
			if(GameController.instance.holdingObject.holdingObject == null){
				GameController.instance.holdingObject.SetHoldingObject(transform.parent.gameObject);
				gameObject.SetActive(false);
				fadeOutOnpickup.enabled = true;
			}
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

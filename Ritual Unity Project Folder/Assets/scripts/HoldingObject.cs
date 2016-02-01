using UnityEngine;
using System.Collections;

public class HoldingObject : MonoBehaviour {
	public GameObject holdPosition, lookPosition, holdPositionTarget;
	public GameObject currentTarget;
	public GameObject holdingObject;
	FirstPersonDrifter drifter;
	MouseLook mouseLook, mouseLookVert;
	public float lookMoveSpeed = 9;
	ArcBall arcBall;
	// Use this for initialization
	void Start () {
		drifter = GetComponent<FirstPersonDrifter>();
		mouseLook = GetComponent<MouseLook>();
		mouseLookVert = Camera.main.GetComponent<MouseLook>();
		arcBall = holdPosition.GetComponent<ArcBall>();
		currentTarget = holdPositionTarget;

	}
	
	// Update is called once per frame
	void Update () {
		if(holdingObject != null && Input.GetMouseButton(1)){
			drifter.enabled = false;
			mouseLook.enabled = false;
			mouseLookVert.enabled = false;
			arcBall.enabled = true;
			currentTarget = lookPosition;
		}else{
			drifter.enabled = true;
			mouseLook.enabled = true;
			mouseLookVert.enabled = true;
			arcBall.enabled = false;
			currentTarget = holdPositionTarget;
		}
		holdPosition.transform.position = Vector3.Lerp(holdPosition.transform.position, currentTarget.transform.position, lookMoveSpeed * Time.deltaTime);

	}
	public void SetLayerRecursive(GameObject obj, int targetLayer){
		obj.layer = targetLayer;
		foreach(Transform child in obj.transform){
			SetLayerRecursive(child.gameObject, targetLayer);
		}

	}
	public void SetHoldingObject(GameObject obj){
		obj.transform.position = holdPosition.transform.position;
		obj.transform.SetParent(holdPosition.transform);
		SetLayerRecursive(obj, 8);
		holdingObject = obj;
	}
	public void RemoveHoldingObject()
	{
		SetLayerRecursive(holdingObject, 0);
		holdingObject = null;
	}
}

using UnityEngine;
using System.Collections;

public class HoldingObject : MonoBehaviour {
	public GameObject holdPosition;
	public GameObject holdingObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SetHoldingObject(GameObject obj){
		obj.transform.position = holdPosition.transform.position;
		obj.transform.SetParent(holdPosition.transform);
		holdingObject = obj;
	}
	public void RemoveHoldingObject()
	{
		holdingObject = null;
	}
}

using UnityEngine;
using System.Collections;

public class RitualPickup : MonoBehaviour {
	public TextMesh textMesh;
	public string text;
	public GameObject backgroundMesh;
	public GameObject ritualObject;
	// Use this for initialization
	void Start () {
		textMesh.text = text;

	}
	
	// Update is called once per frame
	void Update () {
		bool lookingAt = false;
		textMesh.color = Color.black;
		// check to see if the player is looking at this, and then highlight if so
		RaycastHit[] hits;
		hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.forward, 100.0F);
		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];
			if(hit.collider == backgroundMesh.GetComponent<Collider>()){
				textMesh.color = Color.red;
				lookingAt = true;
			}
		}
		if(lookingAt && Input.GetMouseButtonDown(0)){
			gameObject.SetActive(false);
			// instance the ritual object and attach it to the player
			GameObject ritualObjectInstance = (GameObject)Instantiate(ritualObject, Vector3.zero, Quaternion.identity);
			GameController.instance.holdingObject.SetHoldingObject(ritualObjectInstance);
		}
	}
}

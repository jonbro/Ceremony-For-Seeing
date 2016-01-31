using UnityEngine;
using System.Collections;

public class BrazierAnimation : MonoBehaviour {
	public GameObject fireRitualPrefab;
	void ObjectPlaced(GameObject obj){
		GameObject fireRitual = (GameObject)Instantiate(fireRitualPrefab, obj.transform.position, Quaternion.identity);
		fireRitual.transform.SetParent(obj.transform);
		SendMessage("RitualComplete", obj);
	}
}

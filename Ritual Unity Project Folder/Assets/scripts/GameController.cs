using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour {
	private static GameController _instance;
	public List<GameObject> teleportLocations;
	private int galleryPlacementCount = 0;
	public FadeIn pathFade;
	public static GameController instance{
		get{
			if(_instance == null){
				GameObject go = new GameObject();
				_instance = go.AddComponent<GameController>();
			}
			return _instance;
		}
	}

	public GameObject player;
	public HoldingObject holdingObject;
	// Use this for initialization
	void Awake () {
		if(_instance == null){
			_instance = this;
		}
		if(player == null){
			//TODO: find the player
			player = Camera.main.transform.parent.gameObject;
		}
		holdingObject = player.GetComponentInChildren<HoldingObject>();
	}
	
	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.Alpha1))
			player.transform.position = teleportLocations[0].transform.position;
		if(Input.GetKeyDown(KeyCode.Alpha2))
			player.transform.position = teleportLocations[1].transform.position;
		if(Input.GetKeyDown(KeyCode.Alpha3))
			player.transform.position = teleportLocations[2].transform.position;
		if(Input.GetKeyDown(KeyCode.Alpha4))
			player.transform.position = teleportLocations[3].transform.position;
		if(Input.GetKeyDown(KeyCode.Alpha0))
			PlaceInGallery();
		#endif
	}
	public void PlaceInGallery(){
		galleryPlacementCount++;
		if(galleryPlacementCount == 4){
			pathFade.enabled = true;
		}
	}
}

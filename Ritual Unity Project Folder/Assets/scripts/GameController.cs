using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GameController : MonoBehaviour {
	private static GameController _instance;
	public List<GameObject> teleportLocations;
	public List<AudioClip> voiceOver;
	private int galleryPlacementCount = 0;
	private int pickupCount = 0;
	public FadeIn pathFade;
	public GameObject rock;
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
		StartCoroutine(PlayFirstClip());
	}
	IEnumerator PlayFirstClip(){
		yield return new WaitForSeconds(1.0f);
		player.GetComponent<AudioSource>().PlayOneShot(voiceOver[0]);
	}
	public void PickupObject(){
		pickupCount++;
		if(pickupCount == 1){
			player.GetComponent<AudioSource>().PlayOneShot(voiceOver[1]);
		}
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
		player.GetComponent<AudioSource>().PlayOneShot(voiceOver[galleryPlacementCount+1]);
		if(galleryPlacementCount == 4){
			pathFade.enabled = true;
			rock.SetActive(true);
		}
	}
}

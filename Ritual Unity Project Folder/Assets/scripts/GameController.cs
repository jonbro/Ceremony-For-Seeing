using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private static GameController _instance;
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
	
	}
}

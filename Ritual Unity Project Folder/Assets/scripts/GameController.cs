using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	private static GameController _instance;
	public static GameController instance{
		get{
			return _instance;
		}
	}

	public GameObject player;
	public HoldingObject holdingObject;
	// Use this for initialization
	void Start () {
		_instance = this;
		if(player == null){
			//TODO: find the player
		}
		holdingObject = player.GetComponentInChildren<HoldingObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Intoned : MonoBehaviour {
	public List<AudioClip> clips;
	public List<float>	timings;
	public List<AudioSource> players;
	public float maxTime = 0;
	// Use this for initialization
	void Start () {
//		audioSource = GetComponent<AudioSource>();
	}
	public void startPlaying(){
		Play();
	}
	void Play(){
		int count = 0;
		foreach(float timing in timings){
			float hammerTime = timing;
			float lastRotVal = 0;
			int internalCount = count;
			LeanTween.value(gameObject, (float rotVal)=>{
				if(lastRotVal%360 > rotVal%360 && rotVal != 360*5){
					players[internalCount].clip = clips[internalCount];
					players[internalCount].Play();
						
//					audioSource.PlayOneShot(clips[0]);
				}
				lastRotVal = rotVal;
			}, 0, 360*5, hammerTime).setEase(LeanTweenType.easeInOutCubic);
			count++;
		}
		StartCoroutine(WaitAndReplay(maxTime));
	}
	IEnumerator WaitAndReplay(float WaitTime){
		yield return new WaitForSeconds(WaitTime);
		Play();
	}
	// Update is called once per frame
	void Update () {
	
	}
}

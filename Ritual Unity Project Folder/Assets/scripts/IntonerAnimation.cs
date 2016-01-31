using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IntonerAnimation : MonoBehaviour {
	public List<AudioClip> audioClips;
	public List<GameObject> hammers;
	public GameObject intonedPrefab;
	void ObjectPlaced(GameObject obj){
		float maxTime = 0;
		int count = 0;
		GameObject intoned = (GameObject)Instantiate(intonedPrefab, obj.transform.position, Quaternion.identity);
		intoned.transform.SetParent(obj.transform);
		List<float> timings = new List<float>();
		foreach(GameObject hammer in hammers){
			int internalCount = count;
			float hammerTime = 4+5*Random.value;
			timings.Add(hammerTime);
			GameObject _captureHammer = hammer;
			Quaternion originalRot = _captureHammer.transform.rotation;
			float lastRotVal = 0;
			LeanTween.value(_captureHammer, (float rotVal)=>{
				_captureHammer.transform.rotation = originalRot*Quaternion.Euler(0,0,rotVal);
				if(lastRotVal%360 > rotVal%360 && rotVal != 360*5){
					intoned.GetComponent<Intoned>().players[internalCount].clip = audioClips[internalCount];
					intoned.GetComponent<Intoned>().players[internalCount].Play();
				}
				lastRotVal = rotVal;
			}, 0, 360*5, hammerTime).setEase(LeanTweenType.easeInOutCubic);
			maxTime = Mathf.Max(hammerTime, maxTime);
			count++;
		}
		intoned.GetComponent<Intoned>().maxTime = maxTime;
		intoned.GetComponent<Intoned>().timings = timings;
		intoned.GetComponent<Intoned>().clips = audioClips;
		StartCoroutine(RunAnimation(obj, intoned, maxTime));
	}
	IEnumerator RunAnimation(GameObject target, GameObject intoned, float waitTime){
		yield return new WaitForSeconds(waitTime);
		SendMessage("RitualComplete", target);
		intoned.GetComponent<Intoned>().startPlaying();
	}

}

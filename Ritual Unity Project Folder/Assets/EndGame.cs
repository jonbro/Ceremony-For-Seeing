using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EndGame : MonoBehaviour {
	public float fadeOutTime = 4.0f;
	public float creditFadeTime = 0.5f;
	public GameObject canvas;
	public UnityEngine.UI.Image panel;
	public List<UnityEngine.UI.Text> credits;
	public UnityEngine.Audio.AudioMixer mixer;
	public UnityEngine.Audio.AudioMixerSnapshot noSoundtrack;

	void Start(){
		canvas.SetActive(false);
	}
	void ObjectPlaced(GameObject obj){
		StartCoroutine(Blackout());
		GameController.instance.player.GetComponent<AudioSource>().PlayOneShot(GameController.instance.voiceOver[7]);
	}
	IEnumerator Blackout(){
		foreach(UnityEngine.UI.Text t in credits){
			t.color = new Color(0,0,0,0);
		}

		panel.color = new Color(0,0,0,0);
		canvas.SetActive(true);
		float currentTime = 0;
		while(currentTime < fadeOutTime){
			yield return new WaitForEndOfFrame();
			currentTime+=Time.deltaTime;
			panel.color = new Color(0,0,0,currentTime/fadeOutTime);
		}
		yield return new WaitForSeconds(1.0f);
		currentTime = 0;
		while(currentTime < creditFadeTime){
			yield return new WaitForEndOfFrame();
			currentTime+=Time.deltaTime;
			foreach(UnityEngine.UI.Text t in credits){
				t.color = new Color(1,1,1,currentTime/creditFadeTime);
			}
		}
		yield return new WaitForSeconds(3.0f);
		mixer.TransitionToSnapshots(new UnityEngine.Audio.AudioMixerSnapshot[]{noSoundtrack}, new float[]{1}, 3.0f);
		yield return new WaitForSeconds(5.0f);
		Application.Quit();
	}
}

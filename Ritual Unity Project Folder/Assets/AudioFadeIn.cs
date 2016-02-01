using UnityEngine;
using System.Collections;

public class AudioFadeIn : MonoBehaviour {
	AudioSource audioSource;
	public UnityEngine.Audio.AudioMixer mixer;
	public UnityEngine.Audio.AudioMixerSnapshot noSoundtrack, soundtrack;
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		mixer.TransitionToSnapshots(new UnityEngine.Audio.AudioMixerSnapshot[]{soundtrack}, new float[]{1}, 3.0f);
	}
	// Update is called once per frame
	void Update () {
	
	}
}

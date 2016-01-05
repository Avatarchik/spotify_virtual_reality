using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {
	// the audio source attached to this component
	private AudioSource audioSource;

	float t = 0.0f;

	float fadeTime = 5;

	// Use this for initialization
	void Start () {
		this.audioSource = GetComponent<AudioSource>();
		this.t = 0;
	}

	void Update() {
		
		if (audioSource.volume < 1 && audioSource.volume != 0) {
			t += Time.deltaTime;
			audioSource.volume = Mathf.Lerp (0, 1, t / fadeTime);
		}
		else if(audioSource.volume == 1) {
			audioSource.volume = 0;
		}
	}
		

	// start the audio of the selected index
	public void playAudio(AudioClip audioClip) {
		if (audioSource.clip != audioClip) {
			audioSource.clip = audioClip;
			audioSource.volume = 0.01f;
			this.t = 0;
			audioSource.Play ();
		}
	}
}

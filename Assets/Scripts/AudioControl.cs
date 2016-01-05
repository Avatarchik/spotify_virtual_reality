using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {
	// the audio source attached to this component
	private AudioSource audioSource;

	float t = 0.0f;

	float fadeTime = 5;

	float timeStart;

	// Use this for initialization
	void Start () {
		this.audioSource = GetComponent<AudioSource>();
		this.t = 0;
	}

	void Update() {
		if (Time.time > this.timeStart + 4) {
			// we need to fade music out
			t += Time.deltaTime;
			audioSource.volume = Mathf.Lerp (1, 0, t / 1);
		} else if (Time.time < this.timeStart + 3.9) {
			// we need to fade music in
			t += Time.deltaTime;
			audioSource.volume = Mathf.Lerp (0, 1, t / 4);
		} else {
			t = 0;
		}
	}
		

	// start the audio of the selected index
	public void playAudio(string audioName) {
		AudioClip audioClip = (AudioClip)Resources.Load("Audio/Songs/" + audioName, typeof(AudioClip));

		if (audioSource.clip != audioClip) {
			audioSource.clip = audioClip;
			audioSource.volume = 0;
			this.t = 0;
			this.timeStart = Time.time;
			audioSource.Play ();
		}
	}

	public bool audioFinish() {
		return audioSource.isPlaying;
	}
}

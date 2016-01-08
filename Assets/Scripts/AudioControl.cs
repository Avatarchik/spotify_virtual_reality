using UnityEngine;
using System.Collections;


public class AudioControl : MonoBehaviour {
	// the audio source attached to this component
	private AudioSource audioSource;
	float t = 0.0f;
	float timeStart;
	bool isFullAudio = false;
	bool isFadeIn = true;
	// Use this for initialization
	void Start () {
		this.audioSource = GetComponent<AudioSource> ();
		this.t = 0;
	}



	void Update() {
		if (isFullAudio == true) {
			if (isFadeIn) {
				if (Time.time < this.timeStart + 2) {
					// we need to fade music in
					t += Time.deltaTime;
					audioSource.volume = Mathf.Lerp (0, 1, t / 2);
				} 
			} else {
				if (Time.time < this.timeStart + 5) {
					// we need to fade music in
					t += Time.deltaTime;
					audioSource.volume = Mathf.Lerp (1, 0.1f, t / 3);
				} 
			}
		} else {
			if (audioSource.volume != 0 && Time.time > this.timeStart + 2 + 2) {
				// we need to fade music out
				t += Time.deltaTime;
				audioSource.volume = Mathf.Lerp (1, 0, t / 2);

			} else if (audioSource.volume != 1 && Time.time < this.timeStart + 2) {
				// we need to fade music in
				t += Time.deltaTime;
				audioSource.volume = Mathf.Lerp (0, 1, t / 2);
			} 
			else {
				t = 0;
			}
		}
	}
		

	// start the audio of the selected index
	public void playAudio(string audioName) {
		AudioClip audioClip = JourneySingleton.Instance.getCurrentPlace ().getSound ();
		isFadeIn = true;
		isFullAudio = false;
		if (audioSource.clip != audioClip || audioSource.isPlaying == false) {
			audioSource.clip = audioClip;
			audioSource.volume = 0;
			this.t = 0;
			this.timeStart = Time.time;
			audioSource.Play ();
		}
	}

	// start the audio of the selected index
	public void playFullAudio(string audioName) {
		AudioClip audioClip = JourneySingleton.Instance.getCurrentPlace ().getSound ();
		isFullAudio = true;
		isFadeIn = true;
			audioSource.clip = audioClip;
			audioSource.volume = 0;
			this.t = 0;
			this.timeStart = Time.time;
			audioSource.Play ();

	}



	public bool audioFinish() {
		if (audioSource.time >= audioSource.clip.length) {
			return true;
		}
		return false;
	}

	public bool audioIsFinish() {
		if (audioSource.time >= audioSource.clip.length - 5) {
			return true;
		}
		return false;
	}

	public void stop() {
		this.audioSource.Stop ();
	}

	public void fadeOut() {
		isFadeIn = false;
		this.timeStart = Time.time;
		this.t = 0;
	}
}

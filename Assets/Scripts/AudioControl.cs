﻿using UnityEngine;
using System.Collections;


public class AudioControl : MonoBehaviour {
	// the audio source attached to this component
	private AudioSource audioSource;
	float t = 0.0f;
	float timeStart;
	bool isFullAudio = false;
	// Use this for initialization
	void Start () {
		this.audioSource = GetComponent<AudioSource> ();
		this.t = 0;
	}

	void Update() {
		if (isFullAudio == true) {
			if (Time.time < this.timeStart + 2) {
				// we need to fade music in
				t += Time.deltaTime;
				audioSource.volume = Mathf.Lerp (0, 1, t / 2);
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

		isFullAudio = false;
		if (audioSource.clip != audioClip) {
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

	public void stop() {
		this.audioSource.Stop ();
	}
}

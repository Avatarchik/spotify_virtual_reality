﻿using UnityEngine;
using System.Collections;


public class AudioControl : BaseMachine {
	// the audio source attached to this component
	private AudioSource audioSource;
	bool isFullAudio = false;
	protected const string STATE_FADE_IN = "STATE_FADE_IN";
	protected const string STATE_FADE_OUT = "STATE_FADE_OUT";
	protected const string STATE_PLAY = "STATE_PLAY";
	private const float FADE_IN_TIME = 2f;
	private const float FADE_OUT_TIME = 2f;
	private Place currentPlace;


	public AudioControl(): base(false) {

	}

	// Use this for initialization
	void Start () {
		this.audioSource = GetComponent<AudioSource> ();
	}

	void Update() {
		base.Update ();
		switch (this.getState ()) {
		case STATE_FADE_IN:
			if (this.getTimeSinceStateWasSelected () < FADE_IN_TIME) {
				audioSource.volume = Mathf.Lerp (0, 1, this.getTimeSinceStateWasSelected () / FADE_IN_TIME);
			} 
			else if (this.getTimeSinceStateWasSelected () > FADE_IN_TIME + 1) {
				if (isFullAudio) {
					state = STATE_PLAY;
				} else {
					state = STATE_FADE_OUT;
				}
			}
			break;
		case STATE_FADE_OUT:
			if (this.getTimeSinceStateWasSelected () < FADE_OUT_TIME) {
				audioSource.volume = Mathf.Lerp (1, 0, this.getTimeSinceStateWasSelected () / FADE_OUT_TIME);
			} else {
				state = STATE_INITIAL;
			}
			break;
		case STATE_PLAY:
			if (shouldStartFadeOut ()) {
				state = STATE_FADE_OUT;
			}
			break;
		}
	}
		
	/**
	 * Checks when the audio volume should be faded out 
	 */
	private bool shouldStartFadeOut() {
		if (audioSource.time > currentPlace.getSongMaxTime () - FADE_IN_TIME - FADE_OUT_TIME) {
			return true;
		}
		return false;
	}


	/**
	 * start to play a short audio of the selected index
	 */
	public void playAudio() {
		currentPlace = JourneySingleton.Instance.getCurrentPlace ();
		AudioClip audioClip = currentPlace.getSound ();

		state = STATE_FADE_IN;

		isFullAudio = false;
		if (audioSource.clip != audioClip || audioSource.isPlaying == false) {
			audioSource.clip = audioClip;
			audioSource.volume = 0;
			audioSource.Play ();
		}
	}

	/**
	 * starts to play the audio of the selected index (full audio version)
	 */
	public void playFullAudio() {
		AudioClip audioClip = JourneySingleton.Instance.getCurrentPlace ().getSound ();
		isFullAudio = true;
		state = STATE_FADE_IN;

		audioSource.clip = audioClip;
		audioSource.volume = 0;
		audioSource.Play ();
	}

	/**
	 * starts to fade the music out
	 */
	public void fadeOut() {
		state = STATE_FADE_OUT;
	}

	public bool isAudioFadedOut() {
		return audioSource.volume == 0;
	}

	/**
	 * Audio finish?
	 */
	public bool audioFinish() {
		if (audioSource.time >= currentPlace.getSongMaxTime()) {
			return true;
		}
		return false;
	}

	/**
	 * Checks if the current audio is finishing
	 */
	public bool audioIsFinishing() {
		if (audioSource.time >= (currentPlace.getSongMaxTime() - FADE_IN_TIME - FADE_OUT_TIME)) {
			return true;
		}
		return false;
	}

	/**
	 * Stop the current audio
	 */
	public void stop() {
		this.audioSource.Stop ();
	}


}

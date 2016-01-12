using UnityEngine;
using System.Collections;


public class AudioControl : BaseMachine {
	// the audio source attached to this component
	private AudioSource audioSource;
	bool isFullAudio = false;

	// Use this for initialization
	void Start () {
		this.audioSource = GetComponent<AudioSource> ();
	}

	protected const string STATE_FADE_IN = "STATE_FADE_IN";
	protected const string STATE_FADE_OUT = "STATE_FADE_OUT";
	protected const string STATE_PLAY = "STATE_PLAY";

	Place currentPlace;

	private const float FADE_IN_TIME = 2f;
	private const float FADE_OUT_TIME = 2f;

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


		/*if (isFullAudio == true) {
			
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
		}*/
	}
		
	private bool shouldStartFadeOut() {
		if (audioSource.time > currentPlace.getSongMaxTime () - FADE_IN_TIME - FADE_OUT_TIME) {
			return true;
		}
		return false;
	}


	// start the audio of the selected index
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

	// start the audio of the selected index
	public void playFullAudio() {
		AudioClip audioClip = JourneySingleton.Instance.getCurrentPlace ().getSound ();
		isFullAudio = true;
		state = STATE_FADE_IN;

		audioSource.clip = audioClip;
		audioSource.volume = 0;
		audioSource.Play ();

	}

	public void fadeOut() {
		state = STATE_FADE_OUT;
	}

	public bool isAudioFadedOut() {
		return audioSource.volume == 0;
	}

	public bool audioFinish() {
		if (audioSource.time >= audioSource.clip.length) {
			return true;
		}
		return false;
	}

	public bool audioIsFinishing() {
		if (audioSource.time >= (currentPlace.getSongMaxTime() - FADE_IN_TIME - FADE_OUT_TIME)) {
			return true;
		}
		return false;
	}

	public void stop() {
		this.audioSource.Stop ();
	}


}

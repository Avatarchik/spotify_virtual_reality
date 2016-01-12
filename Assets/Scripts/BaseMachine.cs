using UnityEngine;
using System.Collections;

public class BaseMachine : MonoBehaviour {
	protected const string STATE_INITIAL = "STATE_INITIAL";

	protected string state = STATE_INITIAL;
	private string oldState = null;

	private float timeSinceStateWasSelected = 0;
	private float timeWhenStateWasSelected = 0;

	private bool enableDebug = false;


	public BaseMachine(bool enableDebug = false) {
		this.enableDebug = enableDebug;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		if(oldState != state) {
			if (enableDebug) {
				Debug.Log ("[" + this.GetType().Name + "] state= " + state);
			}
			oldState = state;
			timeWhenStateWasSelected = Time.time;
		}
		timeSinceStateWasSelected = Time.time - timeWhenStateWasSelected;
	}

	public float getTimeSinceStateWasSelected() {
		return timeSinceStateWasSelected;
	}

	public float getTimeWhenStateWasSelected() {
		return timeWhenStateWasSelected;
	}

	public string getState() {
		return this.state;
	}

}

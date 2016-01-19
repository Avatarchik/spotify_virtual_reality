using UnityEngine;
using System.Collections;

public class BaseMachine : MonoBehaviour {
	protected const string STATE_INITIAL = "STATE_INITIAL";

	/**
	 * the current machine state
	 */ 
	protected string state = STATE_INITIAL;

	/**
	 * the old machine state
	 */ 
	private string oldState = null;

	/**
	 * How much time since the current staste is selected
	 */ 
	private float timeSinceStateWasSelected = 0;

	/**
	 * Time when the state was selected
	 */ 
	private float timeWhenStateWasSelected = 0;

	/**
	 * Enable debug messages 
	 */
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

    public void reset()
    {
        oldState = null;
    }

	/**
	 * Return how much time since the current staste is selected
	 */ 
	public float getTimeSinceStateWasSelected() {
		return timeSinceStateWasSelected;
	}


	/**
	 * Return time when the state was selected
	 */ 
	public float getTimeWhenStateWasSelected() {
		return timeWhenStateWasSelected;
	}

	/**
	 * Return the current state
	 */ 
	public string getState() {
		return this.state;
	}

}

using UnityEngine;
using System.Collections;

public class CubeAnimation : BaseMachine {

	public float animationSpeed;
	public Vector3 distanceMax;
	public Vector3 distanceMin;
	Vector3 randDistance;
	Vector3 randInit;
	public Vector3 cycleSpeedMin;
	public Vector3 cycleSpeedMax;
	Vector3 cycleSpeedRand;
    public Vector3 finaPosition;
    public Vector3 finaRotation;
   
	public const string STATE_INITIAL_POSITION = "INITIAL_POSITION";
	public const string STATE_RANDOM_MOVE = "RANDOM_MOVE";
	public const string STATE_EXPAND_SPHERIC_MOVE = "EXPAND_SPHERIC_MOVE";
	public const string STATE_FINAL_POSITION = "FINAL_POSITION";
	public const string STATE_FINAL_POSITION_MOVE = "FINAL_POSITION_MOVE";
	public const string STATE_INITIAL_POSITION_MOVE = "FINAL_INITIAL_POSITION_MOVE";

	public float wallFinalToInitialPositionTime = 5;

	public CubeAnimation(): base(false) {
		state = STATE_INITIAL_POSITION;
	}
		
    private Vector3 startPosition;
    private Vector3 startRotation;
    private Vector3 expandingDirection;
    public float expansionSpeed;

    float timeAux;

    // Use this for initialization
    void Start () {
		gameObject.tag = "Walls";

        startPosition = this.transform.position;
        startRotation = this.transform.eulerAngles;


        randInit.x = Random.Range(-1, 1);
		randInit.y = Random.Range(-1, 1);
		randInit.z = Random.Range(-1, 1);

		randDistance.x = Random.Range(distanceMin.x, distanceMax.x);
		randDistance.y = Random.Range(distanceMin.y, distanceMax.y);
		randDistance.z = Random.Range(distanceMin.z, distanceMax.z);

		cycleSpeedRand.x = Random.Range(cycleSpeedMin.x, cycleSpeedMax.x);
		cycleSpeedRand.y = Random.Range(cycleSpeedMin.y, cycleSpeedMax.y);
		cycleSpeedRand.z = Random.Range(cycleSpeedMin.z, cycleSpeedMax.z);
	}

	private void debugPosition() {
		Debug.Log ("Cube: " + this.name + " - Position: " + startPosition.ToString () + " - Rotation: " + startRotation.ToString());
	}

	// Update is called once per frame
	void Update () {
		base.Update ();

		if(Input.GetKey(KeyCode.D)) {
			debugPosition ();
		}
        //        this.transform.position += new Vector3(1,1,1) * Time.deltaTime * animationSpeed;
        
        switch (state)
        {
            case STATE_INITIAL_POSITION:
            {
                this.transform.position = startPosition;
                expandingDirection = Vector3.zero;
				setMovement(STATE_RANDOM_MOVE);
                //setMovement(MovementStatus.EXPAND_SPHERIC_MOVE);
                break;
            }
		case STATE_RANDOM_MOVE:
            {
                Vector3 tmpVector;
                tmpVector.x = Mathf.Sin(randInit.x + (Time.time * cycleSpeedRand.x)) * randDistance.x;
                tmpVector.y = Mathf.Sin(randInit.y + (Time.time * cycleSpeedRand.y)) * randDistance.y;
                tmpVector.z = Mathf.Sin(randInit.z + (Time.time * cycleSpeedRand.z)) * randDistance.z;
                this.transform.position += tmpVector * Time.deltaTime * animationSpeed;
                break;
            }
		case STATE_EXPAND_SPHERIC_MOVE:
            {
                if(expandingDirection == Vector3.zero)
                {
                    expandingDirection = this.transform.position.normalized;
//                    timeAux = Time.time;
                }

// TODO - Calculate angle with expandingDirection vector, then animate the rotation with parametric speed.

                this.transform.position += (Time.deltaTime * expansionSpeed * expandingDirection);

/*
                if((Time.time - timeAux) > 3)
                {
                        //setMovement(MovementStatus.INITIAL_POSITION);
                        this.transform.position = startPosition;
                    }
                else
                {
                    this.transform.position += (Time.deltaTime * expansionSpeed * expandingDirection);
                }
*/
                break;
            }
		case STATE_FINAL_POSITION:
            {
				/*float time = 5;
				if (this.getTimeSinceStateWasSelected () < time) {
					transform.position = Vector3.Lerp(startPosition, finaPosition, this.getTimeSinceStateWasSelected () / time);
				} */

				transform.position = finaPosition;
				//transform.rotation.eulerAngles = finaRotation;

				state = STATE_FINAL_POSITION_MOVE;

                break;
            }
		case STATE_FINAL_POSITION_MOVE:
            {
				if (this.getTimeSinceStateWasSelected () > 5) {
					state = STATE_INITIAL_POSITION_MOVE;
				}
                break;
            }
		case STATE_INITIAL_POSITION_MOVE:
            {


				if (this.getTimeSinceStateWasSelected () < wallFinalToInitialPositionTime) {
					float lerp = Mathfx.Hermite (0, wallFinalToInitialPositionTime, this.getTimeSinceStateWasSelected ()/wallFinalToInitialPositionTime);
					transform.position = Vector3.Lerp (finaPosition, startPosition, lerp/wallFinalToInitialPositionTime);
				} else {
					state = STATE_RANDOM_MOVE;
				}
                break;
            }
        }

		// just a small color test.. remove it
		Renderer r = GetComponent<Renderer>();
		Color newColor = new Color(0, 255, 0);     
		tempo += Time.time;
		//r.material.color = Color.Lerp (r.material.color, newColor, tempo/10000);

	}

	float tempo = 0;

	public void setMovement(string status)
    {
		this.state = status;
    }

	public static void changeAllWallsStatus(string status) {
		GameObject[] walls = GameObject.FindGameObjectsWithTag("Walls");

		foreach (GameObject wall in walls) {
			wall.gameObject.GetComponent<CubeAnimation> ().setMovement (status);
		}
	}
}


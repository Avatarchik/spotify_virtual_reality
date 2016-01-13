using UnityEngine;
using System.Collections;

public class CubeAnimation : MonoBehaviour {

	public float animationSpeed;
	public Vector3 distanceMax;
	public Vector3 distanceMin;
	Vector3 randDistance;
	Vector3 randInit;
	public Vector3 cycleSpeedMin;
	public Vector3 cycleSpeedMax;
	Vector3 cycleSpeedRand;

    public enum MovementStatus{
        INITIAL_POSITION = 0,
        RANDOM_MOVE,
        EXPAND_SPHERIC_MOVE,
        FINAL_POSITION,
        FINAL_POSITION_MOVE,
        INITIAL_POSITION_MOVE,
    }

    static public MovementStatus moveStatus = MovementStatus.INITIAL_POSITION;

    private Vector3 startPosition;
    private Vector3 expandingDirection;
    public float expansionSpeed;

    float timeAux;

    // Use this for initialization
    void Start () {

        startPosition = this.transform.position;
        
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

	// Update is called once per frame
	void Update () {
        //        this.transform.position += new Vector3(1,1,1) * Time.deltaTime * animationSpeed;
        
        switch (moveStatus)
        {
            case MovementStatus.INITIAL_POSITION:
            {
                this.transform.position = startPosition;
                expandingDirection = Vector3.zero;
                setMovement(MovementStatus.RANDOM_MOVE);
                //setMovement(MovementStatus.EXPAND_SPHERIC_MOVE);
                break;
            }
            case MovementStatus.RANDOM_MOVE:
            {
                Vector3 tmpVector;
                tmpVector.x = Mathf.Sin(randInit.x + (Time.time * cycleSpeedRand.x)) * randDistance.x;
                tmpVector.y = Mathf.Sin(randInit.y + (Time.time * cycleSpeedRand.y)) * randDistance.y;
                tmpVector.z = Mathf.Sin(randInit.z + (Time.time * cycleSpeedRand.z)) * randDistance.z;
                this.transform.position += tmpVector * Time.deltaTime * animationSpeed;
                break;
            }
            case MovementStatus.EXPAND_SPHERIC_MOVE:
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
            case MovementStatus.FINAL_POSITION:
            {

                break;
            }
            case MovementStatus.FINAL_POSITION_MOVE:
            {

                break;
            }
            case MovementStatus.INITIAL_POSITION_MOVE:
            {

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

    public static void setMovement(MovementStatus status)
    {
        moveStatus = status;
    }
}


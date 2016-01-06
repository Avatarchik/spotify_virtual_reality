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

	// Use this for initialization
	void Start () {
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

		Vector3 tmpVector;

		tmpVector.x = Mathf.Sin(randInit.x + (Time.time * cycleSpeedRand.x)) * randDistance.x;
		tmpVector.y = Mathf.Sin(randInit.y + (Time.time * cycleSpeedRand.y)) * randDistance.y;
		tmpVector.z = Mathf.Sin(randInit.z + (Time.time * cycleSpeedRand.z)) * randDistance.z;

		this.transform.position += tmpVector * Time.deltaTime * animationSpeed;
	}
}
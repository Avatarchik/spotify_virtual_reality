using UnityEngine;
using System.Collections;

public class CanvasMover : MonoBehaviour {

    public Vector3 initialPosition;
    public Vector3 finalPosition;

    float time = 1;
    float t = 0;
    bool isReverse;
    // Use this for initialization
    void Start () {
        finalPosition = this.gameObject.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        if(t < time)
        {
            t += Time.deltaTime;
            float lerp = Mathfx.Hermite(0, time, t / time);

            if(isReverse)
            {
                this.gameObject.transform.localPosition = Vector3.Lerp(finalPosition, initialPosition, lerp / time);
            }
            else
            {
                this.gameObject.transform.localPosition = Vector3.Lerp(initialPosition, finalPosition, lerp / time);
            }
           
        }
	   
	}

    public void run()
    {
        t = 0;
        isReverse = false;
    }

    public void reverse()
    {
        t = 0;
        isReverse = true;
    }   
}

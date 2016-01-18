using UnityEngine;
using System.Collections;

public class PlayMovieOFf : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

       
        
    }

    public void startMovie()
    {
        Renderer r = GetComponent<Renderer>();
        MovieTexture movie = (MovieTexture)r.material.mainTexture;
        movie.loop = true;
        if (movie.isPlaying == false)
        {
            movie.Play();
        }
        r.enabled = true;
    }

    public void stopMovie()
    {
        Renderer r = GetComponent<Renderer>();
        MovieTexture movie = (MovieTexture)r.material.mainTexture;
        if (movie.isPlaying)
        {
            movie.Stop();
        }
        r.enabled = false;
    }
}

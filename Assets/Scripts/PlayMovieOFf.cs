using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayMovieOFf : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.startMovie();
	}
	
	// Update is called once per frame
	void Update () {

       
        
    }

    public void startMovie()
    {
		RawImage r = GetComponent<RawImage>();
        MovieTexture movie = (MovieTexture)r.mainTexture;
        movie.loop = true;
        if (movie.isPlaying == false)
        {
            movie.Play();
        }
        r.enabled = true;
    }

    public void stopMovie()
    {
		RawImage r = GetComponent<RawImage>();
        MovieTexture movie = (MovieTexture)r.mainTexture;
        if (movie.isPlaying)
        {
            movie.Stop();
        }
        r.enabled = false;
    }
}

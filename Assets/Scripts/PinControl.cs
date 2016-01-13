using UnityEngine;
using System.Collections;

public class PinControl : MonoBehaviour {

	private const int STATE_PIN_IS_OFF = 0;
	private const int STATE_PIN_IS_ON = 1;
	private const int STATE_PIN_IS_TURNING_ON = 2;
	private const int STATE_PIN_IS_TURNING_OFF = 3;
	private const int STATE_PIN_IS_SHINNING = 4;
	private const int STATE_PIN_IS_SHRINKING = 5;

    private const int STATE_PIN_GROW = 6;
    private const int STATE_PIN_SHRINK = 7;

    private int state = STATE_PIN_IS_OFF;

	private float PIN_ON_MID_INTENSITY = 2f;
	private float PIN_ON_SHRINK_INTENSITY = 0.5f;
	private float PIN_ON_HIGH_INTENSITY = 2f;

    private const float fadePinShrink = 1f;
    private const float fadePinShine = 5f;
    private const float fadePinInitlaBlink = 0.25f;
    private const float fadePinStepBlink = 0.03f;

    private float currentPinBlink;

    Fader fader;
    FaderMaterial faderMaterial;
    GameObject lightGameObj;
	Light light;
	AudioSource audioSource;
	AudioClip audioClipPinEnter;
	AudioClip audioClipPinSelected;
    AudioClip audioClipSoundgrow;
    Material materialPin;
    Material materialPinHover;

    GameObject pyramid_1;
    GameObject pyramid_2;
    // Use this for initialization
    void Start () {
		this.lightGameObj = this.gameObject.transform.Find ("Light").gameObject;
		this.light = this.lightGameObj.GetComponent<Light>();
		this.audioSource = this.lightGameObj.GetComponent<AudioSource>();

		audioClipPinEnter = (AudioClip)Resources.Load("Audio/pin_enter", typeof(AudioClip));

		audioClipPinSelected = (AudioClip)Resources.Load("Audio/pin_select_2", typeof(AudioClip));

        audioClipSoundgrow  = (AudioClip)Resources.Load("Audio/soundgrow_grave", typeof(AudioClip));

        materialPin = (Material)Resources.Load("Materials/pin", typeof(Material));
        materialPinHover = (Material)Resources.Load("Materials/pin_hover", typeof(Material));

        this.pyramid_1 = this.gameObject.transform.Find("pin_4/Pyramid_1").gameObject;
        this.pyramid_2 = this.gameObject.transform.Find("pin_4/Pyramid_2").gameObject;
    }

	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATE_PIN_IS_OFF:
			break;
		case STATE_PIN_IS_ON:

			break;
		case STATE_PIN_IS_TURNING_ON:
                if (faderMaterial != null)
                {
                    faderMaterial.update();
                }
			if (fader != null) {
                if (fader.update ()) {
					fader = null;
					state = STATE_PIN_IS_ON;
				}
			}
			break;
		case STATE_PIN_IS_TURNING_OFF:
                if (faderMaterial != null)
                {
                    faderMaterial.update();
                }
                if (fader != null) {
				if (fader.update ()) {
					fader = null;
					state = STATE_PIN_IS_OFF;
				}
			}
			break;
		case STATE_PIN_IS_SHINNING:
			if (fader != null) {
				if (fader.update ()) {
					fader = null;
					state = STATE_PIN_IS_ON;
                        CancelInvoke("FlashLabel");
				}
			}
			break;
            case STATE_PIN_IS_SHRINKING:
                if (fader != null) {
                    if (fader.update ()) {
                        fader = null;
                        internalMakePinShine ();
                    }
                }
                break;
            case STATE_PIN_GROW:
                
                //if(currentPinBlink)
                if (fader != null)
                {
                    faderMaterial.update();
                    if (fader.update())
                    {
                        currentPinBlink = currentPinBlink - fadePinStepBlink;
                        fader = new Fader(light, PIN_ON_HIGH_INTENSITY, PIN_ON_SHRINK_INTENSITY , currentPinBlink);
                        faderMaterial = new FaderMaterial(this.pyramid_1.GetComponent<Renderer>(), this.pyramid_2.GetComponent<Renderer>(), materialPinHover, materialPin, currentPinBlink);
                       
                        state = STATE_PIN_SHRINK;

                        if (currentPinBlink < 0.05f)
                        {
                            state = STATE_PIN_IS_ON;
                            fader = null;
                        }
                    }
                }
                   
                break;

            case STATE_PIN_SHRINK:
                if (fader != null)
                {
                    faderMaterial.update();
                    if (fader.update())
                    {
                        fader = new Fader(light, PIN_ON_SHRINK_INTENSITY, PIN_ON_HIGH_INTENSITY, currentPinBlink);
                        faderMaterial = new FaderMaterial(this.pyramid_1.GetComponent<Renderer>(), this.pyramid_2.GetComponent<Renderer>(), materialPin, materialPinHover, currentPinBlink);
                        this.audioSource.clip = audioClipSoundgrow;
                        this.audioSource.Stop();
                        this.audioSource.Play();
                        state = STATE_PIN_GROW;
                    }
                }
                break;
       }
	}


  
    
    void changePinMaterial()
    {
        Material material = this.pyramid_1.GetComponent<Renderer>().material;
        if (material.name.Contains("hover"))
        {
            this.pyramid_1.GetComponent<Renderer>().material = materialPin;
        }
        else { 
            this.pyramid_1.GetComponent<Renderer>().material = materialPinHover;
        }
    }


    public bool isPinShinning() {
        return this.state == STATE_PIN_IS_SHINNING || this.state == STATE_PIN_IS_SHRINKING || this.state == STATE_PIN_GROW || this.state == STATE_PIN_SHRINK;

    }

	public void turnOnPinLight() {
		fader = new Fader (light, 0, PIN_ON_MID_INTENSITY, 0.3f);

        this.pyramid_1.GetComponent<Renderer>().material = materialPinHover;
        this.pyramid_2.GetComponent<Renderer>().material = materialPinHover;

        state = STATE_PIN_IS_TURNING_ON;
	}

	public void turnOffPinLight() {
		state = STATE_PIN_IS_OFF;
		if (this.light.intensity != 0) {
			fader = new Fader (light, light.intensity, 0, 0.3f);
            faderMaterial = new FaderMaterial(this.pyramid_1.GetComponent<Renderer>(), this.pyramid_2.GetComponent<Renderer>(), materialPinHover, materialPin, 0.3f);

            state = STATE_PIN_IS_TURNING_OFF;
			this.audioSource.Stop ();
		}

        this.pyramid_1.GetComponent<Renderer>().material = materialPin;
        this.pyramid_2.GetComponent<Renderer>().material = materialPin;


    }

    float timeStartPinBlink;

    public void makePinShine() {
        state = STATE_PIN_IS_SHRINKING;
        if (this.light.intensity != 0)
        {
            fader = new Fader(light, light.intensity, PIN_ON_SHRINK_INTENSITY, 1f);
            faderMaterial = new FaderMaterial(this.pyramid_1.GetComponent<Renderer>(), this.pyramid_2.GetComponent<Renderer>(), materialPinHover, materialPin, 1f);
        }
    }

    private void internalMakePinShine() {
        this.audioSource.clip = audioClipSoundgrow;
        this.audioSource.Stop();
        this.audioSource.Play();
        state = STATE_PIN_GROW;
        currentPinBlink = fadePinInitlaBlink;
        fader = new Fader(light, light.intensity, PIN_ON_HIGH_INTENSITY, currentPinBlink);
        faderMaterial = new FaderMaterial(this.pyramid_1.GetComponent<Renderer>(), this.pyramid_2.GetComponent<Renderer>(), materialPin, materialPinHover, currentPinBlink);

        timeStartPinBlink = Time.time;
	}

    private class Fader {
		float t = 0.0f;
		Light l;
		float fadeStart;
		float fadeEnd; 
		float fadeTime;
		public Fader(Light l, float fadeStart, float fadeEnd, float fadeTime) {
			this.l = l;
			this.fadeEnd = fadeEnd;
			this.fadeStart = fadeStart;
			this.fadeTime = fadeTime;
		}

		public bool update() {

			if (t < fadeTime) {
				t += Time.deltaTime;
				l.intensity = Mathf.Lerp(fadeStart, fadeEnd, t / fadeTime);
				return false;
			}

			return true;
		}
	}

    private class FaderMaterial
    {
        float t = 0.0f;
        Renderer r;
        Renderer r2;
        Material material1;
        Material material2;
        float fadeTime;
        public FaderMaterial(Renderer r, Renderer r2, Material material1, Material material2, float fadeTime)
        {

            this.r = r;
            this.r2 = r2;
            this.material1 = material1;
            this.material2 = material2;
            this.fadeTime = fadeTime;
        }

        public bool update()
        {

            if (t < fadeTime)
            {
                t += Time.deltaTime;

                r.material.Lerp(material1, material2, t / fadeTime);
                r2.material.Lerp(material1, material2, t / fadeTime);

                return false;
            }

            return true;
        }
    }

}

using UnityEngine;
using System.Collections;

public class JourneyClientControl : MonoBehaviour {
    private UDPReceive udpReceive;
    private PlaceControl placeControl;
    public CameraChanger cameraChanger;
    public PlayMovieOFf movieOff;
    public PlayMovieOnSpace movieControlGlobe;

    // Use this for initialization
    void Start () {
        this.udpReceive = GetComponent<UDPReceive>();
        this.placeControl = GetComponent<PlaceControl>();

        cameraChanger.changeToStreetView();
        movieOff.startMovie();
    }

    private bool videoIsFadeIn = false;

    private float lastPacketTime;
    private float lastTransitionTime;
    private bool isOnGlobe = true;
    private int journeyCount = 0;
    // Update is called once per frame
    void Update() {
        if(Time.time - lastPacketTime > 5)
        {
            movieOff.startMovie();
        }
        byte[] stream = udpReceive.getLatestUDPPacket();
        if (stream != null && stream.Length != 0)
        {
            lastPacketTime = Time.time;
            UDPPacket packet = new UDPPacket(stream);

            switch (packet.getType())
            {
                case UDPPacket.STREET_VIEW_PACKET:
                    if (packet.getPlaceCode().CompareTo(placeControl.getMaterialName()) != 0)
                    {
                        placeControl.setPlace(packet.getPlaceCode());
                        placeControl.applyMaterial();
                        cameraChanger.updatePublicCameraRotationStreetView(JourneySingleton.Instance.getPlace(packet.getPlaceCode()));
                        lastTransitionTime = Time.time;
                        journeyCount++;
                    }

                    cameraChanger.updateCameraRotation(packet.getQuarternion());
                    movieOff.stopMovie();
                    isOnGlobe = false;
                    break;
                case UDPPacket.GLOBE_PACKET:
                    movieOff.startMovie();
                    isOnGlobe = true;
                    journeyCount = 0;
                    break;
                case UDPPacket.PLACE_TRANSITION_PACKET:
                    movieControlGlobe.fadeIn();
                    videoIsFadeIn = true;
                    break;
            }


            udpReceive.resetLastest();

            if (movieControlGlobe.getState() == PlayMovieOnSpace.STATE_FADED && videoIsFadeIn)
            {
                videoIsFadeIn = false;
                movieControlGlobe.fadeOut();
            }

            float timeToCheck = 24;
            if(journeyCount == 1)
            {
                timeToCheck = 19;
            }

            if ((Time.time - lastTransitionTime) >= timeToCheck && videoIsFadeIn == false && isOnGlobe == false)
            {
                movieControlGlobe.fadeIn();
                videoIsFadeIn = true;
            }

        }
    }
}

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
                    }

                    cameraChanger.updateCameraRotation(packet.getQuarternion());
                    movieOff.stopMovie();
                    break;
                case UDPPacket.GLOBE_PACKET:
                    movieOff.startMovie();
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

        }
    }
}

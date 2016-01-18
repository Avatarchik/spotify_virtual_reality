using UnityEngine;
using System.Collections;

public class JourneyClientControl : MonoBehaviour {
    private UDPReceive udpReceive;
    private PlaceControl placeControl;
    public CameraChanger cameraChanger;
    public PlayMovieOFf movieOff;

    // Use this for initialization
    void Start () {
        this.udpReceive = GetComponent<UDPReceive>();
        this.placeControl = GetComponent<PlaceControl>();

        cameraChanger.changeToStreetView();
        movieOff.startMovie();
    }

    // Update is called once per frame
    void Update() {
        byte[] stream = udpReceive.getLatestUDPPacket();
        if (stream != null && stream.Length != 0)
        {
            UDPPacket packet = new UDPPacket(stream);

             switch (packet.getType())
            {
                case UDPPacket.STREET_VIEW_PACKET:
                    if (packet.getPlaceCode().CompareTo(placeControl.getMaterialName()) != 0)
                    {
                        placeControl.setPlace(packet.getPlaceCode());
                        placeControl.applyMaterial();
                    }

                    cameraChanger.updateCameraRotation(packet.getQuarternion());
                    movieOff.stopMovie();
                    break;
                case UDPPacket.GLOBE_PACKET:
                    movieOff.startMovie();
                    break;
            }

       
            udpReceive.resetLastest();
    }
    

    }
}

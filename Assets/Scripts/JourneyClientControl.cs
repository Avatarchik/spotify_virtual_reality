using UnityEngine;
using System.Collections;

public class JourneyClientControl : MonoBehaviour {
    private UDPReceive udpReceive;
    private PlaceControl placeControl;
    public CameraChanger cameraChanger;

    // Use this for initialization
    void Start () {
        this.udpReceive = GetComponent<UDPReceive>();
        this.placeControl = GetComponent<PlaceControl>();

        cameraChanger.changeToStreetView();
    }

    // Update is called once per frame
    void Update() {
        byte[] stream = udpReceive.getLatestUDPPacket();
        if (stream != null && stream.Length != 0)
        {
            UDPPacket packet = new UDPPacket(stream);

            Debug.Log("udpReceive: " + stream);
            switch (packet.getType())
            {
                case UDPPacket.STREET_VIEW_PACKET:
                    if (packet.getPlaceCode().CompareTo(placeControl.getMaterialName()) != 0)
                    {
                        placeControl.setPlace(packet.getPlaceCode());
                        placeControl.applyMaterial();
                    }

                    cameraChanger.updateCameraRotation(packet.getQuarternion());
                    break;
                case UDPPacket.GLOBE_PACKET:

                    break;
            }

       
            udpReceive.resetLastest();
    }
    

    }
}

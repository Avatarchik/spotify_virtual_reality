using UnityEngine;
using System.Collections;

public class JourneyClientControl : MonoBehaviour {
    private UDPReceive udpReceive;
    private PlaceControl placeControl;
    public CameraChanger cameraChanger;

    // Use this for initialization
    void Start () {
        udpReceive.enabled = true;

        this.udpReceive = GetComponent<UDPReceive>();
        this.placeControl = GetComponent<PlaceControl>();

        cameraChanger.changeToStreetView();
    }
	
	// Update is called once per frame
	void Update () {
        string stream = udpReceive.getLatestUDPPacket();
        switch (UDPPacket.readPacket(stream))
        {
            case UDPPacket.GO_TO_STREET_VIEW:
                break;
            case UDPPacket.UPDATE_CAMERA:
                cameraChanger.updateCameraRotation(UDPPacket.readCameraUpdate(stream));
                break;
        }
    

    }
}

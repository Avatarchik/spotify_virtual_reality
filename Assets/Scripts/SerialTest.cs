using UnityEngine;
using System.Collections;

public class SerialTest : MonoBehaviour {
    SerialController serialController;

	// Use this for initialization
	void Start () {
        serialController = new SerialController();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.O))
        {
            Debug.Log("openComPort");
            serialController.openComPort(SerialController.ComPorts.FURNITURE_COM_PORT_1);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("testSerial");
            //serialController.setColour(SerialController.ComPorts.FURNITURE_COM_PORT_1, "R100G150B255");
           // serialController.setColourWithFadeAndDelay(SerialController.ComPorts.FURNITURE_COM_PORT_1, "R155G185B224", "R010G040B100", "12345", "07890");
            serialController.glitch(SerialController.ComPorts.FURNITURE_COM_PORT_1, "R123G456B789", "12345");
        }
	}
}

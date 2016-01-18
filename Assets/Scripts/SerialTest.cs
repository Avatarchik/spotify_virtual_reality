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
            serialController.testSerial(SerialController.ComPorts.FURNITURE_COM_PORT_1);
        }
	}
}

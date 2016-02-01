using UnityEngine;
using System.Collections;

public class SerialTest : MonoBehaviour {
    SerialController serialController;

	// Use this for initialization
	void Start () {
        serialController = new SerialController();

    }

    bool open = false;
	
	// Update is called once per frame
	void Update () {
        SerialController.ComPorts port = SerialController.ComPorts.FURNITURE_COM_PORT_5;
        if (Input.GetKeyUp(KeyCode.O))
        {
            Debug.Log("openComPort");
            serialController.openComPort(port);
            open = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log("testSerial");
            serialController.setColour(port, "R150G150B150");
            //serialController.setColourWithFadeAndDelay(SerialController.ComPorts.FURNITURE_COM_PORT_1, "R255G000B000", "R000G255B000", "10000", "00000");
            //serialController.glitch(SerialController.ComPorts.FURNITURE_COM_PORT_1, "R255G000B000", "05000");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            Debug.Log("testSerial");
            serialController.setColour(port, "R150G000B000");
            //serialController.setColourWithFadeAndDelay(SerialController.ComPorts.FURNITURE_COM_PORT_1, "R255G000B000", "R000G255B000", "10000", "00000");
            //serialController.glitch(SerialController.ComPorts.FURNITURE_COM_PORT_1, "R255G000B000", "05000");
        }
        if (open) { 
           //cycle();
        }

    }

    string[] colorsGlobe = new string[3] { "#ff0000", "#00ff00", "#0000ff" };
    int currentColorGlobe = 0;
    float timeOnColor = 3.1f;
    float currentTimeOnColor = 0;
    void cycle()
    {
        if (currentTimeOnColor > timeOnColor && Input.GetKeyUp(KeyCode.G))
        {
            int previousColor = currentColorGlobe;
            currentTimeOnColor = 0;

            currentColorGlobe++;
            if (currentColorGlobe >= 3)
            {
                currentColorGlobe = 0;
            }
            Debug.Log("From color: " + Utils.toSerialColor(colorsGlobe[previousColor]) + " to color " + Utils.toSerialColor(colorsGlobe[currentColorGlobe]));
            this.serialController.setColourWithFadeAndDelay(SerialController.ComPorts.FURNITURE_COM_PORT_1, Utils.toSerialColor(colorsGlobe[previousColor]), Utils.toSerialColor(colorsGlobe[currentColorGlobe]), "03000", "00000");
        }
        else
        {
            currentTimeOnColor += Time.deltaTime;
        }
    }
}

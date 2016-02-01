using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class SerialController
{

    public enum ComPorts
    {
        FURNITURE_COM_PORT_1 = 0,
        FURNITURE_COM_PORT_2,
        FURNITURE_COM_PORT_3,
        FURNITURE_COM_PORT_4,
        FURNITURE_COM_PORT_5
    }

    int numComPorts = 5;
    public string[] furnitureComPortName = { "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8" };
    SerialPort[] furnitureComPort = new SerialPort[6];
    int baudRate = 9600;

    public bool openComPort(ComPorts port)
    {
        int portIndex = (int)port;
        if ((portIndex < 0) || (portIndex >= numComPorts))
        {
            Debug.Log("furnitureComPort[" + portIndex + "] does not exists. Fail on openComPort method.");
            return false;
        }
        int numPortsAvailable = System.IO.Ports.SerialPort.GetPortNames().Length;
        int i;
        for (i = 0; i < numPortsAvailable; i++) // Check if port is available
        {
            if (System.IO.Ports.SerialPort.GetPortNames()[i].Equals(furnitureComPortName[portIndex]))
                break;
        }
        if (i == numPortsAvailable)
        {
            Debug.Log("Port name " + furnitureComPortName[portIndex] + " does not exists. Fail on openComPort method.");
            return false;
        }
        try
        {
            // TODO - Test this overload construction, if fails, set all propreties by hand and only open, dont use "new".
            furnitureComPort[portIndex] = new SerialPort(furnitureComPortName[portIndex], baudRate, Parity.None, 8, StopBits.None);
            furnitureComPort[portIndex].Open();
            Debug.Log("open ok furnitureComPort[" + portIndex + "] (" + furnitureComPortName[portIndex] + ")");
        }
        catch (System.Exception ex)
        {
            // Handle exception
            Debug.Log("Unable to open furnitureComPort[" + portIndex + "] (" + furnitureComPortName[portIndex] + "): " + ex.Message);
            return false;
        }
        return true;
    }

    public bool closeComPort(ComPorts port)
    {
        int portIndex = (int)port;
        if ((portIndex < 0) || (portIndex >= numComPorts))
        {
            Debug.Log("furnitureComPort[" + portIndex + "] does not exists. Fail on closeComPort method.");
            return false;
        }
        if (furnitureComPort[portIndex].IsOpen)
        {
            try
            {
                furnitureComPort[portIndex].Close();
            }
            catch (System.Exception ex)
            {
                // Handle exception
                Debug.Log("Unable to close furnitureComPort[" + portIndex + "]: " + ex.Message);
                return false;
            }
        }
        else
        {
            Debug.Log("furnitureComPort[" + portIndex + "] was not opened.");
            return false;
        }
        return true;

    }


    public bool testSerial(ComPorts port)
    {
        int portIndex = (int)port;
        if ((portIndex < 0) || (portIndex >= numComPorts))
        {
            Debug.Log("furnitureComPort[" + portIndex + "] does not exists. Fail on testSerial method.");
            return false;
        }
        try
        {
            furnitureComPort[portIndex].Write("0");
            furnitureComPort[portIndex].Write("F");
        }
        catch (System.Exception ex)
        {
            // Handle exception
            Debug.Log("Unable to write on furnitureComPort[" + portIndex + "] on testSerial method: " + ex.Message);
            return false;
        }
        return true;


    }

    public bool setColour(ComPorts port, string rgbColor)
    {
        int portIndex = (int)port;
        if ((portIndex < 0) || (portIndex >= numComPorts))
        {
            Debug.Log("furnitureComPort[" + portIndex + "] does not exists. Fail on setColour method.");
            return false;
        }
        try
        {
            furnitureComPort[portIndex].Write("1");
            furnitureComPort[portIndex].Write(rgbColor);
            furnitureComPort[portIndex].Write("F");
        }
        catch (System.Exception ex)
        {
            // Handle exception
            Debug.Log("Unable to write on furnitureComPort[" + portIndex + "] on setColour method: " + ex.Message);
            return false;
        }
        return true;


    }

    public bool setColourWithFadeAndDelay(ComPorts port, string rgbColorInit, string rgbColorEnd, string fadeTime, string delayTime)
    {
        int portIndex = (int)port;
        if ((portIndex < 0) || (portIndex >= numComPorts))
        {
            Debug.Log("furnitureComPort[" + portIndex + "] does not exists. Fail on setColourWithFadeAndDelay method.");
            return false;
        }
        try
        {
            furnitureComPort[portIndex].Write("2");
            furnitureComPort[portIndex].Write(rgbColorInit);
            furnitureComPort[portIndex].Write(rgbColorEnd);
            furnitureComPort[portIndex].Write(fadeTime.ToString());
            furnitureComPort[portIndex].Write(delayTime.ToString());
            furnitureComPort[portIndex].Write("F");
        }
        catch (System.Exception ex)
        {
            // Handle exception
            Debug.Log("Unable to write on furnitureComPort[" + portIndex + "] on setColourWithFadeAndDelay method: " + ex.Message);
            return false;
        }
        return true;

    }

    public bool glitch(ComPorts port, string rgbColor, string glitchTime)
    {
        int portIndex = (int)port;
        if ((portIndex < 0) || (portIndex >= numComPorts))
        {
            Debug.Log("furnitureComPort[" + portIndex + "] does not exists. Fail on glitch method.");
            return false;
        }
        try
        {
            furnitureComPort[portIndex].Write("3");
            furnitureComPort[portIndex].Write(rgbColor);
            furnitureComPort[portIndex].Write(glitchTime.ToString());
            furnitureComPort[portIndex].Write("F");
        }
        catch (System.Exception ex)
        {
            // Handle exception
            Debug.Log("Unable to write on furnitureComPort[" + portIndex + "] on glitch method: " + ex.Message);
            return false;
        }
        return true;
    }

    public void openAll()
    {
        foreach (ComPorts port in Enum.GetValues(typeof(ComPorts)))
        {
           this.openComPort(port);
        }
    }

    public void glitchAll(string rgbColor, string glitchTime)
    {
        foreach (ComPorts port in Enum.GetValues(typeof(ComPorts)))
        {
            //glitch(port, rgbColor, glitchTime);
        }
    }

    public void setColourWithFadeAndDelayAll(string rgbColorInit, string rgbColorEnd, string fadeTime, string delayTime)
    {
        foreach (ComPorts port in Enum.GetValues(typeof(ComPorts)))
        {
            //setColourWithFadeAndDelay(port, rgbColorInit, rgbColorEnd, fadeTime, delayTime);
        }

    }


    public void setColourAll(string rgbColor)
    {
        foreach (ComPorts port in Enum.GetValues(typeof(ComPorts)))
        {
            
            setColour(port, rgbColor);
        }
    }
}

     
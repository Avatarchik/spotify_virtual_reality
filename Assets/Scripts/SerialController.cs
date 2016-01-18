using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class SerialController
{

    public enum ComPorts
    {
        FURNITURE_COM_PORT_1 = 0,
        FURNITURE_COM_PORT_2,
        FURNITURE_COM_PORT_3,
        FURNITURE_COM_PORT_4,
        FURNITURE_COM_PORT_5,
        FURNITURE_COM_PORT_6,
        FURNITURE_COM_PORT_7
    }

    int numComPorts = 7;
    public string[] furnitureComPortName = {"COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7"};
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
        }
        catch (System.Exception ex)
        {
            // Handle exception
            Debug.Log("Unable to open furnitureComPort[" + portIndex + "] (" + furnitureComPortName[portIndex] + "): " + ex.Message);
            return false;
        }
        return true;

        //switch (port)
        //{
        //    case SerialController.ComPorts.TOTEN_COM_PORT:
        //    {
        //        int i = 0;
        //        for (i = 0; i < numPortsAvailable; i++) // Check if port is available
        //            {
        //                if (System.IO.Ports.SerialPort.GetPortNames()[i].Equals(totenComPortName))
        //                    break;
        //        }
        //        if(i == numPortsAvailable)
        //        {
        //            return false;
        //        }
        //        totenComPort = new SerialPort(totenComPortName, baudRate, Parity.None, 8, StopBits.None);
        //        try
        //        {
        //            totenComPort.Open();
        //        }
        //        catch (System.Exception ex)
        //        {
        //                // Handle exception
        //                Debug.Log("Unable to open totenComPort!: " + ex.Message);
        //                //System.Console.Error.WriteLine("Unable to open totenComPort! %s", ex.Message);
        //                return false;
        //        }
        //        return true;
        //    }
        //    case SerialController.ComPorts.FURNITURE_COM_PORT:
        //    {
        //        int i = 0;
        //        for (i = 0; i < numPortsAvailable; i++) // Check if port is available
        //        {
        //            if (System.IO.Ports.SerialPort.GetPortNames().Equals(furnitureComPort))
        //                break;
        //        }
        //        if (i == numPortsAvailable)
        //        {
        //            return false;
        //        }
        //        totenComPort = new SerialPort(furnitureComPortName, baudRate, Parity.None, 8, StopBits.None);
        //        try
        //        {
        //            totenComPort.Open();
        //        }
        //        catch (System.Exception ex)
        //        {
        //            // Handle exception
        //            Debug.Log("Unable to open furnitureComPort!: " + ex.Message);
        //            //System.Console.Error.WriteLine("Unable to open furnitureComPort! %s", ex.Message);
        //            return false;
        //        }
        //        return true;
        //    }
        //}

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

        //switch (port)
        //{
        //    case SerialController.ComPorts.TOTEN_COM_PORT:
        //    {
        //        if(totenComPort.IsOpen)
        //        {
        //            try
        //            {
        //                totenComPort.Close();
        //            }
        //            catch (System.Exception ex)
        //            {
        //                // Handle exception
        //                System.Console.Error.WriteLine("Unable to close totenComPort! %s", ex.Message);
        //                return false;
        //            }
        //        }
        //        return true;
        //    }
        //    case SerialController.ComPorts.FURNITURE_COM_PORT:
        //    {
        //        if (furnitureComPort.IsOpen)
        //        {
        //            try
        //            {
        //                furnitureComPort.Close();
        //            }
        //            catch (System.Exception ex)
        //            {
        //                // Handle exception
        //                System.Console.Error.WriteLine("Unable to close furnitureComPort! %s", ex.Message);
        //                return false;
        //            }
        //        }
        //        return true;
        //    }
        //}
        //return false;

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

        //switch (port)
        //{
        //    case SerialController.ComPorts.TOTEN_COM_PORT:
        //        {
        //            try
        //            {
        //                totenComPort.Write("0");
        //                totenComPort.Write("F");
        //            }
        //            catch (System.Exception ex)
        //            {
        //                // Handle exception
        //                Debug.Log("Unable to write on totenComPort on testSerial method!");
        //                return false;
        //            }
        //            return true;
        //        }
        //    case SerialController.ComPorts.FURNITURE_COM_PORT:
        //        {
        //            try
        //            {
        //                totenComPort.Write("0");
        //                totenComPort.Write("F");
        //            }
        //            catch (System.Exception ex)
        //            {
        //                // Handle exception
        //                System.Console.Error.WriteLine("Unable to write on furnitureComPort on testSerial method! %s", ex.Message);
        //                return false;
        //            }
        //            return true;
        //        }
        //    default:
        //        {
        //            System.Console.Error.WriteLine("Wrong parameters on setColour method!");
        //            return false;
        //        }
        //}

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

        //switch (port)
        //{
        //    case SerialController.ComPorts.TOTEN_COM_PORT:
        //    {
        //        try
        //        {
        //            totenComPort.Write("1");
        //            totenComPort.Write("F");
        //            // totenComPort.Write(rgbColor);
        //            }
        //        catch (System.Exception ex)
        //        {
        //            // Handle exception
        //            Debug.Log("Unable to write on totenComPort on setColour method!");
        //            //System.Console.Error.WriteLine("Unable to write on totenComPort on setColour method! %s", ex.Message);
        //            return false;
        //        }
        //        return true;
        //    }
        //    case SerialController.ComPorts.FURNITURE_COM_PORT:
        //    {
        //        try
        //        {
        //            furnitureComPort.Write("1");
        //            furnitureComPort.Write(rgbColor);
        //        }
        //        catch (System.Exception ex)
        //        {
        //            // Handle exception
        //            System.Console.Error.WriteLine("Unable to write on furnitureComPort on setColour method! %s", ex.Message);
        //            return false;
        //        }
        //        return true;
        //    }
        //    default:
        //    {
        //    System.Console.Error.WriteLine("Wrong parameters on setColour method!");
        //    return false;
        //    }
        //}

    }

    public bool setColourWithFadeAndDelay(ComPorts port, string rgbColorInit, string rgbColorEnd, Time fadeTime, Time delayTime)
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



        //switch (port)
        //{
        //    case SerialController.ComPorts.TOTEN_COM_PORT:
        //    {
        //        try
        //        {
        //            totenComPort.Write("2");
        //            totenComPort.Write(rgbColorInit);
        //            totenComPort.Write(rgbColorEnd);
        //            totenComPort.Write(fadeTime.ToString());
        //            totenComPort.Write(delayTime.ToString());
        //        }
        //        catch (System.Exception ex)
        //        {
        //            // Handle exception
        //            System.Console.Error.WriteLine("Unable to write on totenComPort on setColourWithFade method! %s", ex.Message);
        //            return false;
        //        }
        //        return true;
        //    }
        //    case SerialController.ComPorts.FURNITURE_COM_PORT:
        //    {
        //        try
        //        {
        //            furnitureComPort.Write("2");
        //            furnitureComPort.Write(rgbColorInit);
        //            furnitureComPort.Write(rgbColorEnd);
        //            furnitureComPort.Write(fadeTime.ToString());
        //            furnitureComPort.Write(delayTime.ToString());
        //        }
        //        catch (System.Exception ex)
        //        {
        //            // Handle exception
        //            System.Console.Error.WriteLine("Unable to write on furnitureComPort on setColourWithFade method! %s", ex.Message);
        //            return false;
        //        }
        //        return true;
        //    }
        //    default:
        //    {
        //        System.Console.Error.WriteLine("Wrong parameters on setColour method!");
        //        return false;
        //    }
        //}
    }

    public bool glitch(ComPorts port, string rgbColor, Time glitchTime)
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
}


        //    switch (port)
        //    {
        //        case SerialController.ComPorts.TOTEN_COM_PORT:
        //        {
        //            try
        //            {
        //                totenComPort.Write("3");
        //                totenComPort.Write(glitchTime.ToString());
        //            }
        //            catch (System.Exception ex)
        //            {
        //                // Handle exception
        //                System.Console.Error.WriteLine("Unable to write on totenComPort on setColourWithFade method! %s", ex.Message);
        //                return false;
        //            }
        //            return true;
        //        }
        //        case SerialController.ComPorts.FURNITURE_COM_PORT:
        //        {
        //            try
        //            {
        //                furnitureComPort.Write("3");
        //                furnitureComPort.Write(glitchTime.ToString());
        //            }
        //            catch (System.Exception ex)
        //            {
        //                // Handle exception
        //                System.Console.Error.WriteLine("Unable to write on furnitureComPort on setColourWithFade method! %s", ex.Message);
        //                return false;
        //            }
        //            return true;
        //        }
        //        default:
        //        {
        //            System.Console.Error.WriteLine("Wrong parameters on setColour method!");
        //            return false;
        //        }
        //    }
        //}
        
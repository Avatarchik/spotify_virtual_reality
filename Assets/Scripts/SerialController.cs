using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class SerialController {

    public enum ComPorts
    {
        TOTEN_COM_PORT = 0,
        FURNITURE_COM_PORT
    }

    public string totenComPortName = "COM1";
    public string furnitureComPortName = "COM2";
    public int baudRate = 9600;

    SerialPort totenComPort;
    SerialPort furnitureComPort;

    public bool openComPort(ComPorts port)
    {
        int numPortsAvailable = System.IO.Ports.SerialPort.GetPortNames().Length;
        switch (port)
        {
            case SerialController.ComPorts.TOTEN_COM_PORT:
            {
                int i = 0;
                for (i = 0; i < numPortsAvailable; i++) // Check if port is available
                    {
                        if (System.IO.Ports.SerialPort.GetPortNames().Equals(totenComPortName))
                            break;
                }
                if(i == numPortsAvailable)
                {
                    return false;
                }
                totenComPort = new SerialPort(totenComPortName, baudRate, Parity.None, 8, StopBits.None);
                try
                {
                    totenComPort.Open();
                }
                catch (System.Exception ex)
                {
                        // Handle exception
                        System.Console.Error.WriteLine("Unable to open totenComPort! %s", ex.Message);
                        return false;
                }
                return true;
            }
            case SerialController.ComPorts.FURNITURE_COM_PORT:
            {
                int i = 0;
                for (i = 0; i < numPortsAvailable; i++) // Check if port is available
                {
                    if (System.IO.Ports.SerialPort.GetPortNames().Equals(furnitureComPort))
                        break;
                }
                if (i == numPortsAvailable)
                {
                    return false;
                }
                totenComPort = new SerialPort(furnitureComPortName, baudRate, Parity.None, 8, StopBits.None);
                try
                {
                    totenComPort.Open();
                }
                catch (System.Exception ex)
                {
                    // Handle exception
                    System.Console.Error.WriteLine("Unable to open furnitureComPort! %s", ex.Message);
                    return false;
                }
                return true;
            }
        }
        return false;
    }

    public bool closeComPort(ComPorts port)
    {
        switch (port)
        {
            case SerialController.ComPorts.TOTEN_COM_PORT:
            {
                if(totenComPort.IsOpen)
                {
                    try
                    {
                        totenComPort.Close();
                    }
                    catch (System.Exception ex)
                    {
                        // Handle exception
                        System.Console.Error.WriteLine("Unable to close totenComPort! %s", ex.Message);
                        return false;
                    }
                }
                return true;
            }
            case SerialController.ComPorts.FURNITURE_COM_PORT:
            {
                if (furnitureComPort.IsOpen)
                {
                    try
                    {
                        furnitureComPort.Close();
                    }
                    catch (System.Exception ex)
                    {
                        // Handle exception
                        System.Console.Error.WriteLine("Unable to close furnitureComPort! %s", ex.Message);
                        return false;
                    }
                }
                return true;
            }
        }
        return false;
    }

    public bool setColour(ComPorts port, string rgbColor)
    {
        switch (port)
        {
            case SerialController.ComPorts.TOTEN_COM_PORT:
            {
                try
                {
                    totenComPort.Write("1");
                    totenComPort.Write(rgbColor);
                }
                catch (System.Exception ex)
                {
                    // Handle exception
                    System.Console.Error.WriteLine("Unable to write on totenComPort on setColour method! %s", ex.Message);
                    return false;
                }
                return true;
            }
            case SerialController.ComPorts.FURNITURE_COM_PORT:
            {
                try
                {
                    furnitureComPort.Write("1");
                    furnitureComPort.Write(rgbColor);
                }
                catch (System.Exception ex)
                {
                    // Handle exception
                    System.Console.Error.WriteLine("Unable to write on furnitureComPort on setColour method! %s", ex.Message);
                    return false;
                }
                return true;
            }
            default:
            {
            System.Console.Error.WriteLine("Wrong parameters on setColour method!");
            return false;
            }
        }
    }

    public bool setColourWithFadeAndDelay(ComPorts port, string rgbColorInit, string rgbColorEnd, Time fadeTime, Time delayTime)
    {
        switch (port)
        {
            case SerialController.ComPorts.TOTEN_COM_PORT:
            {
                try
                {
                    totenComPort.Write("2");
                    totenComPort.Write(rgbColorInit);
                    totenComPort.Write(rgbColorEnd);
                    totenComPort.Write(fadeTime.ToString());
                    totenComPort.Write(delayTime.ToString());
                }
                catch (System.Exception ex)
                {
                    // Handle exception
                    System.Console.Error.WriteLine("Unable to write on totenComPort on setColourWithFade method! %s", ex.Message);
                    return false;
                }
                return true;
            }
            case SerialController.ComPorts.FURNITURE_COM_PORT:
            {
                try
                {
                    furnitureComPort.Write("2");
                    furnitureComPort.Write(rgbColorInit);
                    furnitureComPort.Write(rgbColorEnd);
                    furnitureComPort.Write(fadeTime.ToString());
                    furnitureComPort.Write(delayTime.ToString());
                }
                catch (System.Exception ex)
                {
                    // Handle exception
                    System.Console.Error.WriteLine("Unable to write on furnitureComPort on setColourWithFade method! %s", ex.Message);
                    return false;
                }
                return true;
            }
            default:
            {
                System.Console.Error.WriteLine("Wrong parameters on setColour method!");
                return false;
            }
        }
    }

    public bool glitch(ComPorts port, Time glitchTime)
    {
        switch (port)
        {
            case SerialController.ComPorts.TOTEN_COM_PORT:
            {
                try
                {
                    totenComPort.Write("3");
                    totenComPort.Write(glitchTime.ToString());
                }
                catch (System.Exception ex)
                {
                    // Handle exception
                    System.Console.Error.WriteLine("Unable to write on totenComPort on setColourWithFade method! %s", ex.Message);
                    return false;
                }
                return true;
            }
            case SerialController.ComPorts.FURNITURE_COM_PORT:
            {
                try
                {
                    furnitureComPort.Write("3");
                    furnitureComPort.Write(glitchTime.ToString());
                }
                catch (System.Exception ex)
                {
                    // Handle exception
                    System.Console.Error.WriteLine("Unable to write on furnitureComPort on setColourWithFade method! %s", ex.Message);
                    return false;
                }
                return true;
            }
            default:
            {
                System.Console.Error.WriteLine("Wrong parameters on setColour method!");
                return false;
            }
        }
    }
}

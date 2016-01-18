using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class UDPPacket
    {
    public const int INIT = 0x00;
    public const int GO_TO_STREET_VIEW = 0x01;
    public const int LOCK_SCREEN = 0x02;
    public const int SHOW_MOVIE = 0x03;
    public const int UPDATE_CAMERA = 0x04;

    public static int readPacket(string stream)
    {
        return Convert.ToInt32(stream.Split('!')[0]);
    }

    public static string createPacket(int code, string stream)
    {
        return code + "!" + stream;
    }

    public static string createGoToStreetView(string code)
    {
        return createPacket(GO_TO_STREET_VIEW, code);
    }

    public static string getStreetViewCode(string stream)
    {
        return stream;
    }

    //float rotationX, float rotationY, float rotationZ
    public static string createUpdateCamera(Quaternion cameraRotation)
    {
        return createPacket(UPDATE_CAMERA, cameraRotation.x + "|" + cameraRotation.y + "|" + cameraRotation.z + "|" + cameraRotation.z);
    }

    public static Quaternion readCameraUpdate(string stream)
    {
        Quaternion cameraRotation = new Quaternion();

        string[] streams = stream.Split('|');

        cameraRotation.x = Convert.ToSingle(streams[0]);
        cameraRotation.z = Convert.ToSingle(streams[1]);
        cameraRotation.z = Convert.ToSingle(streams[2]);
        cameraRotation.z = Convert.ToSingle(streams[3]);
        return cameraRotation;
    }
}
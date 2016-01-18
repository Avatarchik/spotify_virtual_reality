using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class UDPPacket
    {
    public const int INIT = 0x00;
    public const int STREET_VIEW_PACKET = 0x01;
    public const int GLOBE_PACKET = 0x02;


    MemoryStream stream;
    BinaryWriter streamWriter;
    BinaryReader streamReader;

    int type;
    Quaternion quaternion;
    string placeCode;

    public int getType()
    {
        return type;
    }

    public UDPPacket(int type, Quaternion quaternion, string placeCode)
    {
        stream = new MemoryStream();
        streamWriter = new BinaryWriter(stream);
        streamWriter.Write(type);

        this.type = type;
        this.quaternion = quaternion;
        this.placeCode = placeCode;

        this.putQuarternion(quaternion);
        this.putPlaceCode(placeCode);
    }

    public string getPlaceCode()
    {
        return placeCode;
    }

    public UDPPacket(byte[] bytes)
    {
        stream = new MemoryStream(bytes);
        streamReader = new BinaryReader(stream);

        this.type = streamReader.ReadInt32();
        this.quaternion = new Quaternion(streamReader.ReadSingle(), streamReader.ReadSingle(), streamReader.ReadSingle(), streamReader.ReadSingle());
        this.placeCode = streamReader.ReadString();
    }

    private void putQuarternion(Quaternion quarternion) 
    {
        streamWriter.Write(quarternion.x);
        streamWriter.Write(quarternion.y);
        streamWriter.Write(quarternion.z);
        streamWriter.Write(quarternion.w);
    }

    public Quaternion getQuarternion()
    {

        return quaternion; 
    }

    private void putPlaceCode(String code)
    {
        streamWriter.Write(code);
    }

    public byte[] getByteArray()
    {

        streamWriter.Flush();
        return stream.ToArray();
    }

}
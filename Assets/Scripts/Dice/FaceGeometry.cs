using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FaceGeometry
{
    public FaceGeometry(float X, float Y, float Z, float Rx, float Ry, float Rz, float Rw)
    {
        x = X;
        y = Y;
        z = Z;
        rx = Rx;
        ry = Ry;
        rz = Rz;
        rw = Rw;
    }

    public float x;
    public float y;
    public float z;

    public float rx;
    public float ry;
    public float rz;
    public float rw;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Dice/D20")]
public class D20Dice : Dice
{
    protected override void GenerateFacesPlaces()
    {
        base.GenerateFacesPlaces();
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.000000f, 0.000000f, 0.000000f, 1.000000f)); //1
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.809045f, 0.467047f, -0.356810f, 0.000000f)); //2
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.000043f, 0.577331f, 0.755795f, 0.308971f)); //3
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, -0.308972f, 0.755795f, -0.577331f, 0.000042f)); //4
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, -0.000010f, 0.577368f, -0.645485f, -0.499997f)); //5
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, -0.499990f, 0.645503f, -0.288661f, 0.500012f)); //6
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.309018f, -0.178385f, -0.934178f, -0.000013f)); //7
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.809007f, 0.467086f, 0.178425f, -0.309038f)); //9
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.000019f, 0.577354f, -0.110253f, 0.809017f)); //9
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, -0.309002f, -0.755756f, -0.288697f, 0.500008f)); //10
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, -0.000010f, -0.577368f, 0.645485f, -0.499998f)); //11
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.309034f, -0.755754f, -0.288698f, 0.499991f)); //12
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, -0.000002f, 0.356847f, -0.934165f, -0.000004f)); //13
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, -0.809024f, -0.467087f, -0.178422f, -0.308997f)); //14
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.000025f, -0.577354f, 0.110244f, 0.809019f)); //15
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.309009f, -0.755753f, -0.288692f, -0.500011f)); //16
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.499989f, -0.288656f, -0.645504f, -0.500017f)); //17
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, -0.500018f, 0.645503f, -0.288656f, -0.499989f)); //18
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.308997f, -0.178422f, 0.467086f, -0.809025f)); //19
        faces.Add(new FaceGeometry(0.0000000f, 0.0000000f, 0.0000000f, 0.499997f, 0.866030f, 0.000023f, -0.000013f)); //20
    }
}

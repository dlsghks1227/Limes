using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECivElement
{
    EMPTY   = 0,
    DUMMY   = 1,
    COMMAND = 2,
    STORAGE = 3,
    BARRACK = 4,
    AIRBARRACK = 5,
}

public enum ECivShape
{
    CIRCLE,
    RECTANGLE,
    TRIANGLE
}

public struct CivPreset
{
    public ECivElement[,] civ;
    public CivGenerateInfo info;
}

[SerializeField]
public struct CivGenerateInfo
{
    private int civScale;
    private ECivShape civShape;
    private int civDensity;

    public CivGenerateInfo(int scale, ECivShape shape, int density)
    {
        civScale = scale;
        civShape = shape;
        civDensity = density;
    }

    public int Scale()
    {
        return civScale;
    }

    public ECivShape Shape()
    {
        return civShape;
    }

    public int Density()
    {
        return civDensity;
    }
}

public class CivPresetMaker
{
    public static List<CivPreset> presets = new List<CivPreset>();

    private CivPreset pre = new CivPreset();

    public CivPresetMaker()
    {
        pre.civ = new ECivElement[5, 3] {   { (ECivElement)0, (ECivElement)1, (ECivElement)0 },
                                            { (ECivElement)0, (ECivElement)0, (ECivElement)0 },
                                            { (ECivElement)4, (ECivElement)2, (ECivElement)3 },
                                            { (ECivElement)0, (ECivElement)0, (ECivElement)0 },
                                            { (ECivElement)1, (ECivElement)0, (ECivElement)1 }};
        pre.info = new CivGenerateInfo(5, ECivShape.TRIANGLE, 4);

        presets.Add(pre);

        pre.civ = new ECivElement[3, 3] {   { (ECivElement)1, (ECivElement)0, (ECivElement)0 },
                                            { (ECivElement)4, (ECivElement)2, (ECivElement)3 },
                                            { (ECivElement)1, (ECivElement)0, (ECivElement)1 }};
        pre.info = new CivGenerateInfo(3, ECivShape.RECTANGLE, 6);

        presets.Add(pre);

        pre.civ = new ECivElement[5, 5] {   { (ECivElement)0, (ECivElement)0, (ECivElement)1, (ECivElement)0, (ECivElement)1 },
                                            { (ECivElement)0, (ECivElement)0, (ECivElement)0, (ECivElement)0, (ECivElement)0 },
                                            { (ECivElement)1, (ECivElement)4, (ECivElement)2, (ECivElement)3, (ECivElement)1 },
                                            { (ECivElement)0, (ECivElement)0, (ECivElement)0, (ECivElement)0, (ECivElement)0 },
                                            { (ECivElement)1, (ECivElement)0, (ECivElement)1, (ECivElement)0, (ECivElement)1 }};
        pre.info = new CivGenerateInfo(5, ECivShape.RECTANGLE, 4);

        presets.Add(pre);

        pre.civ = new ECivElement[5, 5] {   { (ECivElement)0, (ECivElement)0, (ECivElement)1, (ECivElement)0, (ECivElement)1 },
                                            { (ECivElement)0, (ECivElement)1, (ECivElement)0, (ECivElement)1, (ECivElement)0 },
                                            { (ECivElement)1, (ECivElement)4, (ECivElement)2, (ECivElement)3, (ECivElement)1 },
                                            { (ECivElement)0, (ECivElement)1, (ECivElement)0, (ECivElement)1, (ECivElement)0 },
                                            { (ECivElement)1, (ECivElement)0, (ECivElement)1, (ECivElement)0, (ECivElement)1 }};
        pre.info = new CivGenerateInfo(5, ECivShape.RECTANGLE, 5);

        presets.Add(pre);

        pre.civ = new ECivElement[5, 3] {   { (ECivElement)1, (ECivElement)0, (ECivElement)0 },
                                            { (ECivElement)0, (ECivElement)1, (ECivElement)0 },
                                            { (ECivElement)4, (ECivElement)3, (ECivElement)2 },
                                            { (ECivElement)0, (ECivElement)1, (ECivElement)0 },
                                            { (ECivElement)1, (ECivElement)0, (ECivElement)0 }};
        pre.info = new CivGenerateInfo(5, ECivShape.TRIANGLE, 5);

        presets.Add(pre);

        pre.civ = new ECivElement[2, 2] {   { (ECivElement)1, (ECivElement)3 },
                                            { (ECivElement)4, (ECivElement)2 } };
        pre.info = new CivGenerateInfo(1, ECivShape.TRIANGLE, 1);

        presets.Add(pre);

        pre.civ = new ECivElement[7, 3] {   { (ECivElement)1, (ECivElement)0, (ECivElement)0 },
                                            { (ECivElement)0, (ECivElement)1, (ECivElement)0 },
                                            { (ECivElement)1, (ECivElement)4, (ECivElement)2 },
                                            { (ECivElement)0, (ECivElement)1, (ECivElement)0 },
                                            { (ECivElement)0, (ECivElement)0, (ECivElement)3 },
                                            { (ECivElement)0, (ECivElement)1, (ECivElement)0 },
                                            { (ECivElement)1, (ECivElement)0, (ECivElement)1 }};
        pre.info = new CivGenerateInfo(7, ECivShape.TRIANGLE, 7);

        presets.Add(pre);
    }
    
    public List<CivPreset> GetList()
    {
        return presets;
    }

    public CivPreset Default()
    {
        CivPreset pre = new CivPreset();
        pre.civ = new ECivElement[5, 3] {   { (ECivElement)0, (ECivElement)1, (ECivElement)5 },
                                            { (ECivElement)0, (ECivElement)0, (ECivElement)0 },
                                            { (ECivElement)4, (ECivElement)2, (ECivElement)3 },
                                            { (ECivElement)0, (ECivElement)0, (ECivElement)0 },
                                            { (ECivElement)1, (ECivElement)0, (ECivElement)1 }};
        pre.info = new CivGenerateInfo(3, ECivShape.TRIANGLE, 4);
        return pre;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomTile : Tile
{
    public float height;
    public float temperature;
    public float rainfall;

    public void setHeight(float pHeight)
    {
        height = pHeight;
    }

    public void setTemperature(float pTemperature)
    {
        temperature = pTemperature;
    }

    public void setRainfall(float pRainfall)
    {
        rainfall = pRainfall;
    }

}

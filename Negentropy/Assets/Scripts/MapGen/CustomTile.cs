using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[Serializable]
public class CustomTile : Tile
{
    public float height;
    public float temperature;
    public float rainfall;

    public void setHeight(float pHeight)
    {
        height = pHeight;
    }

    public float getHeight()
    {
        return height;
    }

    public void setTemperature(float pTemperature)
    {
        temperature = pTemperature;
    }

    public float getTemperature()
    {
        return temperature;
    }

    public void setRainfall(float pRainfall)
    {
        rainfall = pRainfall;
    }

    public float getRainfall()
    {
        return rainfall;
    }
}

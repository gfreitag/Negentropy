using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[Serializable]
public class CustomTile : Tile
{
    public float height{get; set;}
    public float temperature{get; set;}
    public float rainfall{get; set;}
    public String biome{get; set;}
    public bool hasCity{get; set;}



    //
    // public void setBiome(String pBiome)
    // {
    //     biome = pBiome;
    // }
    //
    // public String getBiome()
    // {
    //     return biome;
    // }
    //
    // public void setHeight(float pHeight)
    // {
    //     height = pHeight;
    // }
    //
    // public float getHeight()
    // {
    //     return height;
    // }
    //
    // public void setTemperature(float pTemperature)
    // {
    //     temperature = pTemperature;
    // }
    //
    // public float getTemperature()
    // {
    //     return temperature;
    // }
    //
    // public void setRainfall(float pRainfall)
    // {
    //     rainfall = pRainfall;
    // }
    //
    // public float getRainfall()
    // {
    //     return rainfall;
    // }
}

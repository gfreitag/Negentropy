using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class MapData
{
    public int width;
    public int height;
    public Tilemap tilemap;

    public float[,] heightNoiseData;
    public float[,] temperatureNoiseData;
    public float[,] rainfallNoiseData;

    public MapData(int pWidth, int pHeight)
    {
        width = pWidth;
        height = pHeight;
    }

    public static string getBiome(float pHeight, float pTemp, float pRainfall)
    {
        if (pHeight > 0.94)
        {
            return "mountain_range";
        }
        else if(pHeight>0.45){
            if(pTemp < 0.2){
                if(pRainfall < 0.33){
                    return "tundra";
                }
                else if(pRainfall < 0.66){
                    return "alpine_forest";
                }
                else{
                    return "alpine_forest";
                }
            }
            else if(pTemp < 0.4){
                if(pRainfall < 0.33){
                    return "savanna";
                }
                else if(pRainfall < 0.66){
                    return "grassland";
                }
                else{
                    return "deciduous_forest";
                }
            }
            else{
                if(pRainfall < 0.33){
                    return "desert";
                }
                else if(pRainfall < 0.66){
                    return "swamp";
                }
                else{
                    return "rainforest";
                }
            }
        }
        else if(pHeight > 0.38)
        {
            return "beach";
        }
        else if(pHeight > 0.18)
        {
            return "shallow_sea";
        }
        else{
            return "deep_sea";
        }
    }
}

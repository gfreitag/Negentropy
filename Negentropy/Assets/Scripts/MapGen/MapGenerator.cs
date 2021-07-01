using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public MapData mapdata;
    public Tilemap tilemap;
    public TileGen tilegen;

    public bool load;
    CustomTile[,] pixels;
    FastNoiseLite noiseHeight;
    FastNoiseLite noiseTemp;
    FastNoiseLite noiseRain;
    public CustomTile view;

    IEnumerator Start()
    {
        if(load == false)
        {
            mapdata = new MapData(100,100);
            mapdata.heightNoiseData = new float[mapdata.width,mapdata.height];
            mapdata.temperatureNoiseData = new float[mapdata.width,mapdata.height];
            mapdata.rainfallNoiseData = new float[mapdata.width,mapdata.height];
            mapdata.tilemap = tilemap;
            yield return new WaitForSeconds(0.5f);
            //coroutine for progress bar? eventually...
            int randHeightSeed = Random.Range(0, 5000);
            int randTempSeed = Random.Range(0, 5000);
            int randRainSeed = Random.Range(0, 5000);

            pixels = new CustomTile[mapdata.width, mapdata.height];
            noiseHeight = new FastNoiseLite(randHeightSeed);
            noiseTemp = new FastNoiseLite(randTempSeed);
            noiseRain = new FastNoiseLite(randRainSeed);
            noiseHeight.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
            noiseTemp.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
            noiseRain.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
            noiseHeight.SetFrequency(0.056f);
            noiseTemp.SetFrequency(0.04f);
            noiseRain.SetFrequency(0.04f);
            generateNoiseData(ref mapdata.heightNoiseData, noiseHeight);
            generateNoiseData(ref mapdata.temperatureNoiseData, noiseTemp);
            generateNoiseData(ref mapdata.rainfallNoiseData, noiseRain);
            fillMap();
            string jsonData = JsonUtility.ToJson(mapdata);
            PlayerPrefs.SetString("MySettings", jsonData);
            PlayerPrefs.Save();
            Debug.Log("Saved");
        }
        else
        {
            string jsonData = PlayerPrefs.GetString("MySettings");
            MapData loadedData = JsonUtility.FromJson<MapData>(jsonData);
            mapdata = loadedData;
            Debug.Log(loadedData.height);
            fillMap();
            Debug.Log("Loaded");
        }
    }

    //generates noise needed to randomly fill maps.
    void generateNoiseData(ref float[,] data, FastNoiseLite noise)
    {
        for (int x = 0; x < mapdata.height; x++)
        {
            for (int y = 0; y < mapdata.width; y++)
            {
                float noiseVal = noise.GetNoise(x, y);
                //normalization
                data[x,y] = (noiseVal + 1f)/(2f);
            }
        }
    }

    private void fillMap()
    {
        float avg=0f;
        float count=0f;
        for(int x=0; x<mapdata.width; x++)
        {
            for(int y=0; y<mapdata.height; y++)
            {
                string biome = MapData.getBiome(mapdata.heightNoiseData[x,y], mapdata.temperatureNoiseData[x,y], mapdata.rainfallNoiseData[x,y]);
                avg = avg+mapdata.temperatureNoiseData[x,y];
                if(mapdata.temperatureNoiseData[x,y]<0)
                {
                    Debug.Log("NEG");
                }
                count= count + 1f;
                CustomTile tempTile = tilegen.getTile(biome);
                view = tempTile;
                pixels[x,y] = tempTile;
                assignTileData(x,y,mapdata.heightNoiseData[x,y],mapdata.temperatureNoiseData[x,y],mapdata.rainfallNoiseData[x,y]);
                mapdata.tilemap.SetTile(new Vector3Int(-x+mapdata.width/2, -y+mapdata.height/2, 0), tempTile);
            }
        }
        avg = avg/count;
        Debug.Log(avg);
    }

    private void assignTileData(int x, int y, float height, float temperature, float rainfall)
    {
        pixels[x,y].setHeight(height);
        pixels[x,y].setTemperature(temperature);
        pixels[x,y].setRainfall(rainfall);
    }
}

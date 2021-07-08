using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public MapData mapdata;
    public Tilemap tilemap;
    public TileGen tilegen;
    public SaveAndLoader saveAndLoad;

    public bool load;
    CustomTile[,] pixels;
    FastNoiseLite noiseHeight;
    FastNoiseLite noiseTemp;
    FastNoiseLite noiseRain;

    IEnumerator Start()
    {
        mapdata = new MapData();
        mapdata.heightNoiseData = new float[MapData.width, MapData.height];
        mapdata.temperatureNoiseData = new float[MapData.width, MapData.height];
        mapdata.rainfallNoiseData = new float[MapData.width, MapData.height];
        mapdata.tilemap = tilemap;
        pixels = new CustomTile[MapData.width, MapData.height];
        if(load == false)
        {
            yield return new WaitForSeconds(0.5f);
            //coroutine for progress bar? eventually...
            int randHeightSeed = Random.Range(0, 5000);
            int randTempSeed = Random.Range(0, 5000);
            int randRainSeed = Random.Range(0, 5000);

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
            saveAndLoad.saveData();
        }
        else
        {
            saveAndLoad.loadData();
        }
    }

    //generates noise needed to randomly fill maps.
    void generateNoiseData(ref float[,] data, FastNoiseLite noise)
    {
        for (int x = 0; x < MapData.height; x++)
        {
            for (int y = 0; y < MapData.width; y++)
            {
                float noiseVal = noise.GetNoise(x, y);
                //normalization
                data[x,y] = (noiseVal + 1f)/(2f);
            }
        }
    }

    private void fillMap()
    {
        for(int x=0; x<MapData.width; x++)
        {
            for(int y=0; y<MapData.height; y++)
            {
                string biome = MapData.getBiome(mapdata.heightNoiseData[x,y], mapdata.temperatureNoiseData[x,y], mapdata.rainfallNoiseData[x,y]);
                CustomTile tempTile = tilegen.getTile(biome);
                pixels[x,y] = tempTile;
                assignTileData(x,y,mapdata.heightNoiseData[x,y],mapdata.temperatureNoiseData[x,y],mapdata.rainfallNoiseData[x,y]);
                mapdata.tilemap.SetTile(new Vector3Int(-x+MapData.width/2, -y+MapData.height/2, 0), tempTile);
            }
        }
    }

    public void loadMap(CustomTileSaveData[,] aPixels)
    {
        for(int x=0; x<MapData.width; x++)
        {
            for(int y=0; y<MapData.height; y++)
            {
                string biome = MapData.getBiome(aPixels[x,y].height, aPixels[x,y].temperature, aPixels[x,y].rainfall);
                CustomTile tempTile = tilegen.getTile(biome);
                pixels[x,y] = tempTile;
                assignTileData(x,y, aPixels[x,y].height, aPixels[x,y].temperature, aPixels[x,y].rainfall);
                mapdata.tilemap.SetTile(new Vector3Int(-x+MapData.width/2, -y+MapData.height/2, 0), tempTile);
            }
        }
    }

    private void assignTileData(int x, int y, float height, float temperature, float rainfall)
    {
        pixels[x,y].setHeight(height);
        pixels[x,y].setTemperature(temperature);
        pixels[x,y].setRainfall(rainfall);
    }

    public CustomTile[,] getPixels()
    {
        return pixels;
    }
}

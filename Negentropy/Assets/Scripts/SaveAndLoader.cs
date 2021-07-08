using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveAndLoader : MonoBehaviour
{

    BinaryFormatter bf;
    public MapGenerator mapGenerator;

    public void loadData()
    {
        //binary formatter
        bf = new BinaryFormatter();

        //open files
        FileStream mapGenFile = File.Open(Application.persistentDataPath +  "/mapGenData.dat", FileMode.Open);

        //save data object deserialization
        MapGeneratorSaveData mapGenSave = (MapGeneratorSaveData)bf.Deserialize(mapGenFile);

        //calling necessary functions
        mapGenerator.loadMap(mapGenSave.pixels);

        //file closure
        mapGenFile.Close();

    }

    //saves data to binary file.
    public void saveData()
    {
        //binary formatter
        bf = new BinaryFormatter();

        //binary file creation
        FileStream mapGenFile = File.Create(Application.persistentDataPath +  "/mapGenData.dat");

        //save data object creation
        MapGeneratorSaveData mapGenSave = new MapGeneratorSaveData();

        //data assignment
        mapGenSave.pixels = convertTiles(mapGenerator.getPixels());

        //serialization
        bf.Serialize(mapGenFile, mapGenSave);

        //file closure
        mapGenFile.Close();
    }

    private CustomTileSaveData[,] convertTiles(CustomTile[,] orig)
    {
        CustomTileSaveData[,] ret_arr = new CustomTileSaveData[MapData.width, MapData.height];
        for(int x=0; x<MapData.width; x++)
        {
            for(int y=0; y<MapData.height; y++)
            {
                CustomTileSaveData temp = new CustomTileSaveData();
                temp.height = orig[x,y].height;
                temp.temperature = orig[x,y].temperature;
                temp.rainfall = orig[x,y].rainfall;
                ret_arr[x,y] = temp;
            }
        }
        return ret_arr;
    }
}

[Serializable]
class MapGeneratorSaveData
{
    public CustomTileSaveData[,] pixels;
}

[Serializable]
public class CustomTileSaveData
{
    public float height;
    public float temperature;
    public float rainfall;
}

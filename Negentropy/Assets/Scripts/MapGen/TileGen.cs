using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGen : MonoBehaviour
{
    public Sprite[] sprites;
    //private static Color tundra = new Color(200f/255f, 230f/255f, 201f/255f);
    private static Color alpine_forest = new Color(81f/255f, 240f/255f, 129f/255f);
    //private static Color mountain_range = new Color(220f/255f, 220f/255f, 220f/255f);
    private static Color savanna = new Color(187f/255f, 223f/255f, 42f/255f);
    //private static Color grassland = new Color(45f/255f, 218f/255f, 33f/255f);
    private static Color deciduous_forest = new Color(95f/255f, 168f/255f, 39f/255f);
    //private static Color desert = new Color(249f/255f, 158f/255f, 79f/255f);
    private static Color swamp = new Color(111f/255f, 141f/255f, 73f/255f);
    //private static Color rainforest = new Color(10f/255f, 105f/255f, 48f/255f);
    //private static Color beach = new Color(236f/255f, 219f/255f, 152f/255f);
    //private static Color shallow_sea = new Color(81f/255f, 122f/255f, 205f/255f);
    //private static Color deep_sea = new Color(20f/255f, 44f/255f, 141f/255f);
    private static Color blank = new Color(0f/255f, 0f/255f, 0f/255f);

    public CustomTile getTile(string biome)
    {
        Texture2D newTexture = Texture2D.whiteTexture;
        newTexture.Resize(256,256);
        newTexture.Apply();
        Sprite newSprite = Sprite.Create(newTexture, new Rect(0f,0f,256f,256f), new Vector2(0,0));
        CustomTile tile = ScriptableObject.CreateInstance<CustomTile>() as CustomTile;
        tile.sprite = newSprite;
        switch (biome)
        {
            case "tundra":
                tile.sprite = sprites[3];
                break;
            case "alpine_forest":
                tile.color = alpine_forest;
                break;
            case "mountain_range":
                tile.sprite = sprites[2];
                break;
            case "savanna":
                tile.color = savanna;
                break;
            case "grassland":
                tile.sprite = sprites[1];
                break;
            case "deciduous_forest":
                tile.color = deciduous_forest;
                break;
            case "desert":
                tile.sprite = sprites[0];
                break;
            case "swamp":
                tile.color = swamp;
                break;
            case "rainforest":
                tile.sprite = sprites[4];
                break;
            case "beach":
                tile.sprite = sprites[7];
                break;
            case "shallow_sea":
                tile.sprite = sprites[6];
                break;
            case "deep_sea":
                tile.sprite = sprites[5];
                break;
            default:
                Debug.Log(biome);
                tile.color = blank;
                break;
        }
        return tile;
    }
}

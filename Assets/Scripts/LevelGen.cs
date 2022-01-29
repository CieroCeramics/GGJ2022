using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    [Serializable]
    public struct ColorObjectData
    {
        public string name;
        public Color color;
        public GameObject prefabObject;
        public int count;
    }

    [SerializeField] 
    private ColorObjectData[] foundColorObjectData;

    public Texture2D my_texture;
    public Color GenericTileColor;
    public Color FireCharTileColor;
    public Color IceCharTileColor;
    public Color RiverTileColor;
    public Color BridgeTileColor;
    public Color IcePortalTileColor;
    public Color FirePortalTileColor;
    public Color LavaTileColor;


    public GameObject Floor_Prefab;
    public GameObject FireChar_Prefab;
    public GameObject IceChar_Prefab;
    public GameObject River_Prefab;
    public GameObject Bridge_Prefab;
    public GameObject Lava_Prefab;
    public GameObject IcePortal_Prefab;
    public GameObject FirePortal_Prefab;

    [ContextMenu("My Function")]
    void SearchImage()
    {
        var library = new Dictionary<Color, ColorObjectData>();

        for (int x = 0; x < my_texture.width; x++)
        {
            for (int y = 0; y < my_texture.height; y++)
            {
                var foundColor = my_texture.GetPixel(x, y);

                if (library.ContainsKey(foundColor))
                {
                    var temp = library[foundColor];
                    temp.count++;
                    library[foundColor] = temp;
                    continue;
                }

                library.Add(foundColor, new ColorObjectData
                {
                    color = foundColor,
                    count = 1
                });
            }
        }

        foundColorObjectData = library.Values.ToArray();
    }
    [ContextMenu("GenerateLevel")]
    void GenLevel()
    {
        var objectLibrary = new Dictionary<Color, GameObject>();
        var objectCount = new Dictionary<Color, (string name, int count)>();

        foreach (var colorData in foundColorObjectData)
        {
            objectLibrary.Add(colorData.color, colorData.prefabObject);
            objectCount.Add(colorData.color, (colorData.name, 0));
        }

        //--------------------------------------------------------------------------------------------------------//
        
        for (int i = 0; i < my_texture.width; i++)
        {
            for (int j = 0; j < my_texture.height; j++)
            {
                var foundColor = my_texture.GetPixel(i, j);

                if (objectLibrary.TryGetValue(foundColor, out var prefab) == false)
                    return;

                var aTile = Instantiate(prefab, new Vector3(i, prefab.transform.position.y, j), prefab.transform.rotation);
                aTile.GetComponent<Tile>().coordinate =new Vector2Int (i, j);
                var test = objectCount[foundColor];
                test.count++;
                objectCount[foundColor] = test;
                

                /*if( my_texture.GetPixel(i,j) == GenericTileColor ){
                Instantiate(Floor_Prefab, new Vector3(i, 0, j),  Quaternion.Euler(new Vector3(90, 0, 0)));
                }

                else if( my_texture.GetPixel(i,j) == FireCharTileColor ){
                Instantiate(Floor_Prefab, new Vector3(i, 0, j),  Quaternion.Euler(new Vector3(90, 0, 0)));
                Instantiate(FireChar_Prefab, new Vector3(i, 0, j),  Quaternion.identity);
                }

                   else if( my_texture.GetPixel(i,j) == IceCharTileColor ){
                Instantiate(Floor_Prefab, new Vector3(i, 0, j),  Quaternion.Euler(new Vector3(90, 0, 0)));
                Instantiate(IceChar_Prefab, new Vector3(i, 0, j),  Quaternion.identity);
                }


                  else if( my_texture.GetPixel(i,j) == RiverTileColor ){
                Instantiate(River_Prefab, new Vector3(i, 0, j),  Quaternion.Euler(new Vector3(90, 0, 0)));
                }


                   else if( my_texture.GetPixel(i,j) == BridgeTileColor ){
                Instantiate(Bridge_Prefab, new Vector3(i, 0, j),  Quaternion.Euler(new Vector3(90, 0, 0)));
                }


                else if( my_texture.GetPixel(i,j) == IcePortalTileColor ){
                Instantiate(IcePortal_Prefab, new Vector3(i, 0, j),  Quaternion.Euler(new Vector3(90, 0, 0)));
                }


                  else if( my_texture.GetPixel(i,j) == FirePortalTileColor ){
                Instantiate(FirePortal_Prefab, new Vector3(i, 0, j),  Quaternion.Euler(new Vector3(90, 0, 0)));
                  }
                
                  else if( my_texture.GetPixel(i,j) == LavaTileColor ){
                Instantiate(Lava_Prefab, new Vector3(i, 0.5f, j),  Quaternion.Euler(new Vector3(90, 0, 0)));*/
            }

        }

        foreach (var i in objectCount)
        {
            Debug.Log($"{i.Value.name}: {i.Value.count}");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        //--------------------------------------------------------------------------------------------------------//
        

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelGen : MonoBehaviour
{
    //Structs
    //====================================================================================================================//
    
    [Serializable]
    public struct ColorObjectData
    {
        public string name;
        public Color color;
        public GameObject prefabObject;

        public bool forceAddFloor;
        public bool isCharacter;
        public bool isInteractable;

        public Vector3 positionOffset;
    }
#if UNITY_EDITOR
    //Editor Properties
    //====================================================================================================================//
    
    [SerializeField, Header("Generation Data")]
    private Texture2D my_texture;
    [SerializeField] 
    private ColorObjectData[] foundColorObjectData;

    [SerializeField, Header("Parents")]
    private Transform levelObjectsParent;
    [SerializeField]
    private Transform characterParent;
    [SerializeField]
    private Transform interactableObjectsParent;
    
    [SerializeField, Header("Misc")]
    private GameObject floorTilePrefab;

    //Editor Functions
    //====================================================================================================================//

    public void GenerateLevelData()
    {
        ForceClean();
        
        if (my_texture == null)
            throw new NullReferenceException();
        
        SearchImage();

        if (foundColorObjectData == null || foundColorObjectData.Length == 0)
            throw new ArgumentException();

        GenLevel();
    }

    //====================================================================================================================//
    
    [ContextMenu("My Function")]
    private void SearchImage()
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
                    library[foundColor] = temp;
                    continue;
                }

                library.Add(foundColor, new ColorObjectData
                {
                    color = foundColor,
                });
            }
        }


        //Only set list if its not been setup already
        //--------------------------------------------------------------------------------------------------------//
        
        if (foundColorObjectData == null)
        {
            foundColorObjectData = library.Values.ToArray();
            return;
        }

        //Go through scanned colors, looking for anything new
        //--------------------------------------------------------------------------------------------------------//
        var hasNewValues = false;
        var currentValues = foundColorObjectData.ToList();
        foreach (var kvp in library)
        {
            if (foundColorObjectData.Any(x => x.color == kvp.Key))
                continue;
            
            currentValues.Add(kvp.Value);
            hasNewValues = true;
        }

        //Save list data if something new was found
        //--------------------------------------------------------------------------------------------------------//
        
        if (hasNewValues == false)
            return;
        
        foundColorObjectData = currentValues.ToArray();
    }
    [ContextMenu("GenerateLevel")]
    private void GenLevel()
    {

        //--------------------------------------------------------------------------------------------------------//
        
        GameObject InstantiatePrefab(Object somePrefab, Vector3 position, Quaternion rotation)
        {
            // Get the Path to Prefab
            var prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(somePrefab);
         
            // Load Prefab Asset as Object from path
            var newObject = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(Object));
         
            //Instantiate the Prefab in the scene, as a child of the GO this script runs on
            var newPrefab = (GameObject)PrefabUtility.InstantiatePrefab(newObject, null);
            newPrefab.transform.position = position;
            newPrefab.transform.rotation = rotation;
            
            return newPrefab;
        }

        //--------------------------------------------------------------------------------------------------------//
        
        var objectLibrary = new Dictionary<Color, LevelGen.ColorObjectData>();
        var objectCount = new Dictionary<Color, (string name, int count)>();

        foreach (var colorData in foundColorObjectData)
        {
            objectLibrary.Add(colorData.color, colorData);
            objectCount.Add(colorData.color, (colorData.name, 0));
        }

        //--------------------------------------------------------------------------------------------------------//
        
        for (int x = 0; x < my_texture.width; x++)
        {
            for (int y = 0; y < my_texture.height; y++)
            {
                var foundColor = my_texture.GetPixel(x, y);

                //Debug Count
                //--------------------------------------------------------------------------------------------------------//
                
                var test = objectCount[foundColor];
                test.count++;
                objectCount[foundColor] = test;

                //--------------------------------------------------------------------------------------------------------//
                
                if (objectLibrary.TryGetValue(foundColor, out var colorObjectData) == false)
                    return;
                
                var prefab = colorObjectData.prefabObject;
                
                var objectInstance = InstantiatePrefab(prefab, 
                    new Vector3(x, prefab.transform.position.y, y) + colorObjectData.positionOffset,
                    prefab.transform.rotation);
                objectInstance.name = $"{prefab.name}_[{x}, {y}]";

                if (colorObjectData.forceAddFloor)
                {
                    var floorInstance = InstantiatePrefab(floorTilePrefab, new Vector3(x, floorTilePrefab.transform.position.y, y),
                        floorTilePrefab.transform.rotation);
                    floorInstance.name = $"FORCED_{floorTilePrefab.name}_[{x}, {y}]";
                    floorInstance.GetComponent<Tile>().coordinate = new Vector2Int (x, y);
                    floorInstance.transform.SetParent(levelObjectsParent, true);
                }

                if (colorObjectData.isCharacter)
                {
                    objectInstance.transform.SetParent(characterParent, true);
                    continue;
                }

                if (colorObjectData.isInteractable)
                {
                    objectInstance.transform.SetParent(interactableObjectsParent, true);
                    continue;
                }

                var tile = objectInstance.GetComponent<Tile>();

                if (tile == null)
                    continue;
                
                objectInstance.transform.SetParent(levelObjectsParent, true);
                tile.coordinate = new Vector2Int (x, y);
            }

        }

        var sb = new StringBuilder("<b>Generated Objects:</b>\n");
        foreach (var i in objectCount)
        {
            sb.AppendLine($"   {i.Value.name}: {i.Value.count}");
        }
        
        Debug.Log(sb.ToString());
    }

    [ContextMenu("FORCE CLEAN ALL OBJECTS")]
    private void ForceClean()
    {
        void DestroyChildren(params Transform[] targetTransforms)
        {
            foreach (var targetTransform in targetTransforms)
            {
                var count = targetTransform.childCount;

                for (int i = count - 1; i >= 0; i--)
                {
                    DestroyImmediate(targetTransform.GetChild(i).gameObject);
                }
            }
            
        }

        DestroyChildren(levelObjectsParent, characterParent, interactableObjectsParent);
    }
    
#endif
    //====================================================================================================================//
    

}

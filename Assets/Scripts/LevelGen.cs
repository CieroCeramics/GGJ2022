using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{

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

    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i<=my_texture.width+1; i ++)
        {
            
            for  (int j =0; j<=my_texture.height+1; j ++)
            {
                if( my_texture.GetPixel(i,j) == GenericTileColor ){
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
                Instantiate(Lava_Prefab, new Vector3(i, 0.5f, j),  Quaternion.Euler(new Vector3(90, 0, 0)));
                }

            }
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{

    public Texture2D my_texture;
    // Start is called before the first frame update
    void Start()
    {
        for (int i =0; i<=my_texture.width+1; i ++)
        {
            for  (int j =0; j<=my_texture.height+1; j ++)
            {
                Debug.Log(my_texture.GetPixel(i,j));
            }
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

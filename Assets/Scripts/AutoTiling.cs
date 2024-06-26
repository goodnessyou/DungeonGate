using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class AutoTiling : MonoBehaviour
{
    MeshRenderer mesh;

    //public float scaleFactor = 5.0f;
    Material mat;
    
    void Start () 
    {
        mesh = GetComponent<MeshRenderer>();
        float scaleX = transform.localScale.x / 200;
        float scaleY = transform.localScale.y / 200;
        
        GetComponent<Renderer>().sharedMaterial.mainTextureScale = new Vector2 (transform.localScale.x / scaleX , transform.localScale.y / scaleY);
    }

    // Update is called once per frame
    // void Update () 
    // {

    //     if (transform.hasChanged && Application.isEditor && !Application.isPlaying) 
    //     {
            
    //         GetComponent<Renderer>().material.mainTextureScale = new Vector2 (transform.localScale.x / scaleX , transform.localScale.z / scaleY);
    //         transform.hasChanged = false;
    //     } 

    // }
}

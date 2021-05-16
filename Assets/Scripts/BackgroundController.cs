using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField]
    private Vector2 scroll_speed = new Vector2(0, 0);

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollBackground();
    }

    void ScrollBackground(){
        material.mainTextureOffset += scroll_speed * Time.deltaTime;
    }
    
}

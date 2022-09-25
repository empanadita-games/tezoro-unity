using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;

    private float lenght;
    private float startPos;
    private GameObject cam;

    void Start()
    {
        cam = GameObject.Find("CM vcam1");//TO DO: Mejorar esto.
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        
        if(temp > startPos + lenght)
        {
            startPos +=lenght;
        }
        else if( temp < startPos - lenght)
        {
            startPos -= lenght;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("waterRise", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void waterRise()
    {
        transform.Translate(Vector3.up * 0.025f, Space.World);
    }
}

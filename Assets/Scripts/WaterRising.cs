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
        if(transform.position.y <10.75)
        transform.Translate(Vector3.up * 0.025f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name=="duck1"||other.name =="popsicle")
        {
            other.transform.SetParent(gameObject.transform);
        }
    }
}

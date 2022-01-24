using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("waterRise", 0.1f, 0.04f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void waterRise()
    {
        if(transform.position.y <10.75)
        transform.Translate(Vector3.up * 0.01f, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name=="duck1"||other.name =="popsicle")
        {
            other.transform.SetParent(gameObject.transform);
        }
    }
}

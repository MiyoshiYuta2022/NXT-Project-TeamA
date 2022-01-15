using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveArea : MonoBehaviour
{
    public bool b_onFireArea;
    // Start is called before the first frame update
    void Start()
    {
        b_onFireArea = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "PlayerCollision")
        {
            b_onFireArea = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerCollision")
        {
            b_onFireArea = false;
        }
    }

    public bool GetOnFireArea()
    {
        return b_onFireArea;
    }
}

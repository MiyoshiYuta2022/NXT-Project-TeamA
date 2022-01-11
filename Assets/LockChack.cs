using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockChack : MonoBehaviour
{
    
    [SerializeField] GameObject m_FlareParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBecameVisible()
    {
        m_FlareParent.SetActive(true);
    }
    void OnBecameInvisible()
    {
        m_FlareParent.SetActive(false);
    }
}

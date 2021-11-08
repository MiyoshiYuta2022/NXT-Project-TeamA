using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    const float SPEED = 20f;

    const float DESTROY_TIME = 3;

    float m_DestroyCount = 0;

    int m_WaterPower = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(0, 0, SPEED * Time.deltaTime);

        m_DestroyCount += Time.deltaTime;

        if(m_DestroyCount > DESTROY_TIME)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //è∞Ç…ìñÇΩÇ¡ÇΩÇÁ
        if (other.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    public int GetWaterPower()
    {
        return m_WaterPower;
    }
    public void SetWaterPower(int waterpower)
    {
        m_WaterPower = waterpower;
    }
}

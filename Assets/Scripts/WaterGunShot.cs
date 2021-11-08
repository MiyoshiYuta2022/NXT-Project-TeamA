using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunShot : MonoBehaviour
{
    public GameObject Water;

    //発射間隔
    const float INTERVAL = 10;

    //発射間隔待ち時間
    float m_FireInterval = 0;

    const float DIRECTION_1 = -3;
    const float DIRECTION_2 = 5;

    float m_DirectionCount = 0;

    int m_WaterPower = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_FireInterval < 0)
        {
            //マウスを左クリックしている間
            if (Input.GetMouseButton(0))
            {
                GameObject obj = Water;
                Vector3 pos = this.gameObject.transform.position;
                GameObject myobject = this.gameObject;
                Quaternion keeprotation = this.gameObject.transform.rotation;

                if (m_DirectionCount == 10)
                {
                    myobject.transform.Rotate(0, DIRECTION_1, 0);
                    m_DirectionCount = 0;
                }
                else if(m_DirectionCount == 6 || m_DirectionCount == 8)
                {
                    myobject.transform.Rotate(0, DIRECTION_2, 0);
                    m_DirectionCount++;
                }               
                else
                {
                    myobject.transform.Rotate(0, 0, 0); 
                    m_DirectionCount++;
                }
                

                // Cubeプレハブを元に、インスタンスを生成、
                GameObject waterobject =  Instantiate(obj, new Vector3(pos.x, pos.y, pos.z), myobject.transform.rotation);

                waterobject.GetComponent<WaterMove>().SetWaterPower(m_WaterPower);

                m_FireInterval = INTERVAL * Time.deltaTime;
                this.gameObject.transform.rotation = keeprotation;
            }            
        }
        else
        {
            m_FireInterval -= Time.deltaTime;
        }
    }

    public int GetWaterPower()
    {
        return m_WaterPower;
    }
}

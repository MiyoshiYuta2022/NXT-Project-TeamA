using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetSensYValue : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    private float sensivity;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sensivity = GameObject.Find("SensivityY").GetComponent<Slider>().value;

        inputField.text = sensivity.ToString("N1");
    }

    public void SetValue()
    {
        sensivity = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraControl>().sens_x;

        inputField.text = sensivity.ToString();
    }

}

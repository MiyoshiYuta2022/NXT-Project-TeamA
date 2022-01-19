using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetAudioValue : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    private float audioValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audioValue = GameObject.Find("AudioVolume").GetComponent<Slider>().value;

        inputField.text = audioValue.ToString("N1");
    }
}

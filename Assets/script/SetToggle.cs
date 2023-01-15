using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetToggle : MonoBehaviour
{

    public GameObject flower;
    public GameObject toggle;

    
    void Update()
    {
        if(flower.activeSelf==false)
        {
        toggle.GetComponent<Toggle>().isOn = true;
        }
    }
}

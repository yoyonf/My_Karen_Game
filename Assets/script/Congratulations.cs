using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Congratulations : MonoBehaviour
{

    public GameObject[] objects = new GameObject[12];
    private int a = 0;
    private int j = 0;
    public GameObject canvas;
    public GameObject text;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i <= 12; i++)
        {
            if(objects[a].GetComponent<Toggle>().isOn == true)
            {
                j +=1;
                
            
            }
        }


        if(j==12)
        {
        
            canvas.SetActive(true);
            text.SetActive(false);

        }


    }
       // StartCoroutine(Wait());

    //}

    // IEnumerator Wait(){
    //       for (i=0; i<12; i++)
    //     {
    //         if(objects[i].GetComponent<Toggle>().isOn == true){
    //             j = j+j;

            
    //         }
    //     }


    //         if(j==12){
        
    //         canvas.SetActive(true);
    //         text. SetActive(false);


    //     }
    //     yield return new WaitForSeconds(1.0f);
//     }
}

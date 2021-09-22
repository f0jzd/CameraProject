using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTestScript : MonoBehaviour
{

    public Color currentColor;
    public Color newColor;

    private GameObject lerpObject;
    private float t;
    public float multiplier;


    // Start is called before the first frame update
    void Start()
    {
        lerpObject = GetComponent<GameObject>();

        currentColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey(KeyCode.Space))
        {
              
            
            GetComponent<Renderer>().material.color = Color.Lerp(currentColor, newColor,Mathf.Lerp(0,1,t += multiplier * Time.deltaTime));

        }
        
    }
}

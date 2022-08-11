using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
    rb=this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKey(KeyCode.UpArrow)){
        rb.AddForce(new Vector3(5f,0f,0f));
       } 
       if(Input.GetKey(KeyCode.DownArrow)){
        rb.AddForce(new Vector3(-5f,0f,0f));
       } 
    }
}

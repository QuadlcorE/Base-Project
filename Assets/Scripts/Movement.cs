using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement: MonoBehaviour
{
    [SerializeField] float speed = 1; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Receive inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Mouse movement
        //float mLeft = Input.GetAxis("GetMouse")
        //Move Player
        Vector3 temp = new Vector3(horizontalInput, verticalInput, 0);
        temp = temp.normalized * speed * Time.deltaTime;

        transform.position += temp;
    }
}


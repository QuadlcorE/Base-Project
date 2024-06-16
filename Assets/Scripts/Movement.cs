using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField] float speed = 5;

    Vector3 turn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Aim();
    }

    private void Move()
    {
        //Receive inputs
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        //Move Player
        Vector3 temp = new Vector3(horizontalInput, verticalInput, 0);
        temp = temp.normalized * speed * Time.deltaTime;
        transform.position += temp;
    }

    void Aim()
    {
        //Mouse movement
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        float zRot = Mathf.Atan2(turn.y, turn.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, zRot);
    }
}


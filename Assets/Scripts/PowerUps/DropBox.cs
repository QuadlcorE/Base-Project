using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DropBox : MonoBehaviour
{
    private int _dropBoxHealth;

    void Start()
    {
        _dropBoxHealth = 5;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>())
        {
            _dropBoxHealth--;
        }
    }

    private void Update()
    {
        if (_dropBoxHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}

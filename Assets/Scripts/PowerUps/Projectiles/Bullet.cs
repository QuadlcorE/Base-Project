using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    private void Awake()
    {
        _speed = 1500.0f;
    }
}

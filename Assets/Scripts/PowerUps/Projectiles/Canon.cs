using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : Projectile
{
    private void Awake()
    {
        _speed = 2500.0f;
        damagePoints = 10;
    }
}

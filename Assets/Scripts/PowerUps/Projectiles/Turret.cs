using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Projectile
{
    private void Awake()
    {
        _speed = 5000.0f;
        damagePoints = 5;
    }
}

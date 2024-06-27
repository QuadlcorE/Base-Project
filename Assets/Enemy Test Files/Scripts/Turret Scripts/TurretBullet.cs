using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] int life = 3;
    [SerializeField] float speed = 30f;

    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        this.direction = direction;
        Destroy(gameObject, 2f);
    }

    public void Shoot(Vector2 shootDirection)
    {
        this.direction = shootDirection;
        rb.velocity = this.direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        life--;
        if (life<0)
        {
            Destroy(gameObject);
            return;
        }

        var firstContact = collision.contacts[0];
        Vector2 newVelocity = Vector2.Reflect(direction.normalized, firstContact.normal);
        Shoot(newVelocity.normalized);
    }
}

using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public int life = 1;
    public float speed = 20f;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        this.direction = direction;
        Destroy(gameObject, 4f);
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

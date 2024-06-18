using UnityEngine;

public class BounceCalculator : MonoBehaviour
{
    /// <summary>
    /// Force with which the ball bounces off the walls
    /// </summary>
    [SerializeField] float bounceStrength;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile _projectile = collision.gameObject.GetComponent<Projectile>();
        Rigidbody2D _projectileRb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (_projectile != null)
        {
            Vector2 normal = collision.GetContact(0).normal;

            if (_projectileRb != null)
            {
                _projectileRb.AddForce(bounceStrength * Time.deltaTime * -normal);
            }
        }
    }
}
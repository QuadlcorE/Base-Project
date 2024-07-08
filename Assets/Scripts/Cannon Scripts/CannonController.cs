using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject instantiatePoint;
    public CannonBullet bullet;
    public float fireRate = 4f;
    public float rotationStartTime = 3f;

    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TrackTarget(GameObject aimingAt)
    {
        if(Time.time - rotationStartTime < 1)
        {
            Vector2 directionToTarget = aimingAt.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, Time.time - rotationStartTime);
        }
        else
        {
            rotationStartTime = Time.time;
            startRotation = transform.rotation;
        }
    }

    public void Fire()
    {
        CannonBullet firedBullet = Instantiate(bullet, instantiatePoint.transform.position, instantiatePoint.transform.rotation);
        float angle = (Mathf.Atan2(instantiatePoint.transform.up.y, instantiatePoint.transform.up.x) * Mathf.Rad2Deg);
        Vector2 firingDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        firedBullet.Shoot(firingDirection);
    }
}

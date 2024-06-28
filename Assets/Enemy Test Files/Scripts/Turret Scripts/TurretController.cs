using System.Collections;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    // public GameObject aimingAt;
    public GameObject centerInstantiatePoint;
    public TurretBullet bullet;
    public float fireRate = 4f;
    public float deviationRange = 5f;
    public float rotationStartTime = 2f;

    public bool firing = false;

    private Quaternion startRotation;
    private bool canFire = true;

    /* Nifty trick to get mouse position 
    Vector2 MousePos
    {
        get {return cam.ScreenToWorldPoint(Input.mousePosition);}
    }*/

    // Update is called once per frame
    void Update()
    {
        
        if (canFire & firing)
        {
            StartCoroutine(Fire());
        }
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

    public IEnumerator Fire()
    {
        canFire = false;
        TurretBullet firedBullet = Instantiate(bullet, centerInstantiatePoint.transform.position, centerInstantiatePoint.transform.rotation);
        float angle = (Mathf.Atan2(centerInstantiatePoint.transform.up.y, centerInstantiatePoint.transform.up.x) * Mathf.Rad2Deg);
        float randomDeviation = Random.Range(-deviationRange / 2, deviationRange / 2);
        Vector2 firingDirection = new Vector2(Mathf.Cos((angle + randomDeviation) * Mathf.Deg2Rad), Mathf.Sin((angle + randomDeviation) * Mathf.Deg2Rad)).normalized;

        firedBullet.Shoot(firingDirection);
        StartCoroutine(FireRateHandler());
        yield return null;
    }

    IEnumerator FireRateHandler()
    {
        float timeToNextFire = 1 / fireRate;
        yield return new WaitForSeconds(timeToNextFire);
        canFire = true;
    }
}

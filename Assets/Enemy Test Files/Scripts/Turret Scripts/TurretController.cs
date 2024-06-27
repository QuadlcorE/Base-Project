using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    // public GameObject aimingAt;
    public GameObject currentLockedOn;
    public List<GameObject> instantiatePoint;
    public GameObject centerInstantiatePoint;
    public TurretBullet bullet;
    public float fireRate = 4f;
    public float deviationRange = 5f;

    private bool canFire = true;

    /* Nifty trick to get mouse position 
    Vector2 MousePos
    {
        get {return cam.ScreenToWorldPoint(Input.mousePosition);}
    }*/

    // Update is called once per frame
    void Update()
    {
        // TrackTarget();
        /* if (canFire)
        {
            StartCoroutine(Fire());
        } */

        /*
        foreach (GameObject item in instantiatePoint)
        {
            if (canFire)
            {
                StartCoroutine(Fire(item));
            }
        }*/

    }

    public void TrackTarget(GameObject aimingAt, float rotationSpeed)
    {
        /* Calculate the direction to the target
        Vector3 directionToTarget = aimingAt.transform.position - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // Set the rotation to face the target
        transform.rotation = Quaternion.Lerp(transform.rotation, new Vector3(0, 0, angle - 90));
        */

        /*
        Vector3 directionToTarget = aimingAt.transform.position - transform.position;
        directionToTarget.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.rotation = newRotation;*/
    }

    public IEnumerator Fire()
    {
        canFire = false;
        TurretBullet firedBullet = Instantiate(bullet, centerInstantiatePoint.transform.position, this.transform.rotation);
        float angle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
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

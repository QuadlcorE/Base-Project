using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public float RotationSpeed;
    public GameObject tracker;
    public float coolDownTime;
    public float idleTime;

    private TurretController turretController;
    [SerializeField] private TurretStates currentState;
    
    [SerializeField] private float currentIdleTime;
    [SerializeField] private float randomWaitTime;
    [SerializeField] private bool inIdleTime;

    private enum TurretStates
    {
        Idle,
        Tracking,
        Firing
    }


    private void IdleState()
    {
        currentIdleTime += Time.deltaTime;

        if (inIdleTime == false) randomWaitTime = Random.Range(1, idleTime);
        inIdleTime = true;

        if (currentIdleTime >= randomWaitTime)
        {
            currentIdleTime = 0;
            inIdleTime = false;
            currentState = TurretStates.Tracking;
        }

    }

    private void TrackingState()
    {
        if (turretController != null)
        {
            turretController.TrackTarget(tracker, RotationSpeed);
        }
        Vector3 directionToTarget = tracker.transform.position - transform.position;
        directionToTarget.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        // if (transform.rotation == targetRotation) currentState = TurretStates.Firing;
    }

    private IEnumerator FiringState()
    {
        StartCoroutine(turretController.Fire());
        yield return new WaitForSeconds(coolDownTime);
        currentState = TurretStates.Idle;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        turretController = GetComponent<TurretController>();
        currentState = TurretStates.Idle;
        inIdleTime = false;
        currentIdleTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case TurretStates.Idle:
                IdleState();
                break;
            case TurretStates.Firing:
                FiringState();
                break;
            case TurretStates.Tracking:
                TrackingState();
                break;
        }
    }
}

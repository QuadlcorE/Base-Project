using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public GameObject tracker;
    
    public float idleTime;
    public float trackTime;
    public float firingTime;

    private TurretController turretController;
    private TurretStates currentState;
    
    private float currentIdleTime;
    private float randomWaitTime;
    private bool inIdleTime;

    private float currentTrackTime;

    private float currentFiringTime;

    private enum TurretStates
    {
        Idle,
        Tracking,
        Firing
    }


    // ==================== State Definitions ============================
    private void IdleState()
    {
        currentIdleTime += Time.deltaTime;

        if (inIdleTime == false) randomWaitTime = Random.Range(idleTime/2, idleTime);
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
        currentTrackTime += Time.deltaTime;       
        if (turretController != null)
        {
            turretController.TrackTarget(tracker);
        }
        if (currentTrackTime >= trackTime)
        {
            currentTrackTime = 0;
            currentState = TurretStates.Firing;
        }

    }

    private void FiringState()
    {
        turretController.TrackTarget(tracker);
        currentFiringTime += Time.deltaTime;
        turretController.firing = true;
        if (currentFiringTime >= firingTime)
        {
            currentFiringTime = 0;
            turretController.firing = false;
            currentState = TurretStates.Tracking;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        turretController = GetComponent<TurretController>();
        currentState = TurretStates.Idle;
        inIdleTime = false;
        currentIdleTime = 0;
        currentTrackTime = 0;
        currentFiringTime = 0;
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

    public void SetTracker(GameObject tk)
    {
        tracker = tk;
    }
}

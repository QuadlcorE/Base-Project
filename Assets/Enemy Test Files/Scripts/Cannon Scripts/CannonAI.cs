using UnityEngine;

public class CannonAI : MonoBehaviour
{
    public GameObject tracker;

    public float idleTime;
    public float trackTime;
    public float firingTime;

    private CannonController cannonController;
    private CannonStates currentState;

    private float currentIdleTime;
    private float randomWaitTime;
    private bool inIdleTime;

    private float currentTrackTime;

    private float currentFireTime;


    private enum CannonStates
    {
        Idle,
        Tracking,
        Firing
    }

    // ========================== State Definitions ==============================
    private void IdleState()
    {
        currentIdleTime += Time.deltaTime;
        if (inIdleTime == false) randomWaitTime = Random.Range(idleTime/2, idleTime);
        inIdleTime = true;

        if (currentIdleTime >= randomWaitTime)
        {
            currentIdleTime = 0;
            inIdleTime = false;
            currentState = CannonStates.Tracking;
        }
    }

    private void TrackingState()
    {
        currentTrackTime += Time.deltaTime;
        if (cannonController != null) cannonController.TrackTarget(tracker);
        if (currentTrackTime >= trackTime)
        {
            currentTrackTime = 0;
            currentState = CannonStates.Firing;
        }
    }

    private void FiringState()
    {
        if (currentFireTime == 0) cannonController.Fire();
        currentFireTime += Time.deltaTime;
        if (currentFireTime >= firingTime)
        {
            currentFireTime = 0;
            currentState = CannonStates.Tracking;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cannonController = GetComponent<CannonController>();
        currentState = CannonStates.Idle;
        currentIdleTime = 0;
        currentTrackTime = 0;
        currentFireTime = 0;
        inIdleTime = false;   
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case CannonStates.Idle:
                IdleState();
                break;
            case CannonStates.Tracking:
                TrackingState();
                break;
            case CannonStates.Firing:
                FiringState();
                break;
        }
    }

    public void SetTracker(GameObject tk)
    {
        tracker = tk;
    }
}

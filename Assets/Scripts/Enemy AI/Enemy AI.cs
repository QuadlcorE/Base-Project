using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject targetLocation;
    public GameObject targetAim;
    public GameObject moveAbleArea;
    public GameObject SpawnObject;
    public SpawnController SpawnChecker;
    public PowerUps PowerUpManager;
    public EnemyBullet bullet;
    public List<GameObject> aimAbleTargets;

    public float fireRate;

    public float idleTime;
    public float strafingTime;
    public float movingTime;

    public float targetedTime;
    public float cooldownTime;

    public float rotationStartTime;

    private Quaternion startRotation;
    private Bounds moveAbleAreaBounds;
    private float minX;
    private float minY;

    private PowerUpDefinitions[] _availablePowerups;

    [SerializeField] private EnemyMoveStates _currentEnemyMoveState;
    [SerializeField] private EnemyTargetStates _currentEnemyTargetStates;
    [SerializeField] private EnemyPowerUpUseStates _currentEnemyPowerUpUseStates;

     private float _currentIdleTime;
     private float _currentStrafingTime;
     private float _currentMovingTime;
     private float _currentCooldownTime;
     private float _currentTargetingTime;

     private float _randomIdleTime;
     private float _randomStrafingTime;
     private float _randomMovingTime;
     private float _randomCoolDownTime;
     private float _randomTargetTime;

     private bool _isIdle;
     private bool _isStraffing;
     private bool _isMoving;
     private bool _isCoolingDown;
     private bool _isFiring;
     private bool _canFire;

    private enum EnemyMoveStates
    {
        Idle,
        Strafing,
        Moving
    }

    private enum EnemyTargetStates
    {
        Targeted,
        TargetSwitching
    }

    private enum EnemyPowerUpUseStates
    {
        UsingPowerUp,
        Cooldown
    }

    // -------------------------------------------------------------------------
    // ===================== Move States =======================================
    // -------------------------------------------------------------------------
    private void IdleMoveState()
    {
        _currentIdleTime += Time.deltaTime;
        if (!_isIdle)
        {
            targetLocation.transform.position = transform.position;
            _isIdle = true;
            _randomIdleTime = UnityEngine.Random.Range(idleTime / 2, idleTime);
        }

        if (_currentIdleTime >= _randomIdleTime)
        {
            _currentIdleTime = 0;
            _isIdle = false;
            _currentEnemyMoveState = EnemyMoveStates.Moving;
        }
    }

    private void MoveMoveState()
    {
        _currentMovingTime += Time.deltaTime;
        if (!_isMoving)
        {
            _isMoving = true;
            _randomMovingTime = UnityEngine.Random.Range(idleTime / 2, idleTime);
            targetLocation.transform.position = new Vector3(UnityEngine.Random.Range(minX, -minX),
                                    UnityEngine.Random.Range(minY, -minY),
                                    gameObject.transform.position.y);
        }
        if (_currentMovingTime >= _randomMovingTime)
        {
            _currentMovingTime = 0;
            _isMoving = false;
            _currentEnemyMoveState = EnemyMoveStates.Idle;
        }
    }

    private void StrafeMoveState()
    {
        _currentStrafingTime += Time.deltaTime;
        if (!_isStraffing)
        {

        } 
    }



    // -------------------------------------------------------------------------
    // ===================== Target States =====================================
    // -------------------------------------------------------------------------
    private void TargetedState()
    {
        TrackTarget();
        _currentTargetingTime += Time.deltaTime;

        if (_isFiring & _canFire)
        {
            StartCoroutine(Fire());
        }

        if (_currentTargetingTime >= _randomTargetTime)
        {
            _isFiring = !_isFiring;
            _currentTargetingTime = 0;
            _randomTargetTime = UnityEngine.Random.Range(targetedTime / 2, targetedTime);
        }
    }





    // -------------------------------------------------------------------------
    // ===================== PowerUp States ====================================
    // -------------------------------------------------------------------------

    private void UsingPowerUp()
    {
        
        if (!SpawnChecker.isObstructed)
        {
            PowerUpManager.activate(_availablePowerups[UnityEngine.Random.Range(0, 2)], SpawnObject.transform, targetAim);
            _currentEnemyPowerUpUseStates = EnemyPowerUpUseStates.Cooldown;
        }
    }

    private void CoolDownPowerUpState()
    {

        _currentCooldownTime += Time.deltaTime;
        if (_currentCooldownTime >= cooldownTime)
        {
            _currentCooldownTime = 0;
            _currentEnemyPowerUpUseStates = EnemyPowerUpUseStates.UsingPowerUp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentEnemyMoveState = EnemyMoveStates.Idle;
        _currentEnemyTargetStates = EnemyTargetStates.Targeted;
        _currentEnemyPowerUpUseStates = EnemyPowerUpUseStates.Cooldown;

        _isCoolingDown = false;
        _isIdle = false;
        _isMoving = false;
        _isStraffing = false;
        _isFiring = false;
        _canFire = true;

        moveAbleAreaBounds = moveAbleArea.GetComponent<SpriteRenderer>().sprite.bounds;
        minX = moveAbleArea.transform.position.x - moveAbleArea.transform.localScale.x * moveAbleAreaBounds.size.x * 0.5f;
        minY = moveAbleArea.transform.position.y - moveAbleArea.transform.localScale.y * moveAbleAreaBounds.size.y * 0.5f;

        _availablePowerups = GetRandomEnums<PowerUpDefinitions>();
    }


    // Update is called once per frame
    void Update()
    {

        switch (_currentEnemyMoveState)
        {
            case EnemyMoveStates.Idle:
                IdleMoveState();
                break;
            case EnemyMoveStates.Moving:
                MoveMoveState();
                break;
            case EnemyMoveStates.Strafing:
                break;
        }
        

        switch (_currentEnemyTargetStates)
        {
            case EnemyTargetStates.Targeted:
                TargetedState();
                break;
            case EnemyTargetStates.TargetSwitching:
                break;
        }

        switch (_currentEnemyPowerUpUseStates)
        {
            case EnemyPowerUpUseStates.UsingPowerUp:
                UsingPowerUp();
                break;
            case EnemyPowerUpUseStates.Cooldown:
                CoolDownPowerUpState();
                break;
        }

    }

    private void TrackTarget()
    {
        if (Time.time - rotationStartTime < 1)
        {
            Vector2 directionToAimTarget = targetAim.transform.position - transform.position;
            Quaternion targetRotation = Quaternion.Euler(0, 0, Mathf.Atan2(directionToAimTarget.y, directionToAimTarget.x) * Mathf.Rad2Deg);
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
        _canFire = false;
        EnemyBullet firedBullet = Instantiate(bullet, SpawnObject.transform.position, SpawnObject.transform.rotation);
        float angle = (Mathf.Atan2(SpawnObject.transform.up.y, SpawnObject.transform.up.x) * Mathf.Rad2Deg);
        Vector2 firingDirection = new Vector2(Mathf.Cos((angle) * Mathf.Deg2Rad), Mathf.Sin((angle ) * Mathf.Deg2Rad)).normalized;

        firedBullet.Shoot(firingDirection);
        StartCoroutine(FireRateHandler());
        yield return null;
    }

    IEnumerator FireRateHandler()
    {
        float timeToNextFire = 1 / fireRate;
        yield return new WaitForSeconds(timeToNextFire);
        _canFire = true;
    }

    public T[] GetRandomEnums<T>(int count = 3) where T : Enum
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentException("T must be an enumerated type");

        T[] allValues = (T[])Enum.GetValues(typeof(T));

        if (allValues.Length < count)
            throw new ArgumentException($"Enum type {typeof(T).Name} has fewer than {count} values");

        System.Random random = new System.Random();
        return allValues.OrderBy(x => random.Next()).Take(count).ToArray();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public bool isObstructed;
    public Transform followObject;
    public float flashTime = 1f;
    public float flashRate = 5f;
    public SpriteRenderer renderer;
    public bool flashable;

    [SerializeField] private int _obstructionCount;
    [SerializeField] private bool canFlash;
    [SerializeField] private float _currentFlashTime;
    [SerializeField] private bool _flashing;


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("I ran ");
        Debug.Log(collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Walls":
                _obstructionCount++;
                break;
            case "Drops":
                _obstructionCount++;
                break;
            default:
                break;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Walls":
                _obstructionCount--;
                break;
            case "Drops":
                _obstructionCount--;
                break;
            default:
                break;
        }
    }

    public IEnumerator Flashing()
    {
        canFlash = false;
        StartCoroutine(FlashRateHandler());

        yield return null;
    }

    IEnumerator FlashRateHandler()
    {
        float timeToNextSwitch = 1 / flashRate;
        renderer.enabled = true;
        yield return new WaitForSeconds(timeToNextSwitch);
        renderer.enabled = false;
        yield return new WaitForSeconds(timeToNextSwitch);
        canFlash = true;
    }


    public bool CanSpawn { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        isObstructed = false;
        canFlash = true;
        _flashing = false;
        renderer.enabled = false;
        _obstructionCount = 0;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _flashing = true;

        if (_flashing) FLashingState();

        if (canFlash && _flashing)
        {
            StartCoroutine(Flashing());
        }
        
        isObstructed = _obstructionCount > 0;
        transform.position = followObject.position;
    }

    public void FLashingState()
    {
        _currentFlashTime += Time.deltaTime;
        if (_currentFlashTime >= flashTime)
        {
            _currentFlashTime = 0;
            _flashing = false;
        }
    }
}

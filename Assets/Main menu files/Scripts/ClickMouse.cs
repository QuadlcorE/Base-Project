using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMouse : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public MeshRenderer mr;
    public GameObject TimelineObj;
    public CinemachineVirtualCamera CameraVirtual;
    public GameObject eventManager;

    private EventHandler eventHandlerScript;
    private CinemachinePOV cameraVirtualPOV;
    private TimelineControler timelineControllerScript;

    public enum Buttons
    {
        Start,
        Settings,
        Exit,
        Marketplace,
        About
    }

    public Buttons CurrentButton;

    void Start () {
        timelineControllerScript = TimelineObj.GetComponent<TimelineControler>();
        cameraVirtualPOV = CameraVirtual.GetComponent<CinemachinePOV>();
        eventHandlerScript = eventManager.GetComponent<EventHandler>();
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log($" Clicked {gameObject.name}");
        // freeLook.enabled = false;
        ButtonClicked();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Code
        mr.material.color = Color.cyan;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mr.material.color = Color.green;
        // Debug.Log($"You are pointing at {gameObject.name}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mr.material.color = Color.white;
        // Code
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Code
        mr.material.color = Color.white;
    }

    public void ButtonClicked()
    {
        switch (CurrentButton)
        {
            case Buttons.Start:
                // Debug.Log("I got started");
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                // TODO: Disable Mouse input.
                if (timelineControllerScript != null) timelineControllerScript.Play();
                StartCoroutine(eventHandlerScript.StartChangeScene(timelineControllerScript.PlayDuration()));
                break;
            case Buttons.About:
                break;
            case Buttons.Marketplace:
                break;
            case Buttons.Exit:
                break;
        }
    }
}

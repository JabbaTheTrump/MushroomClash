using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraSystem : MonoBehaviour
{
    public int edgeScrollSize = 20;
    public InputAction inputAction;
    public float moveSpeed = 50f;
    private Vector2 moveDir;
    private Vector3 v3Dir;

    private void Update()
    {
        HandleKeyScrolling();
        HandleEdgeScrolling();
        transform.position += v3Dir * moveSpeed * Time.deltaTime;
    }

    private void HandleEdgeScrolling()
    {
        if (MouseSelectSystem.instance.controlsEnabled)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();

            if (mousePos.x < edgeScrollSize) v3Dir.x = -1f;
            if (mousePos.y < edgeScrollSize) v3Dir.y = -1f;
            if (mousePos.x > Screen.width - edgeScrollSize) v3Dir.x = 1f;
            if (mousePos.y > Screen.height - edgeScrollSize) v3Dir.y = 1f;
        }
    }

    private void HandleKeyScrolling()
    {
        if (MouseSelectSystem.instance.controlsEnabled)
        {
            moveDir = inputAction.ReadValue<Vector2>();
            v3Dir = moveDir;
        }
    }

    private void OnEnable() {
        inputAction.Enable();
    }

    private void OnDisable() {
        inputAction.Disable();
    }
}

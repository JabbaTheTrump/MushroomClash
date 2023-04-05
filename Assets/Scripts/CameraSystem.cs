using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraSystem : MonoBehaviour
{
    public InputAction inputAction;
    public float moveSpeed = 50f;
    private Vector2 moveDir;

    private void Update()
    {
        moveDir = inputAction.ReadValue<Vector2>();
        Vector3 v3Dir = moveDir;
        transform.position += v3Dir * moveSpeed * Time.deltaTime;
    }

    private void OnEnable() {
        inputAction.Enable();
    }

    private void OnDisable() {
        inputAction.Disable();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    private Camera mainCamera;

    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent<Vector2> OnMoveBody = new UnityEvent<Vector2>();
    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();


    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        GetBodyMovement();
        GetTurretMovement();
        GetShootingInput();
    }

    private void GetBodyMovement() {
        Vector2 movementVector = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnMoveBody?.Invoke(movementVector.normalized);
        
    }

    private void GetTurretMovement(){
        OnMoveTurret?.Invoke(GetMousePosition());
    }

    private void GetShootingInput(){
        if (Input.GetMouseButtonDown(0)){
            OnShoot?.Invoke();
        }
    }

    private Vector2 GetMousePosition(){
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;
    }
}

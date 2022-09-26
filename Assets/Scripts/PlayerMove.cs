using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private Input _input;
    private NavMeshAgent _agent;
    private Camera _mainCamera;
    private float _navDistance;
    
    [Header("Private fields")]
    [SerializeField]
    private Vector2 _mouseMovement;

    private void Awake()
    {
        _input = new Input();
   
       // _input.Player.Move.canceled -= ReadMovement
        _input.Player.Look.performed += ReadMovement;
        _input.Player.Fire.started += ReadClick;

        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _input.Player.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        //_cam = GetComponent<Camera>();
    }

    private void ReadMovement(InputAction.CallbackContext context)
    {
        _mouseMovement = context.ReadValue<Vector2>();
    }

    private void ReadClick(InputAction.CallbackContext context)
    {
        Ray ray = _mainCamera.ScreenPointToRay(_mouseMovement);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green, 3);
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.transform.gameObject.name);
                _agent.SetDestination(hit.point);
            }
        }
    }

}

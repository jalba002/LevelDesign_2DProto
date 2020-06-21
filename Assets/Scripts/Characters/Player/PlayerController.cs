using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    public PlayerProperties playerProperties;

    [Header("Raycast")] public LayerMask groundLayer;
    public GameObject originPosition;
    public float radius = 0.5f;
    public float raycastRange = 0.5f;

    public bool OnGround { get; set; }
    private Coroutine OnGroundCoroutine;

    private void Awake()
    {
        if (characterProperties != null)
        {
            characterProperties = Instantiate(characterProperties);
        }

        if (stateMachine == null)
        {
            stateMachine = GetComponent<StateMachine>();
        }

        if (attachedCollider == null)
        {
            attachedCollider = GetComponent<Collider>();
        }
    }

    private void Start()
    {
        // enableAirControl = true;
        stateMachine.SwitchState<State_Player_Walk>();
        OnGroundCoroutine = StartCoroutine(CheckGround());
    }

    void Update()
    {
        // TODO Disable state machine when game pauses. 
        // Access with events and disable StateMachine.
        // DO NOT REFERENCE GAMEMANAGER FROM HERE.
        if (stateMachine.isActiveAndEnabled)
        {
            stateMachine.UpdateTick(Time.deltaTime);
        }

        if (transform.position.y <= -10f)
        {
            this.gameObject.transform.position = FindObjectOfType<SpawnPoint>().transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (stateMachine.isActiveAndEnabled)
        {
            stateMachine.FixedUpdateTick(Time.fixedTime);
        }
    }

    IEnumerator CheckGround()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            OnGround = Physics.SphereCast(originPosition.transform.position, radius, Vector3.down * raycastRange,
                out var hitInfo, groundLayer);
        }
    }

    public void KillPlayer()
    {
        if (IsDead) return;

        IsDead = true;
        stateMachine.enabled = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using Characters.Generic;
using Player;
using UnityEngine;

public class State_Player_Roll : State
{
    private PlayerController attachedController;

    private float currentTimer;
    private float originalDrag;
    private float addedForce;

    protected override void OnStateInitialize(StateMachine machine)
    {
        base.OnStateInitialize(machine);
        attachedController = ((PlayerController) Machine.characterController);
    }

    public override void OnStateTick(float deltaTime)
    {
        base.OnStateTick(deltaTime);
        if (currentTimer > 0f)
        {
            currentTimer -= deltaTime;
        }
        attachedController.rigidbody.AddForce(new Vector3(addedForce,0,0), ForceMode.Acceleration);
    }

    public override void OnStateFixedTick(float fixedTime)
    {
        base.OnStateFixedTick(fixedTime);
    }

    public override void OnStateCheckTransition()
    {
        base.OnStateCheckTransition();
        if (currentTimer <= 0f)
        {
            Machine.SwitchState<State_Player_Walk>();
        }
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        
        // attachedController.rigidbody.velocity = new Vector3(0f, attachedController.rigidbody.velocity.y, 0f);

        originalDrag = attachedController.rigidbody.drag;
        attachedController.rigidbody.drag = 0f;
        
        currentTimer = attachedController.playerProperties.dashDuration;
        
        if (Input.GetKey(KeyCode.D))
        {
            addedForce = attachedController.playerProperties.dashSpeed;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            addedForce = -attachedController.playerProperties.dashSpeed;
        }
        else
        {
            currentTimer = 0f;
        }
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        attachedController.rigidbody.drag = originalDrag;
    }
}
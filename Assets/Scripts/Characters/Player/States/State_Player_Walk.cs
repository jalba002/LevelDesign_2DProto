using System.Collections;
using System.Collections.Generic;
using Characters.Generic;
using Player;
using UnityEngine;

public class State_Player_Walk : State
{
    private PlayerController attachedController;

    protected override void OnStateInitialize(StateMachine machine)
    {
        base.OnStateInitialize(machine);
        attachedController = ((PlayerController) Machine.characterController);
    }

    public override void OnStateTick(float deltaTime)
    {
        base.OnStateTick(deltaTime);
        
        if (Input.GetKey(KeyCode.D))
        {
            attachedController.rigidbody.AddForce(
                attachedController.playerProperties.WalkSpeed, 0, 0,
                ForceMode.Acceleration);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            attachedController.rigidbody.AddForce(
                -attachedController.playerProperties.WalkSpeed, 0, 0,
                ForceMode.Acceleration);
        }
        attachedController.rigidbody.velocity = Vector3.ClampMagnitude(attachedController.rigidbody.velocity, attachedController.playerProperties.maxSpeed);

    }

    public override void OnStateFixedTick(float fixedTime)
    {
        base.OnStateFixedTick(fixedTime);
    }

    public override void OnStateCheckTransition()
    {
        base.OnStateCheckTransition();
       
        if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            Machine.SwitchState<State_Player_PlatformDrop>();
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Machine.SwitchState<State_Player_Jump>();
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Machine.SwitchState<State_Player_Roll>();
            return;
        }
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
    }
}
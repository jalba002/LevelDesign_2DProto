using System.Collections;
using System.Collections.Generic;
using Characters.Generic;
using Player;
using UnityEngine;

public class State_Player_Jump : State
{
    private PlayerController attachedController;

    private bool doubleJumped = false;

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

        if (!doubleJumped && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            Jump();
            doubleJumped = true;
        }

        attachedController.rigidbody.velocity = Vector3.ClampMagnitude(attachedController.rigidbody.velocity,
            attachedController.playerProperties.maxSpeed);
    }

    public override void OnStateFixedTick(float fixedTime)
    {
        base.OnStateFixedTick(fixedTime);
    }

    public override void OnStateCheckTransition()
    {
        base.OnStateCheckTransition();
        // If the player is touching ground.
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Machine.SwitchState<State_Player_Roll>();
            return;
        }

        if (attachedController.OnGround)
        {
            Machine.SwitchState<State_Player_Walk>();
            return;
        }
    }

    private void Jump()
    {
        Machine.characterController.rigidbody.AddForce(
            0, attachedController.playerProperties.JumpSpeed, 0,
            ForceMode.Impulse);
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        Jump();
        doubleJumped = false;
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
    }
}
using System.Collections;
using System.Collections.Generic;
using Characters.Generic;
using Player;
using UnityEngine;

public class State_Player_PlatformDrop : State
{
    private PlayerController attachedController;

    private Ray newRay;
    private Collider ignoredCollider;

    private float currentDuration;

    protected override void OnStateInitialize(StateMachine machine)
    {
        base.OnStateInitialize(machine);
        attachedController = ((PlayerController) Machine.characterController);
    }

    public override void OnStateTick(float deltaTime)
    {
        base.OnStateTick(deltaTime);
        if (currentDuration > 0f)
        {
            currentDuration -= deltaTime;
        }
    }

    public override void OnStateFixedTick(float fixedTime)
    {
        base.OnStateFixedTick(fixedTime);
    }

    public override void OnStateCheckTransition()
    {
        base.OnStateCheckTransition();
        if (currentDuration <= 0f)
        {
            Machine.SwitchState<State_Player_Walk>();
        }
    }

    protected override void OnStateEnter()
    {
        base.OnStateEnter();
        newRay = new Ray(attachedController.originPosition.transform.position, Vector3.down);
        if (Physics.Raycast(newRay, out var hitInfo, attachedController.raycastRange, attachedController.groundLayer))
        {
            ignoredCollider = hitInfo.collider;
            IgnoreCollider(ignoredCollider, true);
            currentDuration = 0.5f;
            attachedController.rigidbody.AddForce(0, -attachedController.playerProperties.dashSpeed, 0f,
                ForceMode.Impulse);
        }
        else
        {
            currentDuration = 0f;
        }
    }

    private void IgnoreCollider(Collider hitCollider, bool enable)
    {
        Physics.IgnoreCollision(attachedController.attachedCollider, hitCollider, enable);
    }

    protected override void OnStateExit()
    {
        base.OnStateExit();
        if (ignoredCollider != null)
            IgnoreCollider(ignoredCollider, false);
    }
}
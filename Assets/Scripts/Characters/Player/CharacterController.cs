using System.Collections;
using System.Collections.Generic;
using Characters.Generic;
using Properties;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    [Header("Required Components")] 
    public Rigidbody rigidbody;

    public Collider attachedCollider;
    
    public StateMachine stateMachine { get; protected set; }
        
    public bool IsDead { get; protected set; }

    [System.NonSerialized]
    public CharacterProperties characterProperties;
}

using System.Collections;
using System.Collections.Generic;
using Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "Data_Player_Properties", menuName = "Data/Properties/Player Properties", order = 1)]
public class PlayerProperties : CharacterProperties
{
    public float JumpSpeed = 5f;

    [Space(5)] public float gravity = 10f;
    public float maxVerticalSpeed = 5f;

    [Header("Rigidbody Settings")] public float maxSpeed = 5f;

    [Header("Dash Settings")] public float dashDuration = 0.5f;
    public float dashSpeed = 8f;

    [Header("Drop Settings")] public float dropDuration = 0.25f;
    public float dropSpeed = 4f;
}

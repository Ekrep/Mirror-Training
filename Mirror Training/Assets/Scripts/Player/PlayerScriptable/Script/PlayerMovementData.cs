using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="PlayerScriptables/PlayerMovementData",fileName ="PlayerMovementData")]
public class PlayerMovementData : ScriptableObject
{
    public float movementSpeed;
    public float rotateSpeed;
    public float jumpForce;
    public float jumpSpeed;
    public int jumpAmount;
    public float jumpCooldown;
    public float groundedCheckRaycastLength;
   
}

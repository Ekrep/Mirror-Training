using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseStateMachine : PlayerStateMachine
{

    [HideInInspector]
    public PlayerMovingState movingState;
    [HideInInspector]
    public PlayerJumpingState jumpingState;
    [HideInInspector]
    public PlayerFallingState fallingState;
    //[HideInInspector]
    //public PlayerMovingState movingState;
    //[HideInInspector]
    //public PlayerMovingState movingState;    
    public Rigidbody rb;    
    public float jumpForce;    
    public Animator anim;

    public float movementSpeed;
    public float rotateSpeed;

    private void Awake()
    {
        movingState = new PlayerMovingState(this);
        jumpingState = new PlayerJumpingState(this);
        fallingState = new PlayerFallingState(this);
    }

    private void OnEnable()
    {
        //this line must be change!!!
        transform.position = SpawnManager.Instance.allSpawnPoints[GetRandomSpawnPoint()].position;
    }


    //start or update functions are does not working on this class!!


    protected override PlayerBaseState GetInitialState()
    {       
        return movingState;
    }

    private int GetRandomSpawnPoint()
    {
        int randomSpawnPointElement = Random.Range(0, SpawnManager.Instance.allSpawnPoints.Count);
        return randomSpawnPointElement;
    }
}

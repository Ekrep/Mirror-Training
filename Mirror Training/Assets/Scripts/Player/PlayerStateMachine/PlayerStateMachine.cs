using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStateMachine :NetworkBehaviour 
{
    public PlayerBaseState currentState;


    private void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
        {           
            currentState.Enter();
        }
    }
    private void FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.PhysicsUpdate();
        }
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.LogicUpdate();
            Debug.Log(currentState.stateName);
        }
    }


    public void ChangeState(PlayerBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();//check this maybe this causes bug!!
    }




    protected virtual PlayerBaseState GetInitialState()
    {
        
        return null;
    }
}

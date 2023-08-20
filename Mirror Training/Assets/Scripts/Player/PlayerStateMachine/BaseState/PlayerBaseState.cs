using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState
{

    public string stateName;

    protected PlayerStateMachine stateMachine;


    public PlayerBaseState(string name,PlayerStateMachine stateMachine)
    {
        stateName = name;
        this.stateMachine = stateMachine;
    }


    public virtual void Enter()
    {

    }
    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {


    }

    public virtual void Exit()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerInAirState
{
    public PlayerFallingState(string name,PlayerBaseStateMachine playerBaseStateMachine) : base(name,playerBaseStateMachine)
    {


    }

    public override void Enter()
    {
        base.Enter();
        sm.anim.SetBool("IsFalling", true);
        sm.statusControlCubeMeshRenderer.material.color = Color.red;
        

    }
   
    public override void Exit()
    {
        base.Exit();
        sm.anim.SetBool("IsFalling", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInAirState : PlayerAliveState
{
    public PlayerInAirState(string name,PlayerBaseStateMachine playerBaseStateMachine) : base(name,playerBaseStateMachine)
    {

    }

   
    public override void LogicUpdate()
    {
        
        //not sure
        base.LogicUpdate();
        //Debug.Log(sm.playerInput.Player.Jump.phase);
        //bugging check this!!
        if (sm.playerInput.Player.Jump.triggered && sm.jumpAmount > 0&&sm.isLocalPlayer)
        {
            sm.ChangeState(sm.jumpingState);
            
        }

    }

}

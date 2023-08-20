using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerInAirState
{
    public PlayerFallingState(PlayerBaseStateMachine playerBaseStateMachine) : base("Falling", playerBaseStateMachine)
    {


    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}

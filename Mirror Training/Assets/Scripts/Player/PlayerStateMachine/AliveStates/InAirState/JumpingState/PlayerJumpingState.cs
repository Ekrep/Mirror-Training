using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerInAirState
{
    public PlayerJumpingState(PlayerBaseStateMachine playerBaseStateMachine) : base("Jumping", playerBaseStateMachine)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAliveState : PlayerBaseState
{

    protected PlayerBaseStateMachine sm;

    public PlayerAliveState(string name, PlayerBaseStateMachine playerStateMachine) : base(name, playerStateMachine)
    {
        sm = (PlayerBaseStateMachine)this.stateMachine;
    }

}

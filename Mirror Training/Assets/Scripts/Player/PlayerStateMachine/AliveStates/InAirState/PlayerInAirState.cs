using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerAliveState
{
    public PlayerInAirState(string name,PlayerBaseStateMachine playerBaseStateMachine) : base(name, playerBaseStateMachine)
    {

    }
}

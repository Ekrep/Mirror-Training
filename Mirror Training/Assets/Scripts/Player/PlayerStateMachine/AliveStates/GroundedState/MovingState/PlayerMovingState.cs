using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerAliveState//later split it another mainState(Grounded)
{
    public PlayerMovingState(PlayerBaseStateMachine playerBaseStateMachine) : base("Moving", playerBaseStateMachine)
    {
    
    }

    public override void Enter()
    {
        base.Enter();
        
        
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (sm.isLocalPlayer)
        {
            MovementControl();
        }
    }
    public override void Exit()
    {
        base.Exit();
    }

    private void MovementControl()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");



        if (Mathf.Abs(xInput) > 0 || Mathf.Abs(zInput) > 0)
        {
            sm.anim.SetBool("IsWalking", true);
            sm.rb.velocity = new Vector3(sm.movementSpeed * xInput * Time.fixedDeltaTime, sm.rb.velocity.y, sm.movementSpeed * zInput * Time.fixedDeltaTime);
            Vector3 lookRotVector = new Vector3(sm.rb.velocity.x, 0, sm.rb.velocity.z);
            Quaternion lookRot = Quaternion.Lerp(sm.transform.rotation, Quaternion.LookRotation(lookRotVector, sm.transform.up), sm.rotateSpeed * Time.deltaTime);
            sm.transform.rotation = lookRot;
        }
        else
        {
            sm.anim.SetBool("IsWalking", false);
            sm.rb.velocity = new Vector3(0, sm.rb.velocity.y, 0);

        }



    }
}

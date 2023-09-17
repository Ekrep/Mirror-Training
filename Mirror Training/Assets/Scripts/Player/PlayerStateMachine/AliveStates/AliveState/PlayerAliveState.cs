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
    public override void Enter()
    {
        base.Enter();
        sm.namea = sm.currentState.stateName;
        if (sm.isLocalPlayer)
        {
            //sm.ServerTestFunc(sm.currentState.stateName,sm.netId);
            sm.CMDSendPlayerToServerPlayerList(sm, sm.namea);
        }


    }
    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        if (sm.isLocalPlayer)
        {
            sm.anim.SetFloat("Velocity", Mathf.Abs(sm.rb.velocity.x) + Mathf.Abs(sm.rb.velocity.z));
        }
        GroundCheck();
        sm.anim.SetBool("IsInAir", !sm.isGrounded);

    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
       
        
    }

    //check this code later!!
    private void GroundCheck()
    {

        RaycastHit groundedRayHit;
        Debug.DrawRay(sm.transform.position, -sm.transform.up, Color.red);
        if (!Physics.Raycast(new Vector3(sm.transform.position.x, sm.transform.position.y + 0.5f, sm.transform.position.z), -sm.transform.up, out groundedRayHit, sm.groundCheckRaycastLength))
        {
            sm.isGrounded = false;
            if (!sm.isLocalPlayer)
                return;
           
            if (sm.rb.velocity.y < 0 && sm.currentState != sm.fallingState)
            {
                sm.ChangeState(sm.fallingState);
                //Debug.Log("girdim fall statechange");

            }

        }
        else
        {
            sm.isGrounded = true;
            

            //first landing
            if (sm.currentState != sm.movingState && sm.rb.velocity.y == 0)
            {

                
                //Debug.Log("Landed Partical");
                if (!sm.isLocalPlayer)
                    return;
                sm.jumpAmount = 0;
                sm.ChangeState(sm.movingState);
                
                
                


                //Debug.Log("girdim moving statechange");
            }


        }
    }

    public IEnumerator JumpCoolDown()
    {
        if (sm.jumpAmount==0)
        {
            yield return new WaitForSeconds(sm.jumpCooldown);
            sm.jumpAmount = sm.flagJumpAmount;

        }
        
    }
    protected void ChangeStateDelayed(string stateName, float delayTime) 
    {
        sm.StartCoroutine(ChangeStateDelayedIEnumarator(stateName, delayTime));       
    }
    private IEnumerator ChangeStateDelayedIEnumarator(string stateName,float delayTime) 
    {
        yield return new WaitForSeconds(delayTime);
        sm.ChangeState(sm.movingState);
    }

}

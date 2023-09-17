using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerInAirState
{
    public PlayerJumpingState(string name,PlayerBaseStateMachine playerBaseStateMachine) : base(name,playerBaseStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        if (sm.isLocalPlayer)
        {
            EnterProcess();
        }
        sm.PlayParticleAndSetPosition("Jump", sm.transform.position, 0.5f);
        sm.statusControlCubeMeshRenderer.material.color = Color.yellow;

    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        LookRotation();
    }



    public override void Exit()
    {
        base.Exit();
        sm.anim.SetBool("IsJumping", false);

    }

    private void EnterProcess() 
    {
        sm.anim.SetFloat("JumpBlend", sm.jumpAmount);
        sm.anim.SetBool("IsJumping", true);
        if (sm.jumpAmount == 1)
        {
            sm.anim.Play("Jump", 0, 0f);


        }
        //Debug.Log("Jumpstate Girdi");
        //sm.anim.SetFloat("JumpBlend", sm.jumpAmount);
        JumpProcess();
        //sm.rb.AddForce(Vector3.up*sm.jumpForce*Time.fixedDeltaTime, ForceMode.Impulse);
        sm.jumpAmount--;
    }

    private void JumpProcess() 
    {
        float xInput = sm.playerInput.Player.Move.ReadValue<Vector2>().x;
        float zInput = sm.playerInput.Player.Move.ReadValue<Vector2>().y;
        sm.rb.velocity = new Vector3(sm.movementSpeed * xInput * Time.fixedDeltaTime, sm.jumpForce * Time.fixedDeltaTime, sm.movementSpeed * zInput * Time.fixedDeltaTime);
        

    }
    private void LookRotation() 
    {
        if (sm.jumpAmount==0&&sm.isLocalPlayer)
        {
            Vector3 lookRotVector = new Vector3(sm.rb.velocity.x, 0, sm.rb.velocity.z);
            Quaternion lookRot = Quaternion.Lerp(sm.transform.rotation, Quaternion.LookRotation(lookRotVector, sm.transform.up), sm.rotateSpeed * Time.deltaTime);
            sm.transform.rotation = lookRot;
        }
        

    }
}

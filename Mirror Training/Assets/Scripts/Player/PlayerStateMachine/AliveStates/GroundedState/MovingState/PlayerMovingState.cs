using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovingState : PlayerAliveState//later split it to another mainState(Grounded)
{

    public PlayerMovingState(PlayerBaseStateMachine playerBaseStateMachine) : base("Moving", playerBaseStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        sm.statusControlCubeMeshRenderer.material.color = Color.black;
        // must be change later!!
        sm.StartCoroutine(JumpCoolDown());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (sm.isLocalPlayer)
        {
            MovementControl();
            if (sm.playerInput.Player.Jump.triggered && sm.jumpAmount > 0)
            {
                sm.ChangeState(sm.jumpingState);

            }
        }
    }



    public override void Exit()
    {
        base.Exit();
        sm.anim.SetBool("IsWalking", false);

    }

    private void MovementControl()
    {


        float xInput = sm.playerInput.Player.Move.ReadValue<Vector2>().x;
        float zInput = sm.playerInput.Player.Move.ReadValue<Vector2>().y;
        Debug.Log(sm.playerInput.Player.Move.IsInProgress());


        sm.rb.velocity = new Vector3(sm.movementSpeed * xInput * Time.fixedDeltaTime, sm.rb.velocity.y, sm.movementSpeed * zInput * Time.fixedDeltaTime);
        if (Mathf.Abs(xInput) > 0 || Mathf.Abs(zInput) > 0)
        {
            
            sm.anim.SetBool("IsWalking", true);
            //NetworkTime.rtt
            Vector3 lookRotVector = new Vector3(sm.rb.velocity.x, 0, sm.rb.velocity.z);
            Quaternion lookRot = Quaternion.Lerp(sm.transform.rotation, Quaternion.LookRotation(lookRotVector, sm.transform.up), sm.rotateSpeed * Time.deltaTime);
            sm.transform.rotation = lookRot;
        }
        if (Mathf.Abs(sm.rb.velocity.x) == 0 && Mathf.Abs(sm.rb.velocity.z) == 0)
        {



            sm.anim.SetBool("IsWalking", false);
            //check this it's bugging!!

        }





    }







}

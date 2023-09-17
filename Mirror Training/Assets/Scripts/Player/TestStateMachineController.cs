using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TestStateMachineController : MonoBehaviour
{
    public PlayerBaseStateMachine stateMachine;
    public string currentStateName;

    bool isGrounded;
    void Start()
    {

    }
    private void FixedUpdate()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        //currentStateName = stateMachine.currentState.stateName;
        if (!stateMachine.isLocalPlayer)
        {
            /*
            //CheckGround();
            if (stateMachine.anim.GetBool("IsJumping") && currentStateName != "Jump")
            {
                currentStateName = "Jump";
                stateMachine.PlayParticleAndSetPosition("Jump", transform.position, 0.5f);
            }
            if (stateMachine.anim.GetBool("IsFalling")&&currentStateName!= "Fall")
            {
                currentStateName = "Fall";
                stateMachine.PlayParticleAndSetPosition("FallGround", transform.position, 0.5f);
            }
            if (stateMachine.anim.GetBool("IsWalking") && currentStateName != "Walking")
            {
                currentStateName = "Walking";
            }*/

        }
    }

    private void CheckGround()
    {
        RaycastHit groundedRayHit;
        Debug.DrawRay(transform.position, -transform.up, Color.red);
        if (!Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), -transform.up, out groundedRayHit, stateMachine.groundCheckRaycastLength))
        {
            isGrounded = false;
            if (!stateMachine.anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                stateMachine.ChangeState(stateMachine.fallingState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.jumpingState);
            }

        }
        else
        {
            isGrounded = true;
            stateMachine.ChangeState(stateMachine.movingState);
        }
    }
}

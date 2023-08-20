using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerControl : NetworkBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Animator anim;

    public float movementSpeed;
    public float rotateSpeed;

    private void OnEnable()
    {
        transform.position = SpawnManager.Instance.allSpawnPoints[GetRandomSpawnPoint()].position;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            MovementControl();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime);
            }

        }

    }

    private int GetRandomSpawnPoint()
    {
        int randomSpawnPointElement = Random.Range(0, SpawnManager.Instance.allSpawnPoints.Count);
        return randomSpawnPointElement;
    }

    private void MovementControl()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");


       
        if (Mathf.Abs(xInput) > 0 || Mathf.Abs(zInput) > 0)
        {
            anim.SetBool("IsWalking", true);
            rb.velocity = new Vector3(movementSpeed * xInput * Time.fixedDeltaTime, rb.velocity.y, movementSpeed * zInput * Time.fixedDeltaTime);
            Vector3 lookRotVector = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            Quaternion lookRot = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(lookRotVector, transform.up),rotateSpeed*Time.deltaTime);
            transform.rotation = lookRot;
        }
        else
        {
            anim.SetBool("IsWalking", false);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
           
        }
        


    }

    
}

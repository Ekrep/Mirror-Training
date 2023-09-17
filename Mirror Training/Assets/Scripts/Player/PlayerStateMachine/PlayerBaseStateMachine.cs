using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;


public class PlayerBaseStateMachine : PlayerStateMachine
{
    //current bugs because of program cannot sync the states of players.
    //Add network rigidbody and control from the main class

    public string namea;
    public PlayerInput playerInput;
    [SerializeField]
    private PlayerMovementData playerData;
    [SerializeField]
    private PlayerParticleData playerParticleData;

    public Dictionary<string, PlayerBaseState> playerStates = new Dictionary<string, PlayerBaseState>();

    public Dictionary<string, NetworkParticles> particles = new Dictionary<string, NetworkParticles>();

    public List<ParticleSystem> testList;

    [HideInInspector]
    public PlayerMovingState movingState;
    [HideInInspector]
    public PlayerJumpingState jumpingState;
    [HideInInspector]
    public PlayerFallingState fallingState;
    //[HideInInspector]
    //public PlayerMovingState movingState;    
    public Rigidbody rb;
    public Animator anim;
    public MeshRenderer statusControlCubeMeshRenderer;
    public Transform groundControlPoint;


    [HideInInspector]
    public float jumpForce;
    [HideInInspector]
    public int jumpAmount;
    [HideInInspector]
    public float jumpSpeed;
    [HideInInspector]
    public float movementSpeed;
    [HideInInspector]
    public float rotateSpeed;
    [HideInInspector]
    public float groundCheckRaycastLength;
    [HideInInspector]
    public int flagJumpAmount;
    [HideInInspector]
    public float jumpCooldown;




    public bool isGrounded;

    private void Awake()
    {
        movingState = new PlayerMovingState(this);
        jumpingState = new PlayerJumpingState("Jumping", this);
        fallingState = new PlayerFallingState("Falling", this);//subState of Air
        InitilazePlayerStatesDictionary();
        InitilazePlayerData();

        //For The Player Inputs
        playerInput = new PlayerInput();
        playerInput.Player.Enable();

        //for the playerEffects








    }

    private void OnEnable()
    {
        //this line must be change later!!!
        transform.position = SpawnManager.Instance.allSpawnPoints[GetRandomSpawnPoint()].position;

    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (isLocalPlayer)
        {
            GameManager.Instance.myPlayer = gameObject;
            GameManager.Instance.PlayerAdded();

        }
        if (isLocalPlayer && isClient)
        {
            //CMDSendPlayerToServerPlayerList(this);
        }
    }


    protected override void CheckConnectionType()
    {
        base.CheckConnectionType();
        if (isClient)
        {
            //Debug.Log("clientttt");

            SetParticlesReadyForGameFromClient();
        }

    }

    public void TestSpawn()
    {

    }

    //start or update functions are does not working on this class!!


    protected override PlayerBaseState GetInitialState()
    {

        return movingState;

    }

    private int GetRandomSpawnPoint()
    {
        int randomSpawnPointElement = Random.Range(0, SpawnManager.Instance.allSpawnPoints.Count);
        return randomSpawnPointElement;
    }

    private void InitilazePlayerData()
    {
        movementSpeed = playerData.movementSpeed;
        rotateSpeed = playerData.rotateSpeed;
        jumpForce = playerData.jumpForce;
        groundCheckRaycastLength = playerData.groundedCheckRaycastLength;
        jumpSpeed = playerData.jumpSpeed;
        jumpAmount = playerData.jumpAmount;
        flagJumpAmount = jumpAmount;
        jumpCooldown = playerData.jumpCooldown;

    }

    private void InitilazePlayerStatesDictionary()
    {
        playerStates.TryAdd(movingState.stateName, movingState);
        playerStates.TryAdd(jumpingState.stateName, jumpingState);
        playerStates.TryAdd(fallingState.stateName, fallingState);
    }


    private void SetParticlesReadyForGameFromClient()
    {
        foreach (string item in playerParticleData.playerParticles.Keys)
        {

            NetworkParticles ps = Instantiate(playerParticleData.playerParticles[item]);
            ps.gameObject.SetActive(false);
            particles.TryAdd(item, ps);

            testList.Add(particles[item].particle);
            //NetworkServer.Spawn(ps.gameObject);

        }
    }



    public void PlayParticleAndSetPosition(string particleName, Vector3 particlePosition, float particleDisableTime)
    {
        particles[particleName].transform.position = particlePosition;
        particles[particleName].gameObject.SetActive(true);
        particles[particleName].particle.Play();
        StartCoroutine(DisableParticle(particleDisableTime, particles[particleName].particle));
    }

    public IEnumerator DisableParticle(float disableTime, ParticleSystem particleSystem)
    {
        yield return new WaitForSeconds(disableTime);
        particleSystem.Stop();
        particleSystem.gameObject.SetActive(false);

    }


    //with this attribute you can call this function on client to server
    [Command]
    public void ServerTestFunc(string sendString, uint netid)
    {

        Debug.Log(sendString + "" + netId);


    }

    //Client sends itself to Server 
    [Command]
    public void CMDSendPlayerToServerPlayerList(PlayerBaseStateMachine playerBaseStateMachine, string name)
    {

        //GameManager.Instance.players.Add(playerBaseStateMachine);


        //Debug.Log("added");
        if (isServer)
        {
            //RPCSendPlayerListToClients(GameManager.Instance.players);
            RPCChangeState(playerBaseStateMachine, name);
        }


    }

    // Server Replys to Clients (next i must add the all states in dictionary after that i control the states from that dictionary)
    [ClientRpc]
    private void RPCChangeState(PlayerBaseStateMachine playerBaseStateMachine, string name)
    {
        if (!isLocalPlayer)
        {
            playerBaseStateMachine.namea = name;
            playerBaseStateMachine.ChangeState(playerBaseStateMachine.playerStates[name]);
        }

    }


    [ClientRpc]
    private void RPCSendPlayerListToClients(List<PlayerBaseStateMachine> playerBaseStateMachines)
    {
        GameManager.Instance.players = playerBaseStateMachines;
        /* for (int i = 0; i < GameManager.Instance.players.Count; i++)
         {
             if (GameManager.Instance.players[i] == GameManager.Instance.myPlayer.GetComponent<PlayerBaseStateMachine>())
             {
                 GameManager.Instance.players.Remove(GameManager.Instance.players[i]);
             }
         }*/

    }

}

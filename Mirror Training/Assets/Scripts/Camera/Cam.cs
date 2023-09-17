using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Mirror;
using UnityEngine.InputSystem;
public class Cam : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera cinemachineCam;

    public GameObject myPlayer;

    private Vector3 followOffset;

    public PlayerInput inputs;

    private void Awake()
    {
        inputs = new PlayerInput();
        inputs.Enable();
    }
    private void OnEnable()
    {
        GameManager.OnPlayerAdded += GameManager_OnPlayerAdded;
    }

    private void GameManager_OnPlayerAdded()
    {
        cinemachineCam.Follow = GameManager.Instance.myPlayer.transform;
        cinemachineCam.LookAt = GameManager.Instance.myPlayer.GetComponent<PlayerReferences>().playerCamPoint;
        //cinemachineCam.m_Follow = GameManager.Instance.myPlayer.transform;
        //cinemachineCam.m_LookAt = GameManager.Instance.myPlayer.transform;
        //followOffset=cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        
    }

    private void OnDisable()
    {
        GameManager.OnPlayerAdded -= GameManager_OnPlayerAdded;
    }

    void Start()
    {
        
    }
 


    void Update()
    {
        //Debug.Log(cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset);
        //Axis();
    }


    private void Axis() 
    {

        
        //cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        if (inputs.Player.MouseRightClick.IsInProgress())
        {
            float mouseDeltaX = inputs.Player.MouseDelta.ReadValue<Vector2>().x;
            followOffset = new Vector3(followOffset.x + mouseDeltaX, followOffset.y, followOffset.z + mouseDeltaX);
            //cinemachineCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset=followOffset;
        }
    
    }

    
}

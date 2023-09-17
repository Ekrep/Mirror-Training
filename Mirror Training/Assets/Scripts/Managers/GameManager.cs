using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Mirror;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action OnPlayerAdded;

    public List<PlayerBaseStateMachine> players;

    public GameObject myPlayer;

    private void Awake()
    {
        Instance = this;
    }


    public void PlayerAdded() 
    {
        if (OnPlayerAdded!=null)
        {
            OnPlayerAdded();
        }
    }

}

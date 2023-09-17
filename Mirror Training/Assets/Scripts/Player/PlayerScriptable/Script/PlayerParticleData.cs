using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(menuName = "PlayerScriptables/PlayerParticleEffectData")]

public class PlayerParticleData : SerializedScriptableObject
{
    public Dictionary<string, NetworkParticles> playerParticles;

    
}

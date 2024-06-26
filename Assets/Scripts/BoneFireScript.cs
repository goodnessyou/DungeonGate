using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneFireScript : MonoBehaviour
{
    public GameLogic gameLogic;
    public PlayerLogic playerLogic;
    public Transform ReSpawnPosition;


    public void UseBoneFire()
    {
        gameLogic.RestOnFire();
    }
}

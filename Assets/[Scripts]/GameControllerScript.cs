using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public Transform playerTranform;
    public Transform playerSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerTranform.position = playerSpawnPoint.position;
    }

}

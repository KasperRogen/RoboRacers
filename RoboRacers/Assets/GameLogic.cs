using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;

public class GameLogic : MonoBehaviour
{
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        if(NetworkManager.Instance.IsServer)
        {
            Canvas.SetActive(false);
            return;
        }
        NetworkManager.Instance.InstantiatePlayerCards();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BeardedManStudios.Forge.Networking.Unity;

public class GameLogic : MonoBehaviour
{
    public GameObject Canvas;
    public List<GameObject> SpawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        if(NetworkManager.Instance.IsServer)
        {
            Canvas.SetActive(false);
            return;
        }

        NetworkManager.Instance.InstantiatePlayerCards(0, SpawnPoints[Random.Range(0, SpawnPoints.Count)].transform.position, Quaternion.identity, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

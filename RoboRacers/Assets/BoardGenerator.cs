using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{

    public List<GameObject> Tiles;
    public int boardWidth;
    public int boardHeight;
    public float calculatedBoardWidth;
    public float calculatedBoardHeight;
    public Vector3 boardCenter;
    public List<GameObject> Players;


    // Start is called before the first frame update
    void Start()
    {
        float tileSize = Tiles[0].GetComponent<BoxCollider>().size.x;

        calculatedBoardWidth = (tileSize * boardWidth);
        calculatedBoardHeight = (tileSize * boardHeight);

        boardCenter = transform.position - new Vector3(calculatedBoardWidth - tileSize, 0, calculatedBoardHeight - tileSize) / 2;

        for (int i = 0; i < boardWidth; i++)
        {
            for(int j = 0; j < boardHeight; j++)
            {

                Vector3 pos = boardCenter;

                pos.x += Tiles[0].GetComponent<BoxCollider>().size.x * i;
                pos.z += Tiles[0].GetComponent<BoxCollider>().size.z * j;

                Instantiate(Tiles[0], pos, Quaternion.identity, transform);
            }
        }

        Instantiate(Players[0], transform.GetChild(0).transform.position + Vector3.up * 0.5f, Quaternion.identity);

    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}

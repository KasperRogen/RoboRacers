using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject Board;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        BoardGenerator generator = Board.GetComponent<BoardGenerator>();
        Vector3 bounds = new Vector3(generator.calculatedBoardWidth, 0, generator.calculatedBoardHeight);
        bounds += Vector3.one;
        float distance = Mathf.Max(bounds.x, bounds.y, bounds.z);
        distance /= (2.0f * Mathf.Tan(0.5f * GetComponent<Camera>().fieldOfView * Mathf.Deg2Rad));
        transform.position = new Vector3(transform.position.x, distance, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

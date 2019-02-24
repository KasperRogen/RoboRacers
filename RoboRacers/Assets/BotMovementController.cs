using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovementController : MonoBehaviour
{

    Vector3 targetPosition, targetRotation;
    PlayerManager playerManager;
    public bool IsDone = false;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>().botControllers.Add(this);
    }

    // Update is called once per frame
    void update()
    {

                //Vector3 newDir = Vector3.RotateTowards(transform.TransformDirection(Vector3.forward), currentTarget, 1 * Time.deltaTime, 0.1f);
                //transform.rotation = Quaternion.LookRotation(newDir);

    }



    public void CalculateNewRotation(int degrees)
    {
        IsDone = false;
        targetRotation = transform.TransformDirection(Vector3.forward);
        targetRotation = Quaternion.AngleAxis(degrees, Vector3.up) * targetRotation;
        StartCoroutine(Rotate());
    }

    public void CalculateNewMove(int spaces)
    {
        IsDone = false;

        targetPosition = transform.position;

        RaycastHit hit;
        for (int i = 1; i <= spaces; i++)
        {
            if (Physics.Raycast(transform.position + transform.TransformDirection(Vector3.forward) * i, Vector3.down, out hit))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 1);
                targetPosition = hit.transform.position;
                targetPosition.y = transform.position.y;
            }
        }
        StartCoroutine(Move());
    }


    IEnumerator Rotate()
    {
        Quaternion a = transform.rotation;
        Quaternion b = Quaternion.Euler(targetRotation);

        while (Vector3.Angle(transform.TransformDirection(Vector3.forward), targetRotation) > 0)
        {
            yield return new WaitForEndOfFrame();
            Vector3 newDir = Vector3.RotateTowards(transform.TransformDirection(Vector3.forward), targetRotation, 1 * Time.deltaTime, 0.1f);
            transform.rotation = Quaternion.LookRotation(newDir);
            Debug.Log(Vector3.Angle(transform.TransformDirection(Vector3.forward), targetRotation));
        }
        yield return new WaitForSeconds(1);
        IsDone = true;
    }

    IEnumerator Move()
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0)
        {
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 1 * Time.deltaTime);
        }
        yield return new WaitForSeconds(1);
        IsDone = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Movement Card", menuName = "Cards/New Movement Card")]
public class MovementCard : Card
{
    [Header("TYPE")]
    public MovementType movementType;
    [Space(20)]

    [Header("MOVEMENT")]
    public int spaces;
    [Space(20)]

    [Header("ROTATION")]
    public RotationDirection rotationDirection;
    public int rotationAmount;
    

    Vector3 currentTarget;
    GameObject GO;

    public override void Execute(GameObject _GO)
    {
        IsDone = false;
        GO = _GO;

        switch (movementType)
        {
            case MovementType.MOVE:
                    currentTarget = GO.transform.position + GO.transform.TransformDirection(Vector3.forward) * spaces;
            break;

            case MovementType.ROTATE:
                switch (rotationDirection)
                {
                    case RotationDirection.LEFT:
                        currentTarget = GO.transform.TransformDirection(Vector3.forward);
                        currentTarget = Quaternion.AngleAxis(-rotationAmount, Vector3.up) * currentTarget;
                        break;

                    case RotationDirection.RIGHT:
                        currentTarget = GO.transform.TransformDirection(Vector3.forward);
                        currentTarget = Quaternion.AngleAxis(rotationAmount, Vector3.up) * currentTarget;
                        break;
                }
                
            break;
        }
    }

    

    public override void Tick()
    {

        
        switch (movementType)
        {

            case MovementType.MOVE:

                GO.transform.position = Vector3.MoveTowards(GO.transform.position, currentTarget, 0.01f);
                
                if (GO.transform.position == currentTarget)
                {
                    IsDone = true;
                }

            break;

            case MovementType.ROTATE:
                Vector3 newDir = Vector3.RotateTowards(GO.transform.TransformDirection(Vector3.forward), currentTarget, 0.01f, 0.1f);
                GO.transform.rotation = Quaternion.LookRotation(newDir);
                
                if (Vector3.Angle(GO.transform.TransformDirection(Vector3.forward), currentTarget) == 0)
                {
                    IsDone = true;
                }

            break;
        }
    }
}


public enum RotationDirection
{
    LEFT,
    RIGHT
}

public enum MovementType
{
    MOVE,
    ROTATE
}
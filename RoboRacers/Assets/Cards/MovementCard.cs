using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(fileName = "New Movement Card", menuName = "Cards/New Movement Card")]
public class MovementCard : Card
{
    [Header("STUFF")]
    public MovementType movementType;
    public LayerMask BlocksMovementMask;
    [Space(20)]

    [Header("MOVEMENT")]
    public int spaces;
    [Space(20)]

    [Header("ROTATION")]
    public RotationDirection rotationDirection;
    public int rotationAmount;

    public override void Execute(GameObject GO)
    {

        switch (movementType)
        {
            case MovementType.MOVE:
                GO.GetComponent<BotMovementController>().CalculateNewMove(spaces);
            break;

            case MovementType.ROTATE:
                switch (rotationDirection)
                {
                    case RotationDirection.LEFT:
                        GO.GetComponent<BotMovementController>().CalculateNewRotation(-rotationAmount);
                        break;

                    case RotationDirection.RIGHT:
                        GO.GetComponent<BotMovementController>().CalculateNewRotation(+rotationAmount);
                        break;
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
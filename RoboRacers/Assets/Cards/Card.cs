using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Cards/New Card")]
public abstract class Card : ScriptableObject
{
    public string cardName;
    public Sprite icon;

    [HideInInspector]
    public bool IsDone = false;

    public abstract void Execute(GameObject GO);
    public abstract void Tick();
}

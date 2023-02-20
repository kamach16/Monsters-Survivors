using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Power Up")]
public class PowerUp : ScriptableObject
{
    public PowerUpType powerUpType;
    public string powerUpName;
    public string description;
    public Sprite artwork;
}

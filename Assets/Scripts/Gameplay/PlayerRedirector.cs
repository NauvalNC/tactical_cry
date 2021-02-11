using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedirector : MonoBehaviour
{
    public Player character;

    public void Shoot() 
    {
        character.Shoot();
    }
}

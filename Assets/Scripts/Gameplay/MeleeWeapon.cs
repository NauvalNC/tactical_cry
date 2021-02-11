using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float DMG = 50f;
    public int hitMaskIndex = 8;
    public GameObject hitPS;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == hitMaskIndex)
        {
            Character ch = collision.gameObject.GetComponent<Character>();
            if (ch)
            {
                ch.GetHit(DMG);
                Instantiate(hitPS, transform.position, Quaternion.identity);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    public float destroyOnBelow = -6f;

    public virtual void OnSupply() { SFXManager.Instance.Play("button"); }

    private void Update()
    {
        if (transform.position.y <= destroyOnBelow) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            OnSupply();
            Destroy(gameObject);
        }
    }
}

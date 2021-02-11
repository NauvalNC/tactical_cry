using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitPS;
    public float speed = 20f;
    public int hitMaskIndex;
    public float DMG = 10;
    Vector3 direction;
    bool isDeployed = false;

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        transform.localScale = new Vector3(direction.x * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isDeployed = true;
    }

    private void Update()
    {
        if (isDeployed == false) return;
        CheckOutOfBound();
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == hitMaskIndex) 
        {
            Character ch = collision.gameObject.GetComponent<Character>();
            if (ch) 
            {
                ch.GetHit(DMG);
                Instantiate(hitPS, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    void CheckOutOfBound() 
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPos.x < 0 || screenPos.x > Screen.width)
        {
            Destroy(gameObject);
        }
    }
}

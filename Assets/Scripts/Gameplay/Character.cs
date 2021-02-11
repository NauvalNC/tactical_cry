using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float HP = 100f;
    public float maxHP = 100f;
    public float speed = 1f;
    public float speedUnit = 5f;
    public AudioSource hitSource;
    public AudioClip[] hitSounds;

    private void Start()
    {
        CallInStart();
    }

    private void Update()
    {
        CallInUpdate();    
    }

    public virtual void CallInStart() 
    {

    }

    public virtual void CallInUpdate() 
    {
        if (HP <= 0) InvokeDeath();
    }

    /// <summary>
    /// Direction -1 to left, 1 to right
    /// </summary>
    /// <param name="direction"></param>
    public virtual void Movement(int direction)
    {
        float value = (int)direction;
        if (value == -1)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (value == 1)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        transform.Translate(new Vector3(value, 0, 0) * speed * Time.deltaTime);
    }

    /// <summary>
    /// Implement attack
    /// </summary>
    public virtual void Attack()
    {

    }

    public virtual void GetHit(float DMG) 
    {
        hitSource.clip = hitSounds[0];
        hitSource.Play();
        HP -= DMG;
        if (HP <= 0) HP = 0;
    }

    public virtual void InvokeDeath() 
    {
        hitSource.clip = hitSounds[1];
        hitSource.Play();
        Destroy(gameObject);
    }

    public void Recover(float amount)
    {
        HP += amount;
        if (HP > maxHP) HP = maxHP;
    }
}

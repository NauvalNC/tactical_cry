using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public Animator enemyAC;
    public int direction = 1;
    public float stopDistance = 2f;
    Transform target;

    public override void CallInStart()
    {
        base.CallInStart();
        target = GameObject.FindWithTag("Player").transform;
    }

    public override void CallInUpdate()
    {
        base.CallInUpdate();
        
        if (Player.Instance.HP <= 0)
        {
            Movement(0);
            enemyAC.SetBool("isMelee", false);
            enemyAC.SetBool("isWalking", false);
            return;
        }

        AIMovement();
    }

    void AIMovement() 
    {
        float distance = target.position.x - transform.position.x;
        if (Mathf.Abs(distance) <= stopDistance)
        {
            direction = 0;
            Attack();
        }
        else
        {
            direction = distance > 0 ? 1 : -1;
            enemyAC.SetBool("isMelee", false);
        }

        Movement(direction);

        enemyAC.SetBool("isWalking", Mathf.Abs(direction) > 0 ? true : false);
        if (enemyAC.GetBool("isWalking")) enemyAC.speed = speed / speedUnit;
        else enemyAC.speed = 1f;
    }

    public override void InvokeDeath()
    {
        base.InvokeDeath();
        GameManager.Instance.killCount += 1;
    }

    public override void Attack()
    {
        base.Attack();
        enemyAC.SetBool("isMelee", true);
    }
}

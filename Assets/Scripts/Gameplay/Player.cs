using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private static Player instance;
    public static Player Instance 
    {
        get 
        { 
            if (instance == null) instance = FindObjectOfType<Player>();
            return instance;
        }
    }

    public Animator footPortionAC, torsoPortionAC;
    public float jumpValue = 10f;
    public LayerMask groundLayer;

    [Header("Shooting Manager")]
    public GameObject bullet;
    Vector3 shootPos;
    public AudioSource gunSource;
    public AudioClip[] gunSounds;

    [HideInInspector]
    public int gunStatus = 0;
    
    [HideInInspector]
    public int ammo = 0;
    
    public float bulletDMG = 20f;
    public GameObject[] startShootObj;

    Rigidbody2D rb;
    BoxCollider2D coll;

    [Header("KeyCode")]
    public KeyCode shoot;
    public KeyCode melee;
    public KeyCode switchWeapon;
    public KeyCode jump;

    public override void CallInStart()
    {
        base.CallInStart();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    public override void CallInUpdate()
    {
        base.CallInUpdate();
        Setup();
        ChangeWeapon();
        Jump();
        Movement(0);
        Attack();
        ManageGunSound();
    }

    void ManageGunSound() 
    {
        gunSource.loop = gunStatus == 0 ? false : true;
        gunSource.clip = gunSounds[gunStatus];

        if (gunStatus == 0)
        {
            gunSource.loop = false;
            return;
        }

        if (gunSource.isPlaying == false && torsoPortionAC.GetBool("isShooting")) gunSource.Play();
        if (gunSource.loop && torsoPortionAC.GetBool("isShooting") == false) gunSource.Stop();
    }

    void Setup() 
    {
        torsoPortionAC.SetFloat("gunStatus", (float)gunStatus);
        shootPos = startShootObj[gunStatus].transform.position;
        if (ammo <= 0)
        {
            ammo = 0;
            gunStatus = 0;
        }
    }

    public override void Movement(int direction) 
    {
        if (Input.GetKey(KeyCode.LeftArrow)) direction = -1;
        else if (Input.GetKey(KeyCode.RightArrow)) direction = 1;

        footPortionAC.SetBool("isWalking", Mathf.Abs(direction) > 0 ? true : false);
        if (footPortionAC.GetBool("isWalking")) footPortionAC.speed = speed / speedUnit;
        else footPortionAC.speed = 1f;

        base.Movement(direction);
    }

    public override void Attack() 
    {
        torsoPortionAC.SetBool("isMelee", Input.GetKeyDown(melee));
        torsoPortionAC.SetBool("isShooting", Input.GetKey(shoot));
    }

    void ChangeWeapon() 
    {
        if (Input.GetKeyDown(switchWeapon) && ammo > 0) 
        {
            gunStatus = (gunStatus == 0) ? 1 : 0;
            SFXManager.Instance.Play("switch");
        }
    }

    public void Shoot() 
    {
        if (gunStatus == 0) gunSource.Play();

        Bullet obj = Instantiate(bullet, shootPos, Quaternion.identity).GetComponent<Bullet>();
        obj.DMG = bulletDMG;
        obj.Shoot(new Vector3(transform.localScale.x > 0 ? 1 : -1, 0, 0));
        if (gunStatus == 1) ammo -= 1;
    }

    void Jump() 
    {
        if (Input.GetKeyDown(jump) && IsGrounded())
        {
            SFXManager.Instance.Play("jump");
            rb.velocity = Vector2.up * jumpValue;
        }
    }

    bool IsGrounded() 
    {
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 1f, groundLayer);
        return hit.collider != null;
    }

    public override void InvokeDeath()
    {
        this.enabled = false;

        PlayerPrefs.SetInt("kills", GameManager.Instance.killCount);
        PlayerPrefs.SetInt("wave", GameManager.Instance.waveCount);

        SceneLoader.sceneToLoad = "GameOver";
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerCurrentHP = 100;
    public int playerMaxHP = 100;
    public int playerDamage = 20;
    public float moveSpeed = 5f;
    private Animator playerAnimator = null;
    private bool playerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDead == false)
        {
            Move();
            Attack();
        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h == 0 && v == 0)
        {
            GetComponent<Animator>().SetBool("isRun", false);
        }
        else
        {
            this.GetComponent<Animator>().SetBool("isRun", true);
            h = h * moveSpeed * Time.deltaTime;
            v = v * moveSpeed * Time.deltaTime;
            Vector3 vector = new Vector3(h, 0, v);
            this.transform.position += vector;
            if (vector != Vector3.zero)
            {
                transform.forward = vector;
            }
        }
    }

    private void Attack()
    {
        bool but = Input.GetButtonDown("Fire1");

        if(but == true)
        {
            playerAnimator.SetBool("isRun", false);
            playerAnimator.SetBool("isAttack", true);
        }
    }

    public void EndAttackAni()
    {
        playerAnimator.SetBool("isAttack", false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "ZombieWeapon")
        {
            int damage = collision.transform.root.GetComponent<Enemy>().zombieDamage;
            playerCurrentHP -= damage;

            if(playerCurrentHP <= 0)
            {
                // 플레이어 죽음
                GetComponent<Animator>().SetBool("isDeath", true);
            }
        }
    }
}

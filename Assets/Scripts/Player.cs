using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int playerCurrentHP = 100;
    public int playerMaxHP = 100;
    public int playerDamage = 20;
    public float moveSpeed = 5f;
    private Animator playerAnimator = null;
    private State playerState = State.Idle;

    [SerializeField]
    private GameObject swordCollider = null;

    public float hitTime = 1f;

    public enum State
    {
        Idle, Move, Attack, Hit, Dead
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState != State.Dead || playerState != State.Hit)
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
            playerState = State.Idle;
        }
        else
        {
            playerState = State.Move;
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
            if (playerState != State.Dead || playerState != State.Hit)
            {
                playerState = State.Attack;
                playerAnimator.SetBool("isRun", false);
                playerAnimator.SetBool("isAttack", true);
                swordCollider.SetActive(true);
                swordCollider.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void EndAttackAni()
    {
        playerAnimator.SetBool("isAttack", false);
        swordCollider.SetActive(false);
        playerState = State.Idle;
    }

    public void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "ZombieWeapon")
        {
            int damage = collision.transform.root.GetComponent<Enemy>().zombieDamage;
            Hit(damage);
        }
    }

    public void Hit(int damage)
    {
        if (playerState != State.Hit)
        {

            playerCurrentHP -= damage;
            if (playerCurrentHP <= 0)
            {
                StartCoroutine(CoDeath());
            }
            else
            {
                StartCoroutine(CoHit());
            }

            UIManager.instance.HpUpdate(playerCurrentHP, playerMaxHP);
        }
    }

    private IEnumerator CoHit()
    {
        playerState = State.Hit;
        GetComponent<Animator>().SetBool("isAttack", false);
        GetComponent<Animator>().SetBool("isRun", false);
        GetComponent<Animator>().SetBool("isHit", true);
        swordCollider.SetActive(false);
        yield return new WaitForSeconds(hitTime);
        GetComponent<Animator>().SetBool("isHit", false);
        playerState = State.Idle;
    }

    private IEnumerator CoDeath()
    {
        playerCurrentHP = 0;
        playerState = State.Dead;
        GetComponent<Animator>().SetBool("isDeath", true);

        // 죽는 모션이 충분히 나올때까지 기다리고
        yield return new WaitForSeconds(3f);

        // 게임 오버 UI 출력
        UIManager.instance.GameOver();

        // 대기하고
        yield return new WaitForSeconds(10f);

        // 씬을 재시작
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

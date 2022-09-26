using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private State enemyState = State.Idle;

    [SerializeField]
    private float attackRange = 3f;

    public int zombieHP = 100;
    public int zombieDamage = 20;
    public float hitTime = 0.5f;

    private GameObject[] zombieHands = null;

    public enum State
    {
        Idle, Chase, Attack, Hit, Dead
    }

    // Start is called before the first frame update
    void Start()
    {
        zombieHands = GameObject.FindGameObjectsWithTag("ZombieWeapon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Hit(int damage)
    {
        if(enemyState != State.Hit)
        {
            zombieHP -= damage;
            Debug.Log("좀비HP " + zombieHP);
            if (zombieHP <= 0)
            {
                // 좀비 죽음
                enemyState = State.Dead;
                GetComponent<Animator>().SetBool("isDeath", true);
            }
            else
            {
                StartCoroutine(CoHit());
            }
        }
    }

    private IEnumerator CoHit()
    {
        enemyState = State.Hit;
        GetComponent<Animator>().SetBool("isAttack", false);
        GetComponent<Animator>().SetBool("isWalk", false);
        GetComponent<Animator>().SetBool("isHit", true);
        GetComponent<NavMeshAgent>().enabled = false;
        zombieHands[0].GetComponent<SphereCollider>().enabled = false;
        zombieHands[1].GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(hitTime);
        GetComponent<Animator>().SetBool("isHit", false);
        enemyState = State.Idle;
    }


    private void Chase(GameObject playerObj)
    {
        float _dis = Vector3.Distance(playerObj.transform.position, this.transform.position);

        // 공격 범위보다 좀비와 플레이어 사이 거리가 가까울 경우
        if(_dis < attackRange)
        {
            enemyState = State.Attack;
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<Animator>().SetBool("isAttack", true);
            GetComponent<Animator>().SetBool("isWalk", false);

            zombieHands[0].GetComponent<SphereCollider>().enabled = true;
            zombieHands[1].GetComponent<SphereCollider>().enabled = true;
        }
        else
        {
            enemyState = State.Chase;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().destination = playerObj.transform.position;
            GetComponent<Animator>().SetBool("isWalk", true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if(tag == "Player")
        {
            Chase(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player")
        {
            Chase(collision.gameObject);
        }
    }

    public void EndAttackAni()
    {
        GetComponent<Animator>().SetBool("isAttack", false);
        zombieHands[0].GetComponent<SphereCollider>().enabled = false;
        zombieHands[1].GetComponent<SphereCollider>().enabled = false;
        enemyState = State.Idle;
    }
}

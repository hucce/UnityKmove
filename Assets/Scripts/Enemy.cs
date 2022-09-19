using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private State enemyState = State.Idle;

    [SerializeField]
    private float attackRange = 3f;

    public enum State
    {
        Idle, Chase, Attack, Hit, Dead
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

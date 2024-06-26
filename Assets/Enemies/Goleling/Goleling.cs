using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;

public class Goleling : Enemy
{
    [SerializeField]
    Transform target;
    NavMeshAgent agent;
    public float LookRadius;
    
    
    public Collider AttackCollider;
    [SerializeField]
    private int Damage = 1;


    void Start()
    {
        AttackCollider.GetComponent<EnemyWeapon>().SetDamage(Damage);
        SetStateIdle();
        transform.GetComponent<Collider>().enabled = true;
        agent = transform.GetComponent<NavMeshAgent>();
        agent.enabled = true;
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        curHP = maxHP;
        canvas.gameObject.SetActive(true);
        EnemyHPBar.fillAmount = (float)curHP/(float)maxHP;
    }

    
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                if(curHP <= 0)
                {
                    Death();
                    break;
                }
                animator.SetBool("isWalk", false);
                animator.SetBool("isAttack", false);
                //Debug.Log("Waiting...");
                break;

            case State.Follow:
                if(curHP <= 0)
                {
                    Death();
                    break;
                }
                agent.speed = 5f;
                animator.SetBool("isAttack", false);
                animator.SetBool("isWalk", true);
                agent.SetDestination(target.position);
                LookTarget();
                //Debug.Log("Follow!");
                break;

            case State.Attack:
                if(curHP <= 0)
                {
                    Death();
                    break;
                }
                agent.speed = 0f;
                animator.SetBool("isWalk", false);
                animator.SetBool("isAttack", true);
                agent.SetDestination(transform.position);
                LookTarget();
                //Debug.Log("Attacking!");
                break;

            case State.Retreat:
                if(curHP <= 0)
                {
                    Death();
                    break;
                }
                agent.speed = 3.5f;
                agent.SetDestination(transform.GetComponent<Enemy>().spawnPosition);
                animator.SetBool("isWalk", true);
                //Debug.Log("Retreat...");
                if(agent.remainingDistance <= 0.1f)
                {
                    currentState = State.Idle;
                }
                break;

            case State.Dead:
                //Debug.Log("Dead!");
                break;
        }
    }

    void LookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }


    
    

    private void hitEnd()
    {
        if(currentState == State.Idle)
        {
            SetStateFolow();
        }
        else{}
        animator.SetBool("isHit", false);
        agent.speed = 3.5f;
        if(curHP <= 0)
        {
            Death();
        }
        
    }

    private void AttackStart()
    {
        AttackCollider.enabled = true;

    }
    private void AttackEnd()
    {
        AttackCollider.enabled = true;

    }

    private void Death()
    {
        
        animator.SetBool("isDead", true);
        animator.SetBool("isAttack", false);
        animator.SetBool("isWalk", false);
        animator.SetBool("isHit", false);

        canvas.gameObject.SetActive(false);
        audioManager.Play("GolelingDie");
        transform.GetComponent<Collider>().enabled = false;
        agent.enabled = false;
        
        SetStateDead();
        SpawnDrop();

        target.GetComponent<PlayerLogic>().GetXP(transform.GetComponent<Enemy>().GetRewardXP());
    }

    void SpawnDrop()
    {
        GameObject newDrop = (GameObject)Resources.Load("Pouch");
        
        newDrop.GetComponent<ItemContainer>().item.itemData = drops[0];
        newDrop.GetComponent<ItemContainer>().item.itemData.prefab = newDrop;

        Vector3 randomPosition = new Vector3(
        Random.Range(0.5f, 0.5f),
        Random.Range(0, 0),
        Random.Range(0.5f, 0.5f)
        );

        Instantiate(newDrop, transform.position + randomPosition, newDrop.transform.rotation);
    }
}

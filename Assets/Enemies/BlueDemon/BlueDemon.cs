using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;


public class BlueDemon : Enemy
{
    [SerializeField]
    Transform target;
    NavMeshAgent agent;
    public float LookRadius;
    
    
    public Collider PunchCollider;
    public Collider WeaponCollider;
    [SerializeField]
    private int PunchDamage = 1;
    [SerializeField]
    private int WeaponDamage = 1;

    void Start()
    {
        PunchCollider.GetComponent<EnemyWeapon>().SetDamage(PunchDamage);
        WeaponCollider.GetComponent<EnemyWeapon>().SetDamage(WeaponDamage);
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
                animator.SetBool("isWalk", false);
                animator.SetBool("isAttack", false);
                //Debug.Log("Waiting...");
                break;

            case State.Follow:
                agent.speed = 3.5f;
                animator.SetBool("isAttack", false);
                animator.SetBool("isWalk", true);
                agent.SetDestination(target.position);
                LookTarget();
                //Debug.Log("Follow!");
                break;

            case State.Attack:
                agent.speed = 0f;
                animator.SetBool("isWalk", false);
                animator.SetBool("isAttack", true);
                agent.SetDestination(transform.position);
                LookTarget();
                //Debug.Log("Attacking!");
                break;

            case State.Retreat:
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

    private void PunchStart()
    {
        PunchCollider.enabled = true;
    }
    private void PunchEnd()
    {
        PunchCollider.enabled = false;
        //agent.speed = 3.5f;
    }
    private void WeaponStart()
    {
        WeaponCollider.enabled = true;
    }
    private void WeaponEnd()
    {
        WeaponCollider.enabled = false;
        //agent.speed = 3.5f;
    }
    private void Death()
    {
        animator.SetBool("isDead", true);
        canvas.gameObject.SetActive(false);
        audioManager.Play("DemonDie");
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
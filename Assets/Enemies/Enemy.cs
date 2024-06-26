using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;
using UnityEditor;
using Unity.Mathematics;

public class Enemy : MonoBehaviour
{
    
    public GameObject meshEnemy;
    public GameObject GetHitCollider;
    public AudioManager audioManager;
    public EnemyCanvas canvas;
    
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    public Animator animator;
    public Image EnemyHPBar;
    public string enemyName;
    public int maxHP;
    public int curHP;
    public int rewardXP;
    [Range (0f, 1)]
    public float physDef;
    [Range (0f, 1)]
    public float magDef;
    public Item[] drops;
    
    [System.Serializable]
    public enum State { Idle, Follow, Attack, Retreat, Dead}

    public State currentState = State.Idle;

    public void SetStateIdle()
    {
            currentState = State.Idle;
        
    }
    public void SetStateFolow()
    {
        if(currentState != State.Dead)
        {
            currentState = State.Follow;
        }
        
    }
    public void SetStateAttack()
    {
        if(currentState != State.Dead)
        {
            currentState = State.Attack;
        }
        
    }
    public void SetStateRetreat()
    {
        if(currentState != State.Dead && currentState != State.Idle)
        {
            currentState = State.Retreat;
        }
        
    }
    public void SetStateDead()
    {
        currentState = State.Dead;
        GetHitCollider.SetActive(false);
    }
    
    private void Awake() 
    {
        GetHitCollider.SetActive(true);
        spawnPosition = this.transform.position;
        spawnRotation = this.transform.rotation;
    }

    public void UpdateUI(int damage)
    {
        EnemyHPBar.fillAmount = (float)curHP/(float)maxHP;
        canvas.SetDamageText(damage);
    }
    public void ReSpawn()
    {
        if(currentState != State.Idle)
        {
            Material[] oldMaterials;

            if(meshEnemy.GetComponent<SkinnedMeshRenderer>() != null)
            {
                oldMaterials = meshEnemy.GetComponent<SkinnedMeshRenderer>().materials;
                Material[] newMaterials = new Material[meshEnemy.GetComponent<SkinnedMeshRenderer>().materials.Length-2];
                for(int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = oldMaterials[i];
                }
                meshEnemy.GetComponent<SkinnedMeshRenderer>().materials = newMaterials;
            }

            if(meshEnemy.GetComponent<MeshRenderer>() != null)
            {
                oldMaterials = meshEnemy.GetComponent<MeshRenderer>().materials;
                Material[] newMaterials = new Material[meshEnemy.GetComponent<MeshRenderer>().materials.Length-2];
                for(int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = oldMaterials[i];
                }
                meshEnemy.GetComponent<MeshRenderer>().materials = newMaterials;
            }
            GameObject clone = (GameObject)Instantiate(gameObject);


            clone.transform.SetParent(this.transform.parent);
            clone.name = gameObject.name;
            clone.transform.position = spawnPosition;
            clone.transform.rotation = spawnRotation;
            clone.GetComponent<Enemy>().spawnPosition = this.spawnPosition;
            clone.GetComponent<Enemy>().spawnRotation = this.spawnRotation;

            GameObject.Destroy(gameObject);
        }
    }

    public int GetRewardXP()
    {
        return rewardXP;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class EnemyCanvas : MonoBehaviour
{
    private string EnemyName;
    public TMP_Text EnemyName_Text;
    public TMP_Text DamageText;
    private float timer = 0;
    private int damageBuffer = 0;
    public void SetDamageText(int damage)
    {
        damageBuffer += damage;
        DamageText.text = "-" + damageBuffer.ToString();
        timer = 5;
    }

 

    void Start()
    {
        EnemyName = transform.GetComponentInParent<Enemy>().enemyName;
        
        EnemyName_Text.text = EnemyName;
    }

    void Update()
    {
        Quaternion lookRotation = Camera.main.transform.rotation;
        transform.rotation = lookRotation;
        if(timer >= 5f || timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            DamageText.text = "";
            damageBuffer = 0;
        }
    }
}

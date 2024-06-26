using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    public PlayerWeapon sword;
    public PlayerWeapon heavySword;
    public GameObject skillSource;
    public GameLogic gameLogic;
    public AudioManager audioManager;
    [SerializeField]
    private int maxHP;
    [SerializeField]
    private int curHP;
    [SerializeField]
    private int needXP = 20;
    [SerializeField]
    private int curXP = 0;
    [SerializeField]
    private int flaskHeal = 20;
    public int flaskMaxCount = 4;
    public int flaskCount = 4;
    private int skillPoints = 0;
    private int strength = 10;
    private int intelligence = 10;

    public Image hpBar;
    public Image xpBar;
    public TMP_Text xpText;
    public TMP_Text skillPointsText;
    public TMP_Text MaxHPText;
    public TMP_Text FlaskHealText;
    public TMP_Text DamageText;
    public TMP_Text IntText;
    public TMP_Text flaskCountText;
    public Image flaskBorder;
    private float flaskDelay = 5f;
    private float flaskTimer = 5f;
    private bool flaskUsefull = true;
    private bool isRoll = false;
    
    public Transform ReSpawnPosition;

    void Start()
    {
        audioManager.Play("LightAmbient");
        sword.SetDamage(strength);
        heavySword.SetDamage(strength + (int)strength/2);
        curHP = maxHP;
        flaskCount = flaskMaxCount;
        UpdateUI();
    }
    public void SetSpawn(Transform pos)
    {
        ReSpawnPosition = pos;
    }
    public void UpdateUI()
    {
        hpBar.fillAmount = (float)curHP/maxHP;
        xpBar.fillAmount = (float)curXP/needXP;
        xpText.text = curXP + "/" + needXP;
        flaskCountText.text = "" + flaskCount; 
        MaxHPText.text = "Здоровье: " + maxHP.ToString();
        FlaskHealText.text = "Лечение: " + flaskHeal.ToString();
        DamageText.text = "Сила: " + strength.ToString();
        IntText.text = "Интеллект: " + intelligence.ToString();
        skillPointsText.text = "Очки навыков: " + skillPoints.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EnemyWeapon"))
        {
            CombatEngine.hittingThePlayer(this.transform.GetComponent<PlayerLogic>(), other.GetComponent<EnemyWeapon>());
        }
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            Flask();
        }
        TimerFlaskDelay();
    }
    private void TimerFlaskDelay()
    {
        if(flaskUsefull == false)
        {
            if(flaskTimer < flaskDelay)
            {
                flaskTimer += Time.deltaTime;
            }
            if(flaskTimer >= flaskDelay)
            {
                flaskUsefull = true;
            }
            flaskBorder.fillAmount = flaskTimer/flaskDelay;
        }
        
    }
 

    public void GetXP(int xp)
    {
        curXP = curXP + xp;
        if(curXP >= needXP)
        {
            while(curXP >= needXP)
            {
                skillPoints++;
                curXP = curXP - needXP;
                needXP = needXP + 10;
            }
            
        }
        UpdateUI();
    }

    private void Flask()
    {
        if(flaskCount > 0 && flaskUsefull)
        {
            flaskUsefull = false;
            audioManager.Play("flaskSound");
            curHP = curHP + flaskHeal;
            flaskCount--;
            if(curHP > maxHP) curHP = maxHP;
            flaskBorder.fillAmount = 0f;
            flaskTimer = 0f;
            UpdateUI();
        }
        
    }
    public void Rest()
    {
        curHP = maxHP;
        flaskCount = flaskMaxCount;
        UpdateUI();
    }

    public void AddMaxHP()
    {
        
        if(skillPoints>0)
        {
            skillPoints--;
            maxHP = maxHP + 5;
            UpdateUI();
        }
    }
    public void AddFlaskHeal()
    {
        
        if(skillPoints>0)
        {
            skillPoints--;
            flaskHeal = flaskHeal + 5;
            UpdateUI();
        }
    }
    public void AddDamage()
    {
        
        if(skillPoints>0)
        {
            skillPoints--;
            PlayerData.Strength = strength;
            sword.SetDamage(strength);
            heavySword.SetDamage(strength + (int)strength/2);
            strength++;
            UpdateUI();
        }
    }
    public void AddInt()
    {
        if(skillPoints>0)
        {
            skillPoints--;
            PlayerData.Int = intelligence;
            intelligence++;
            UpdateUI();
        }
    }
    public void Dying()
    {
        gameObject.GetComponent<Animator>().SetBool("isDying", true);
        gameObject.GetComponent<Animator>().SetBool("isRollover", false);
        gameObject.GetComponent<ThirdPersonMovement>().OnDisable();
    }
    public void ReSpawn()
    {
        gameLogic.RespawnAllMobs();
        gameObject.GetComponent<Animator>().SetBool("isDying", false);
        gameObject.GetComponent<CharacterController>().enabled = false;
        curHP = maxHP;
        transform.position = ReSpawnPosition.position;
        gameObject.GetComponent<CharacterController>().enabled = true;
        UpdateUI();
        gameObject.GetComponent<ThirdPersonMovement>().OnEnable();
    }


    public void SkillAttack()
    {
        GameObject newObject = (GameObject)Resources.Load("Skills/FireBall");
        newObject.GetComponent<PlayerWeapon>().SetDamage(intelligence);
        Instantiate(newObject, skillSource.transform.position, transform.rotation);

    }


    public int GetCurHP()
    {
        return curHP;
    }
    public void SetCurHP(int hp)
    {
        curHP = hp;
    }
    public int GetMaxHP()
    {
        return maxHP;
    }
    public bool GetIsRoll()
    {
        return isRoll;
    }
    public void SetIsRoll(bool value)
    {
        isRoll = value;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class CombatEngine
{
    
    static public void hittingThePlayer(PlayerLogic player, EnemyWeapon enemyWeapon)
    {
        if(!player.GetIsRoll())
        {
            player.SetCurHP(player.GetCurHP() - enemyWeapon.GetDamage());
            player.UpdateUI();
            player.audioManager.Play("playerHit");
            if(player.GetCurHP() <= 0)
            {
                player.Dying();
            } 
        }
        
    }

    static public void hittingTheEnemy(Enemy enemy, PlayerWeapon playerWeapon)
    {
        enemy.audioManager.Play("enemyHit");
        enemy.animator.SetBool("isHit", true);
        if(playerWeapon.isPhys)
        {
            enemy.curHP = enemy.curHP - (int)(playerWeapon.damage - playerWeapon.damage * enemy.physDef);
            enemy.UpdateUI((int)(playerWeapon.damage - playerWeapon.damage * enemy.physDef));
        }
        else
        {
            enemy.curHP = enemy.curHP - (int)(playerWeapon.damage - playerWeapon.damage * enemy.magDef);
            enemy.UpdateUI((int)(playerWeapon.damage - playerWeapon.damage * enemy.magDef));
        }
    }
        
}

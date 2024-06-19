using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float heath = 0;
    public float maxHeath = 0;
    public float damage = 0;
    public float exp = 0;
    public float lv = 0;

    public float maxExp = 0;
    public float lvDmg = 0;
    public float lvHeath = 0;
    public float lvExp = 0;
    private HpBar hpBar;
    public event System.Action<Stats> OnLevelUp;


    void Awake()
    {
        hpBar = transform.GetComponent<HpBar>();
        hpBar.UpdateHealth(heath, maxHeath);

    }

    public void TakeDamage(float damage)
    {
        heath -= damage;
        hpBar.UpdateHealth(heath, maxHeath);
        hpBar.ShowDamage(damage);
        if (heath <= 0)
        {
            if (gameObject.transform.CompareTag("enemy"))
            {
                if (gameObject.GetComponent<EnemyController>() == null)
                    Destroy(gameObject);
                else
                    gameObject.GetComponent<EnemyController>().Die();

            }
        }
    }

    public void SetExp(float exp)
    {
        this.exp += exp;
        hpBar.ShowGainExp(exp);
        if (this.exp >= maxExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        exp = 0;
        lv += 1;
        maxExp += lvExp * lv;
        heath += lvHeath * lv;
        maxHeath += lvHeath * lv;
        damage += lvDmg * lv;
        hpBar.UpdateHealth(heath, maxHeath);

        if (OnLevelUp != null) {
            OnLevelUp?.Invoke(this);
        }
    }
}

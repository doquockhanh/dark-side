using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public GameObject HpBarPrefap;
    private GameObject hpBar;
    public float hpBarWidth = 1f;
    public float hpBarHeight = 1f;
    public float hpBarPosY = 0f;
    private Slider hpSlider;
    private Text hpText;
    private bool playerSideRight = true;
    private bool initPlayerSide = true;

    // Phần dành cho hiển thị damage 
    public GameObject damageTextPrefab;
    public GameObject expTextPrefab;

    void Awake()
    {
        InitHealthComponents();
        damageTextPrefab = Resources.Load<GameObject>("damageText");
        expTextPrefab = Resources.Load<GameObject>("expText");
    }

    void Update()
    {
        AvoidFlipHealthBar();
    }

    void InitHealthComponents()
    {
        Vector2 HpbarPos = transform.position;
        HpbarPos.y = transform.position.y + hpBarPosY;
        hpBar = Instantiate(HpBarPrefap, HpbarPos, Quaternion.identity);

        hpBar.transform.SetParent(transform);
        hpBar.transform.localScale = new Vector3(hpBar.transform.localScale.x * hpBarWidth, hpBar.transform.localScale.y, 0);

        initPlayerSide = transform.localScale.x < 0;

        hpSlider = hpBar.GetComponentsInChildren<Slider>()[0];
        hpText = hpBar.GetComponentsInChildren<Text>()[0];
    }

    void AvoidFlipHealthBar()
    {
        playerSideRight = transform.localScale.x < 0;
        if (playerSideRight == initPlayerSide)
        {
            if (hpBar.transform.localScale.x < 0)
            {
                hpBar.transform.localScale = new Vector3(-hpBar.transform.localScale.x, hpBar.transform.localScale.y, 0);
            }

        }
        else
        {
            if (hpBar.transform.localScale.x > 0)
            {
                hpBar.transform.localScale = new Vector3(-hpBar.transform.localScale.x, hpBar.transform.localScale.y, 0);
            }
        }
    }

    // Hàm để cập nhật thanh máu
    public void UpdateHealth(float hp, float maxHp)
    {
        if (hpSlider != null)
            hpSlider.value = hp / maxHp;
        if (hpText != null)
            hpText.text = $"{hp}/{maxHp}";
    }

    public void ShowDamage(float damageAmount)
    {
        if (damageTextPrefab == null)
        {
            return;
        }
        Vector2 pos = hpBar.transform.position;
        pos.x = Random.Range(hpBar.transform.position.x - 1f, hpBar.transform.position.x + 1f);
        GameObject damageText = Instantiate(damageTextPrefab, pos, Quaternion.identity);
        Text text = damageText.GetComponent<Text>();
        text.text = $"-{damageAmount} hp";
        damageText.transform.SetParent(hpBar.transform);
        damageText.transform.localScale = new Vector3(1f, 1f, 0);
        StartCoroutine(FadeOutAndDestroy(damageText, 2.5f));
    }

    public void ShowGainExp(float exp)
    {
        if (expTextPrefab == null)
        {
            return;
        }
        Vector2 pos = hpBar.transform.position;
        pos.x = Random.Range(hpBar.transform.position.x - 1f, hpBar.transform.position.x + 1f);
        GameObject expText = Instantiate(expTextPrefab, pos, Quaternion.identity);
        Text text = expText.GetComponent<Text>();
        text.text = $"+{exp} ep";
        expText.transform.SetParent(hpBar.transform);
        expText.transform.localScale = new Vector3(1f, 1f, 0);
        StartCoroutine(FadeOutAndDestroy(expText, 2.5f));
    }


    IEnumerator FadeOutAndDestroy(GameObject obj, float duration)
    {
        Text damageText = obj.GetComponent<Text>();

        Vector3 startPos = obj.transform.position;

        float startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            obj.transform.position = startPos + Vector3.up * t;
            damageText.color = new Color(damageText.color.r, damageText.color.g, damageText.color.b, 1f - t);
            yield return null;
        }

        Destroy(obj);
    }

}

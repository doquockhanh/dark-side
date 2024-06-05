using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool detected = false;
    public int currentGun = 0;
    public int countOfGuns = 2;
    private Transform hpBarTransform;
    private GameObject gunPrefap;
    private GameObject boomerangPrefap;
    private GameObject gunInstance;
    private GameObject boomerangInstance;

    void Awake()
    {
        GameManager.Instance.LoadPlayerStats();
    }
    void Start()
    {
        hpBarTransform = transform.Find("HealthBar(Clone)");
        gunPrefap = Resources.Load<GameObject>("Gun");
        gunInstance = Instantiate(gunPrefap, hpBarTransform.position - new Vector3(1.8f, 0, 0), Quaternion.identity);
        gunInstance.transform.SetParent(hpBarTransform);
        gunInstance.transform.localScale = new Vector3(0.3f, 0.3f, 0);

        boomerangPrefap = Resources.Load<GameObject>("Boomerang");
        boomerangInstance = Instantiate(boomerangPrefap, hpBarTransform.position - new Vector3(1.8f, 0, 0), Quaternion.identity);
        boomerangInstance.transform.SetParent(hpBarTransform);
        boomerangInstance.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        SwapGun();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SwapGun();
        }
    }

    void SwapGun()
    {
        currentGun = currentGun + 1 > countOfGuns ? (currentGun + 1) % countOfGuns : currentGun + 1;

        switch (currentGun)
        {
            case 1:
                {
                    DisableScriptIfEnabled<UseGun>(gameObject);
                    EnableScriptIfDisabled<UseBoomerang>(gameObject);
                    boomerangInstance.SetActive(true);
                    gunInstance.SetActive(false);
                    break;
                }
            case 2:
                {
                    DisableScriptIfEnabled<UseBoomerang>(gameObject);
                    EnableScriptIfDisabled<UseGun>(gameObject);
                    boomerangInstance.SetActive(false);
                    gunInstance.SetActive(true);
                    break;
                }
        }
    }

    private void EnableScriptIfDisabled<T>(GameObject target) where T : MonoBehaviour
    {
        T scriptInstance = target.GetComponent<T>();
        if (scriptInstance != null && !scriptInstance.enabled)
        {
            scriptInstance.enabled = true;
        }
    }

    private void DisableScriptIfEnabled<T>(GameObject target) where T : MonoBehaviour
    {
        T scriptInstance = target.GetComponent<T>();

        if (scriptInstance != null && scriptInstance.enabled)
        {
            scriptInstance.enabled = false;
        }
    }
}

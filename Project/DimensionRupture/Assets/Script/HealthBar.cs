using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpEffectImage;

    [SerializeField] private PlayerStatus playerStatus;

    [SerializeField] public float hp;
    [SerializeField] public float maxHp;
    [SerializeField] private float hurtSpeed = 0.005f;

    private void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        hp = playerStatus.currentHP;
        maxHp = playerStatus.maxHP;
    }

    public void UpdateHp()
    {
        StartCoroutine(UpdateHpCo());
    }

    IEnumerator UpdateHpCo()
    {
        hpImage.fillAmount = hp / maxHp;
        while (hpEffectImage.fillAmount >= hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
            yield return new WaitForSeconds(0.005f);
            //Debug.Log("A");
        }
        if (hpEffectImage.fillAmount < hpImage.fillAmount)
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }

        
    }
}

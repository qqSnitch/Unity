using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth= 100;    
    public float chipSpeed=2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damage overlay")]
    public Image overlayDamage;
    public Image overlayHeal;
    public float duration;
    public float fadeSpeed;

    private float durationTimer;
    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
        overlayHeal.color = new Color(overlayHeal.color.r, overlayHeal.color.g, overlayHeal.color.b, 0);
        overlayDamage.color = new Color(overlayDamage.color.r, overlayDamage.color.g, overlayDamage.color.b,0);
    }

    // Update is called once per frame
    void Update()
    {
        health=Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
        if(overlayDamage.color.a> 0)
        {
            if(health<30)
                return;
            durationTimer += Time.deltaTime;
            if(durationTimer>duration)
            {
                //fade image
                float tempAlpha = overlayDamage.color.a;
                tempAlpha-=Time.deltaTime * fadeSpeed;
                overlayDamage.color = new Color(overlayDamage.color.r, overlayDamage.color.g, overlayDamage.color.b, tempAlpha);
            }

        }
        if (overlayHeal.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                //fade image
                float tempAlpha = overlayHeal.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlayHeal.color = new Color(overlayHeal.color.r, overlayHeal.color.g, overlayHeal.color.b, tempAlpha);
            }

        }
    }
    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health/maxHealth;
        if(fillB>hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete=lerpTimer/chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF,backHealthBar.fillAmount, percentComplete);
        }
    }
    public void TakeDamage(float damage)
    {
        health-=damage;
        lerpTimer = 0f;
        durationTimer = 0;
        overlayHeal.color = new Color(overlayHeal.color.r, overlayHeal.color.g, overlayHeal.color.b,0);
        overlayDamage.color = new Color(overlayDamage.color.r, overlayDamage.color.g, overlayDamage.color.b,(float)0.2);
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        overlayDamage.color = new Color(overlayDamage.color.r, overlayDamage.color.g, overlayDamage.color.b,0);
        overlayHeal.color = new Color(overlayHeal.color.r, overlayHeal.color.g, overlayHeal.color.b,(float)0.2);
    }
}

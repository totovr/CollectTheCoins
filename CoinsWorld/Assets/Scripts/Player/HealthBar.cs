using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform healthBar;
    private Text healthText;

    private float maxLife = 1;
    private float lifeReduce;

    private string originalTextLife = "100%";

    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("HealthBarAnimation").GetComponent<RectTransform>();
        healthText = GameObject.FindGameObjectWithTag("LifeText").GetComponent<Text>();
        lifeReduce = maxLife;
    }

    // This will be called if the user was hit
    void UpdateHealthBar(float _hitDamage)
    {
        if (lifeReduce > 0)
        {
            lifeReduce -= _hitDamage;
            healthBar.localScale = new Vector3(lifeReduce, 1, 1);
            healthText.text = (Math.Round(lifeReduce, 1) * 100).ToString() + "%";
        }
    }

    // call this from GameManager
    void ResetTheHealthBar()
    {
        lifeReduce = 1;
        healthBar.localScale = new Vector3(maxLife, 1, 1);
        healthText.text = originalTextLife;
    }

}

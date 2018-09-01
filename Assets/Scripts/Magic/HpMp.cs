using System;
using UnityEngine;
using UnityEngine.UI;

public class HpMp : MonoBehaviour
{
    public CharacterStats resourceInfo;

    private float currentHealth;     // The current health
    private float maxHealth;         // The maximum health
    private float healthRegen;       // The rate health replenishes at

    private float currentMana;       // The current mana
    private float maxMana;           // The maximum mana
    private float manaRegen;         // The rate mana replenishes at

    private bool hasHealthBar;       // Determines if we have a health and/or mana bar to update
    private bool hasManaBar;         // ^^^^
    public GameObject healthBar;     // The related health bar
    public GameObject manaBar;       // The related mana bar

    private Slider _healthBarSlider; // Private slider
    private Slider _manaBarSlider;   // ^^^^

    public float sliderSpeed = 5f;   // How fast the slider will slide

    private float _displayedMana;    // The values the health and mana bars will follow
    private float _displayedHealth;  // ^^^^


    void Start()
    {
        if (resourceInfo == null)
        {
            Debug.LogError("no resourceinfo assigned");
        }
        else
        {
            maxHealth = resourceInfo.health;
            currentHealth = maxHealth;
            healthRegen = resourceInfo.healthRegen;

            maxMana = resourceInfo.mana;
            currentMana = maxMana;
            manaRegen = resourceInfo.manaRegen;
        }

        if (healthBar != null)
        {
            hasHealthBar = true;
        }
        if (manaBar != null)
        {
            hasManaBar = true;
        }

        _displayedHealth = currentHealth;
        _displayedMana = currentMana;

        if (hasHealthBar)
        {
            _healthBarSlider = healthBar.GetComponent<Slider>();
            _healthBarSlider.value = _displayedHealth / maxHealth;
        }

        if (hasManaBar)
        {
            _manaBarSlider = manaBar.GetComponent<Slider>();
            _manaBarSlider.value = _displayedMana / maxMana;
        }
    }

    private void Update()
    {
        currentHealth += healthRegen * Time.deltaTime;
        currentMana += manaRegen * Time.deltaTime;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }

        if (Math.Round(_displayedMana, 1) == currentMana)
        {
            _displayedMana = currentMana;
        }
        if (Math.Round(_displayedHealth, 1) == currentHealth)
        {
            _displayedHealth = currentHealth;
        }

        if (_displayedHealth < currentHealth)
        {
            _displayedHealth += Time.deltaTime * sliderSpeed;
            UpdateResourceBar(Sliders.health);
        }
        else if (_displayedHealth > currentHealth && (_displayedHealth - currentHealth) > 0.1f)
        {
            _displayedHealth -= Time.deltaTime * sliderSpeed;
            UpdateResourceBar(Sliders.health);
        }


        if (_displayedMana < currentMana)
        {
            _displayedMana += Time.deltaTime * sliderSpeed;
            UpdateResourceBar(Sliders.mana);
        }
        else if (_displayedMana > currentMana && (_displayedMana - currentMana) > 0.1f)
        {
            _displayedMana -= Time.deltaTime * sliderSpeed;
            UpdateResourceBar(Sliders.mana);
        }
        
    }

    private enum Sliders
    {
        health, mana
    }

    private void UpdateResourceBar(Sliders resourceSlider)
    {
        switch (resourceSlider)
        {
            case Sliders.health:
                if (this.hasHealthBar)
                {
                    _healthBarSlider = healthBar.GetComponent<Slider>();
                    _healthBarSlider.value = _displayedHealth / maxHealth;
                }
                break;
            case Sliders.mana:
                if (this.hasHealthBar)
                {
                    _manaBarSlider = manaBar.GetComponent<Slider>();
                    _manaBarSlider.value = _displayedMana / maxMana;
                }
                break;
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        if (currentHealth - incomingDamage <= 0) // Checking if the object will "survive"
        {
            currentHealth = 0;
            Destroy(this.gameObject);
            return;
        }

        currentHealth -= incomingDamage;
    }

    public void SpendMana(int manaCost)
    {
        currentMana -= manaCost;
        if (currentMana < 0)
        {
            currentMana = 0;
            Debug.LogError("Currentmana reached negative, forgot to call hasEnoughMana?");
        }
    }

    public void RegainMana(int manaBonus)
    {
        currentMana += manaBonus;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    // Method for other scripts to check if we have enough mana to use an ability
    public bool HasEnoughMana(int manaCost)
    {
        return manaCost > currentMana ? false : true;
    }
}

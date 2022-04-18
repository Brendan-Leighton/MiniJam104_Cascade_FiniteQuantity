using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image _healthBar;
    [SerializeField] private Character _character;

    private float _maxHealth;
    private float _currHealth;

    private void Awake()
    {
        _maxHealth = _character.GetMaxHealth();
        _currHealth = _character.GetCurrentHealth();
    }

    // Update is called once per frame
    void Update()
    {
        _currHealth = _character.GetCurrentHealth();
        _healthBar.fillAmount = _currHealth / _maxHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private Image _totalhealthBar;
    [SerializeField] private Image _currenthealthBar;
    private void Start()
    {
        _totalhealthBar.fillAmount = _playerHealth._currentHealth / 10;
    }

    private void Update()
    {
        _currenthealthBar.fillAmount = _playerHealth._currentHealth / 10;
    }
}

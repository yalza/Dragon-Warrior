using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private float _startingHealth;

    [Header("Iframes")]
    [SerializeField] private float _invunerabilityDuration;
    [SerializeField] private int _numberOfFlashes;
    
    public float _currentHealth { get; private set; }

    private Animator _anim;

    private SpriteRenderer _sprite;
    

    private void Awake()
    {
        _currentHealth = _startingHealth;
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _startingHealth);

        if(_currentHealth > 0)
        {
            _anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else {
            
            //player dead
            _anim.SetTrigger("death");
            GetComponent<PlayerMovement>().enabled = false;

        }
    }

    public void AddHealth(float health)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + health, 0, _startingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < _numberOfFlashes; i++)
        {
            _sprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(_invunerabilityDuration / (_numberOfFlashes * 2));
            _sprite.color = Color.white;
            yield return new WaitForSeconds(_invunerabilityDuration / (_numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
}

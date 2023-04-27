using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Animator _anim;
    private BoxCollider2D _coli;
    private bool hit;
    private float _direction;
    private float _lifeTime;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _coli = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float moveSpeed = _speed * Time.deltaTime * _direction;
        transform.Translate(moveSpeed, 0, 0);

        _lifeTime += Time.deltaTime;
        if(_lifeTime > 3)
        {
            Deactive();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") return;
        hit = true;
        _coli.enabled = false;
        _anim.SetTrigger("explode");

        
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
        gameObject.SetActive(true);
        hit = false;
        _coli.enabled = true;
        _lifeTime = 0;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX; 
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }


    private void Deactive()
    {
        gameObject.SetActive(false);
    }

}

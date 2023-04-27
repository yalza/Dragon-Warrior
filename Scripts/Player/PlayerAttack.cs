using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _attackCoolDown;
    private float _coolDownTimer;
    private Animator _anim;
    private PlayerMovement _playerMovement;
    [SerializeField] private GameObject _firePoint;
    [SerializeField] private GameObject[] _fireBalls;

    // Start is called before the first frame update
    void Start()
    {
        _anim= GetComponent<Animator>();
        _playerMovement= GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && _coolDownTimer > _attackCoolDown && _playerMovement.canAttack())
        {
            Attack();
        }

        _coolDownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        _coolDownTimer = 0;
        _anim.SetTrigger("attack");

        _fireBalls[FindFireBall()].transform.position = _firePoint.transform.position;
        _fireBalls[FindFireBall()].GetComponent<Projectile>().SetDirection(transform.localScale.x);
    }

    private int FindFireBall()
    {
        for(int i = 0; i< _fireBalls.Length; i++)
        {
            if (!_fireBalls[i].activeSelf)
            {
                return i;
            }
        }
        return 0;
    }
}

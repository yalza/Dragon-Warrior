using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float _moveDis;
    [SerializeField] private float _moveSpeed;



    private Vector3 _nextPos;
    private Vector3 _leftPos;
    private Vector3 _rightPos;

    private void Start()
    {
        _leftPos = new Vector3(transform.position.x - _moveDis, transform.position.y, transform.position.z);
        _rightPos = new Vector3(transform.position.x + _moveDis, transform.position.y, transform.position.z);
        _nextPos = _leftPos;
    }


    private void Update()
    {
        if(Mathf.Abs(transform.position.x - _nextPos.x) < 0.1f)
        {
            if(_nextPos == _leftPos)
            {
                _nextPos = _rightPos;
            }
            else
            {
                _nextPos = _leftPos;
            }
        }

        transform.position = new Vector3(transform.position.x + _moveSpeed * Time.deltaTime * Mathf.Sign(_nextPos.x - transform.position.x), transform.position.y, transform.position.z);
    }
}

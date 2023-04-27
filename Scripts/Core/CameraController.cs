using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Room Camera;
    [SerializeField] private float _speed;
    private float _currentPosX;
    private Vector3 _velocity = Vector3.zero;
    

    //Follow Player
    [SerializeField] private Transform _player;
    [SerializeField] private float _aheadDistance;
    private float _lookAhead;


    private void Update()
    {
        // Room Camera
        //  transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_currentPosX, transform.position.y, transform.position.z), ref _velocity, _speed);

        //Follow player
        transform.position = new Vector3(_player.position.x + _lookAhead, transform.position.y, transform.position.z);
        _lookAhead = Mathf.Lerp(_lookAhead, (_aheadDistance * _player.localScale.x), Time.deltaTime * _speed);
    }


    public void MoveToNewRoom(Transform newRoom)
    {
        _currentPosX = newRoom.position.x;
    }


}

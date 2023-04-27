using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform _prevRoom;
    [SerializeField] Transform _nextRoom;
    [SerializeField] CameraController _cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                _cam.MoveToNewRoom(_nextRoom);
            }
            else
            {
                _cam.MoveToNewRoom(_prevRoom);
            }
        }
    }
}

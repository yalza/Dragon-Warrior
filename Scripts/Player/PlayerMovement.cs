using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;
    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _coli;
    private float _wallJumpCooldown;
    private float _hor;



    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _coli = GetComponent<BoxCollider2D>();  
    }

    private void Update()
    {
        _hor = Input.GetAxis("Horizontal");
        // Flip player when moving left-right
        if (_hor > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (_hor < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }


        // Wall jump logic
        if(_wallJumpCooldown > 0.2f)
        {
            

            _body.velocity = new Vector2(_hor * _speed, _body.velocity.y);

            if(onWall() && !isGrounded())
            {
                _body.gravityScale = 0;
                _body.velocity = Vector2.zero;
            }
            else
            {
                _body.gravityScale = 7;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

        }
        else
        {
            _wallJumpCooldown += Time.deltaTime;
        }

        _anim.SetBool("run", _hor != 0);
        _anim.SetBool("grounded", isGrounded());


      
    }

    private void Jump()
    {
        if(isGrounded())
        {
            _body.velocity = new Vector2(_body.velocity.x, _jumpForce);
            _anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            
            if(_hor == 0)
            {
                _body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                _body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }

            _wallJumpCooldown = 0;
        }

        
    }

    private bool isGrounded()
    {    
        RaycastHit2D hit = Physics2D.BoxCast(_coli.bounds.center, _coli.bounds.size, 0, Vector2.down, 0.1f,_groundLayer);
        return hit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_coli.bounds.center, _coli.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, _wallLayer);
        return hit.collider != null;
    }

    public bool canAttack()
    {
        return _hor == 0 && isGrounded() && !onWall();
    }

 
}

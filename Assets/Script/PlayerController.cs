using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Controls")]
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpPower;


    [Header("Thresholds")]
    [SerializeField] private GameObject leftThreshold;
    [SerializeField] private GameObject rightThreshold;

    [Header("Movement Controls")]

    [SerializeField] private KeyCode jumpKey;

    [Header("Ground Colliding")]
    [SerializeField] private bool _grounded;

    [SerializeField] private CameraController camController;
    [SerializeField] private GameObject offset;
    [SerializeField] private float maxOffsetDistance;
    private float offsetDistance;
    #region Components
    private Rigidbody2D _rb;
    private Animator _an;
    private SpriteRenderer _sr;
    #endregion






    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _an = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * movespeed;

        if (move != 0f)
        {
            Debug.Log("Receiving input");
        }

        if (Input.GetKey(jumpKey) && _grounded)
        {
            Debug.Log("Performed a jump");
            _rb.AddForce(new Vector3(0, jumpPower, 0));
            _grounded = false;
        }


        float yVel = _rb.velocity.y;
        _an.SetFloat("HorizontalMovement", _rb.velocity.x);
        _an.SetFloat("VerticalMovement", _rb.velocity.y);
        if(_rb.velocity.x > 0f)
        {
            transform.localScale = new Vector3(3, 3, 1);
            AddOffset(_rb.velocity.x);
        }
        else if(_rb.velocity.x < 0f)
        {
            transform.localScale = new Vector3(-3, 3, 1);
            AddOffset(_rb.velocity.x);

        }
        else
        {
            ReduceOffset();
        }
        _rb.velocity = new Vector3(move, yVel, 0f);

        offset.transform.localPosition = new Vector3(offsetDistance, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.layer == 6)
        {
            Debug.Log("Collided with Ground");
            _grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Stopped colliding Ground");
        if (collision.collider.gameObject.layer == 6)
        {
            _grounded = false;
        }
    }

    private void AddOffset( float offset)
    {
        if (offset != 0)
        {
            if(offsetDistance < maxOffsetDistance)
            {
                offsetDistance += 0.1f;
            }
        }
    }

    private void ReduceOffset()
    {
        
            if (offsetDistance > 0)
            {
                offsetDistance -= 0.01f;
            }
            if(offsetDistance < 0)
        {
            offsetDistance = 0;
        }
        
    }


}

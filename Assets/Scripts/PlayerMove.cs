using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private Animator _animator;
    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0,0,speed) *Time.deltaTime);
            _animator.SetBool("MoveForward", true);
            _animator.SetBool("MoveBackward", false);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, 0, -speed*0.5f) * Time.deltaTime);
            _animator.SetBool("MoveBackward", true);
            _animator.SetBool("MoveForward", false);
        }
        else
        {
            _animator.SetBool("MoveForward", false);
            _animator.SetBool("MoveBackward", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -speed*3, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, speed*3, 0));
        }
    }
}

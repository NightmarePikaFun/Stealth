using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool sit = false;
    private BoxCollider _collider;

    [SerializeField]
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.X))
        {
            sit = true;
            _collider.center = new Vector3(0, 0.5f, 0);
            _collider.size = new Vector3(1,1,1);
        }
        else
        {
            _collider.center = new Vector3(0, 1f, 0);
            _collider.size = new Vector3(1, 1.9f, 1);
            sit = false;
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
                _animator.SetBool("MoveForward", true);
                _animator.SetBool("MoveBackward", false);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, 0, -speed * 0.5f) * Time.deltaTime);
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
                transform.Rotate(new Vector3(0, -speed * 3, 0));
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(new Vector3(0, speed * 3, 0));
            }
        }
        Sit();
    }

    void Sit()
    {
        _animator.SetBool("Sit", sit);
    }

    public bool GetSit()
    {
        return sit;
    }
}

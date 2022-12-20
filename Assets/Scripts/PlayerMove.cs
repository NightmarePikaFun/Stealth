using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    private Rigidbody _rigidbody;
    private Animator _animator;
    private bool sit = false;
    private BoxCollider _collider;
    private GameObject observer;

    private int cast = 0;

    [SerializeField]
    private float speed;
    [SerializeField]
    private int maxCast;
    [SerializeField]
    private GameObject stepSoundZone;


    // Start is called before the first frame update
    void Start()
    {
        observer = GameObject.FindGameObjectWithTag("observer");
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.LeftControl))
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
            if (Input.GetKey(KeyCode.W) && observer.GetComponent<Observer>().CanPlayerMove())
            {
                transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
                _animator.SetBool("MoveForward", true);
                _animator.SetBool("MoveBackward", false);
                SpawnSound(10);
                
            }
            else if (Input.GetKey(KeyCode.S) && observer.GetComponent<Observer>().CanPlayerMove())
            {
                transform.Translate(new Vector3(0, 0, -speed * 0.5f) * Time.deltaTime);
                _animator.SetBool("MoveBackward", true);
                _animator.SetBool("MoveForward", false);
                SpawnSound(5);
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

    void SpawnSound(float radius)
    {
        cast++;
        if (cast >= maxCast)
        {
            cast = 0;
            GameObject stepSound = Instantiate(stepSoundZone);
            stepSound.GetComponent<GrowZone>().SetMaxRange(radius);
            stepSound.transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }
    }
}

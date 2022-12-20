using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{

    private bool canSit = false;
    private bool sit = false;
    private GameObject player;
    private Vector3 playerPos;

    private CapsuleCollider _capsule;
    [SerializeField]
    private GameObject barrelModel;

    private GameObject observer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.gray;
        _capsule = GetComponent<CapsuleCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        observer = GameObject.FindGameObjectWithTag("observer");
    }

    // Update is called once per frame
    void Update()
    {
        if(canSit && Input.GetKeyDown(KeyCode.F))
        {
            //Hide player etc;
            sit = !sit;
            if (sit)
            {
                
                _capsule.isTrigger = true;
                player.layer = 2;
                playerPos = player.transform.position;
                player.tag = "Null";
                player.transform.position = barrelModel.transform.position;
                GetComponent<Renderer>().material.color = Color.gray;
                observer.GetComponent<Observer>().PlayerMoveSet(false);
            }
            else
            {
                player.layer = 0;
                player.transform.position = playerPos;
                player.tag = "Player";
                _capsule.isTrigger = false;
                GetComponent<Renderer>().material.color = Color.red;
                observer.GetComponent<Observer>().PlayerMoveSet(true);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.gameObject.tag == "Player")
        {
            canSit = true;
            GetComponent<Renderer>().material.color = Color.yellow;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSit = false;
            GetComponent<Renderer>().material.color = Color.gray;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenScript : MonoBehaviour
{
    [SerializeField]
    private GameObject leftDoor;
    [SerializeField]
    private GameObject rightDoor;
    [SerializeField]
    private GameObject lightness;

    private bool open = false;
    private bool canActivate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canActivate && Input.GetKeyDown(KeyCode.F) && !open)
        {
            if (leftDoor != null)
                leftDoor.transform.Rotate(new Vector3(0, 90, 0));
            if (rightDoor != null)
                rightDoor.transform.Rotate(new Vector3(0, -90, 0));
            lightness.SetActive(false);
            open = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            canActivate = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            canActivate = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour
{
    [SerializeField]
    private int watchX, watchZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && CalcAlertZone(other.transform.position) && LeverActive())
        {

            Debug.Log("Alert!");
        }
    }

    private bool CalcAlertZone(Vector3 other)
    {
        bool canX = false, canZ = false;
        if(watchX>0)
        {
            if(transform.position.x - other.x > 0)
                canX = true;
        }
        else
        {
            if (transform.position.x - other.x < 0)
                canX = true;
        }
        if(watchZ>0)
        {
            if (transform.position.z - other.z > 0)
                canZ = true;
        }
        else
        {
            if (transform.position.z - other.z < 0)
                canZ = true;
        }
        
        if (canX && canZ)
            return true;
        else
            return false;
    }

    private bool LeverActive()
    {
        return true;
    }
}

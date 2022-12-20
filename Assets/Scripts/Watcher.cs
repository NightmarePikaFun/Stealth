using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour
{
    [SerializeField]
    private int watchX, watchZ;
    [SerializeField]
    private GameObject zonePref;

    private GameObject observer;
    private float growSpeed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        observer = GameObject.FindGameObjectWithTag("observer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGrowSpeed(float value)
    {
        growSpeed = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "Player" && CalcAlertZone(other.transform.position) && LeverActive())
        {
            Vector3 pos = new Vector3(transform.position.x, 0.05f, transform.position.z);
            GameObject zone = Instantiate(zonePref);
            zone.GetComponent<GrowZone>().SetGrowSpeed(growSpeed);
            zone.transform.position = pos;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && CalcAlertZone(other.transform.position) && LeverActive())
        {
            observer.GetComponent<Observer>().SeeYou(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            observer.GetComponent<Observer>().NotSeeYou(this.gameObject);
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

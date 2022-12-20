using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepZone : MonoBehaviour
{

    private float growSpeed;
    private float maxScale;
    private GameObject observer;
    private float outputScale;

    // Start is called before the first frame update
    void Start()
    {
        growSpeed = GetComponent<GrowZone>().GetGrowSpeed();
        maxScale = GetComponent<GrowZone>().GetMaxScale();
        outputScale = maxScale;
        observer = GameObject.FindGameObjectWithTag("observer");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.localScale.x < maxScale)
        {
            outputScale -= growSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Guardin")
        {
            observer.GetComponent<Observer>().AddHearPower(other.gameObject,outputScale, transform.position);
        }
    }
}

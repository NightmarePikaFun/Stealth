using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiasbleWatchers : MonoBehaviour
{
    [SerializeField]
    private GameObject[] watchers;
    [SerializeField]
    private GameObject leverLight;

    private bool canClick = false;
    private bool activity = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F) && canClick && !activity)
        {
            StartTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            canClick = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canClick = false;
        }
    }

    private void StartTimer()
    {
        Debug.Log("Timer start");
        leverLight.GetComponent<Renderer>().material.color = Color.gray;
        activity = !activity;
        for(int i = 0; i < watchers.Length;i++)
        {
            watchers[i].GetComponent<Watcher>().SetGrowSpeed(1.0f);
        }
        Invoke("BackGrowSpeed", 30);
    }

    private void BackGrowSpeed()
    {
        for (int i = 0; i < watchers.Length; i++)
        {
            watchers[i].GetComponent<Watcher>().SetGrowSpeed(10.0f);
        }
        activity = !activity;
        leverLight.GetComponent<Renderer>().material.color = Color.yellow;
    }
}

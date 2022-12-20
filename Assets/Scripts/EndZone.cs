using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    [SerializeField]
    private GameObject winAlert;

    private bool canUse = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canUse && Input.GetKey(KeyCode.F))
        {
            winAlert.SetActive(true);
            canUse = false;
            KillAllEnemys();
        }
    }

    void KillAllEnemys()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemy.Length;i++)
        {
            Destroy(enemy[i]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            canUse = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canUse = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField]
    private GameObject alert;
    [SerializeField]
    private float maxListenPower;
    [SerializeField]
    private float degradeListen;
    [SerializeField]
    private GameObject listenGhost, deadEyeGhost;

    private GameObject[] enemys;
    private bool[] canSee;
    private float[] listen;

    private bool canPlayerMove = true;

    public bool CanPlayerMove()
    {
        return canPlayerMove;
    }

    public void PlayerMoveSet(bool value)
    {
        canPlayerMove = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        canSee = new bool[enemys.Length];
        listen = new float[enemys.Length];
        for (int i = 0; i< canSee.Length;i++)
        {
            canSee[i] = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool activeAlert = false;
        for (int i = 0; i < enemys.Length; i++)
        {
            if(canSee[i])
            {
                activeAlert = true;
            }
            if (listen[i] > 0)
            {
                listen[i] -= degradeListen;
            }
            else
            {
                listen[i] = 0;
            }
        }
        alert.SetActive(activeAlert);
    }

    public void SeeYou(GameObject gameObject)
    {
        for(int i =0; i<enemys.Length;i++)
        {
            if(enemys[i]==gameObject)
            {
                canSee[i] = true;
                break;
            }
        }
    }

    public void NotSeeYou(GameObject gameObject)
    {
        for(int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] == gameObject)
            {
                canSee[i] = false;
                break;
            }
        }
    }

    public void AddHearPower(GameObject gameObject, float power, Vector3 castPosition)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] == gameObject)
            {
                listen[i] += power;
                Debug.Log("Listen :"+listen[i]);
                if(listen[i]>=maxListenPower)
                {
                    SpawnGhostListen(castPosition);
                }
                break;
            }
        }
    }

    public void SpawnGhostTarget()
    {
        Debug.Log(GameObject.FindGameObjectWithTag("Killer") == null);
        if (GameObject.FindGameObjectWithTag("Killer") == null)
        {
            GameObject tmp = Instantiate(deadEyeGhost);
            tmp.transform.position = new Vector3(0, 10, 0);
            Debug.Log("I kill you!");
        }
    }

    public void SpawnGhostListen(Vector3 pos)
    {
        if (GameObject.FindGameObjectWithTag("Listener") == null)
        {
            GameObject tmp = Instantiate(listenGhost);
            tmp.transform.position = new Vector3(0, 10, 0);
            listenGhost.GetComponent<GhsotListener>().SetPos(pos);
            Debug.Log("I hear you!");
        }
    }
}

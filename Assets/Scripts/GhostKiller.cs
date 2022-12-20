using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKiller : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private GameObject player;
    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        catch
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerNotHide())
        {
            Vector3 napravlen = (player.transform.position - this.transform.position).normalized;
            transform.Translate(napravlen * speed * Time.deltaTime);
            playerPos = player.transform.position;
        }
        else
        {
            Vector3 napravlen = (playerPos - this.transform.position).normalized;
            transform.Translate(napravlen * speed * Time.deltaTime);
            if (Vector3.Distance(playerPos, this.transform.position) > 0.6)
                Destroy(this.gameObject);
        }
    }

    private bool PlayerNotHide()
    {
        GameObject hidePlayer = GameObject.FindGameObjectWithTag("Player");
        if (hidePlayer == null)
            return false;
        else
            return true;
    }
}


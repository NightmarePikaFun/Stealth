using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhsotListener : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Vector3 playerPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 napravlen = (playerPos - this.transform.position).normalized;
        transform.Translate(napravlen * speed * Time.deltaTime);
        if (Vector3.Distance(playerPos, this.transform.position) < 0.6)
            FindPlayer();
    }

    public void SetPos(Vector3 inputPos)
    {
        playerPos = inputPos;
    }

    void FindPlayer()
    {
        GameObject player;
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Vector3 pos = transform.position; pos.y += 2;
            Vector3 target = player.transform.position; target.y += 1.7f;
            Ray ray = new Ray(pos, -pos + target);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.DrawRay(pos, hit.point - pos, Color.red);
                Vector3 a = hit.point; a.y = 0;
                Vector3 b = target; b.y = 0;
                float raycastDistanceToPlayer = Vector3.Distance(a, b);
                if (raycastDistanceToPlayer < 0.85)
                {
                    playerPos = player.transform.position;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
        catch
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] route;
    [SerializeField]
    private int[] routeAngle;

    private int routeNumber = 0;

    private bool moveNaprav = false; // false to end, true to start;

    void Start()
    {
        
    }

    void FixedUpdate()
    {

        if (CalcDistance(transform.position, route[routeNumber]) < 0.5f)
        {
            if (routeNumber + 1 != route.Length)
            {
                transform.Rotate(new Vector3(0, routeAngle[routeNumber], 0));
                SetPose();
                routeNumber++;
            }
            else
            {
                transform.Rotate(new Vector3(0, routeAngle[routeNumber], 0));
                ReverseRoute();
            }
        }
        else
        {
            transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 pos = transform.position; pos.y += 2;
            Vector3 target = other.transform.position; target.y += 1.7f;
            Ray ray = new Ray(pos, -pos+target);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                Debug.DrawRay(pos, hit.point-pos, Color.red);
                Vector3 a = hit.point;a.y = 0;
                Vector3 b = target; b.y = 0;
                if (Vector3.Distance(a, b) < 0.85)
                    Debug.Log("I see you");
            }
        }
    }

    private void SetPose()
    {
        Vector3 tmpPos = route[routeNumber];tmpPos.y = transform.position.y;
        transform.position = tmpPos;
    }

    private float CalcDistance(Vector3 a, Vector3 b)
    {
        a.y = 0; b.y = 0;
        return Vector3.Distance(a, b);
    }

    private void ReverseRoute()
    {
        Vector3[] newRoute = new Vector3[route.Length];
        int[] newAngle = new int[2];
        for(int i = 0; i < route.Length;i++)
        {
            newRoute[i] = route[route.Length - 1 - i];
        }
        newAngle[0] = routeAngle[0];
        newAngle[1] = routeAngle[route.Length - 1];
        //routeAngle[route.Length - 1] = newAngle[0];
        //routeAngle[0] = newAngle[1];
        route = newRoute;
        routeNumber = 0;
    }
}

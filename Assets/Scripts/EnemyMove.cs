using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Vector3[] route;
    [SerializeField]
    private int[] routeAngle;
    [SerializeField]
    private float speed;

    private int routeNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    private float CalcDistance(Vector3 a, Vector3 b)
    {
        a.y = 0; b.y = 0;
        return Vector3.Distance(a, b);
    }

    private void SetPose()
    {
        Vector3 tmpPos = route[routeNumber]; tmpPos.y = transform.position.y;
        transform.position = tmpPos;
    }

    private void ReverseRoute()
    {
        Vector3[] newRoute = new Vector3[route.Length];
        int[] newAngle = new int[2];
        for (int i = 0; i < route.Length; i++)
        {
            newRoute[i] = route[route.Length - 1 - i];
        }
        newAngle[0] = routeAngle[0];
        newAngle[1] = routeAngle[route.Length - 1];
        route = newRoute;
        routeNumber = 0;
    }
}

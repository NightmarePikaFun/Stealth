using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    // Start is called before the first frame updateì 
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3[] route;
    [SerializeField]
    private int[] routeAngle;
    [SerializeField]
    private GameObject growZone;

    private GameObject observer;
    private int routeNumber = 0;
    private bool moveNaprav = false; // false to end, true to start;
    private bool seeZoneBool;

    [SerializeField]
    private int maxCast;

    private int cast = 0;

    void Start()
    {
        observer = GameObject.FindGameObjectWithTag("observer");
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

    private float raycastDistanceToPlayer = 100000000;

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
                raycastDistanceToPlayer = Vector3.Distance(a, b);
                if (raycastDistanceToPlayer < 0.85)
                {
                    cast++;
                    observer.GetComponent<Observer>().SeeYou(this.gameObject);
                    if(cast>=maxCast)
                    {
                        cast = 0;
                        Vector3 tmpVec = new Vector3(transform.position.x,0.5f,transform.position.z);
                        GameObject tmpZone = Instantiate(growZone);
                        tmpZone.transform.position = tmpVec;
                        tmpZone.GetComponent<VisionZone>().SetParent(this.gameObject);
                    }
                }
                else
                    observer.GetComponent<Observer>().NotSeeYou(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            raycastDistanceToPlayer = 1000000;
            observer.GetComponent<Observer>().NotSeeYou(this.gameObject);
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
        route = newRoute;
        routeNumber = 0;
    }

    public void SetSeeZone()
    {
        if(raycastDistanceToPlayer<0.85)
            observer.GetComponent<Observer>().SpawnGhostTarget();
    }
}

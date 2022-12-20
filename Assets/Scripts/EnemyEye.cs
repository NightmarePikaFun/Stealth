using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEye : MonoBehaviour
{
    // Start is called before the first frame updateì 
    [SerializeField]
    private GameObject growZone;

    private GameObject observer;

    [SerializeField]
    private int maxCast;

    private int cast = 0;

    void Start()
    {
        observer = GameObject.FindGameObjectWithTag("observer");
    }

    void FixedUpdate()
    {
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
                        tmpZone.GetComponent<VisionZone>().SetParent(transform.parent.gameObject, gameObject);
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


    public void SetSeeZone()
    {
        if(raycastDistanceToPlayer<0.85)
            observer.GetComponent<Observer>().SpawnGhostTarget();
    }
}

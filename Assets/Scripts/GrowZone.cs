using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowZone : MonoBehaviour
{

    [SerializeField]
    private float growSpeed;
    [SerializeField]
    private float maxScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.transform.localScale.x<maxScale)
        {
            Vector3 grow = transform.localScale;
            grow.x += growSpeed * Time.deltaTime;
            grow.z += growSpeed * Time.deltaTime;
            transform.localScale = grow;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetGrowSpeed(float _speed)
    {
        growSpeed = _speed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionZone : MonoBehaviour
{
    private GameObject _parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Присвоение родителей
    /// </summary>
    /// <param name="gameObject">Родительский объект</param>
    /// <param name="eye">Объект слежения (Глаза)</param>
    public void SetParent(GameObject gameObject, GameObject eye)
    {
        _parent = eye;
        transform.SetParent(gameObject.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _parent.GetComponent<EnemyEye>().SetSeeZone();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _parent.GetComponent<EnemyEye>().SetSeeZone();
        }
    }

}

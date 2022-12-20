using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    public int Zone;

    public GameObject[] neigborns;

    public GameObject[] GetNeigborns()
    {
        return neigborns;
    }
}

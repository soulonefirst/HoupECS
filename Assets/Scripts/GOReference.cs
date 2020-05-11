using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;

public class GOReference : MonoBehaviour 
{
    private void Start()
    {
        if(DollsBuffer.dolls == null)
        {
            DollsBuffer.dolls = new List<GameObject>();
        }
        if (!DollsBuffer.dolls.Contains(gameObject))
        {
            DollsBuffer.dolls.Add(gameObject);
        }

    }

}

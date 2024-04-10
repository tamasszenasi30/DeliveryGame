using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerGO;

    private Vector3 offset;

    //public Transform playerA;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerGO.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerGO.transform.position + offset;
        
    }
}

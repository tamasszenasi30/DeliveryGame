using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{

    public GameObject miniVanGO;

    NavMeshAgent navmesh;

    Vector3 startPosition;

    Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();

        navmesh.speed = 55f;

        startPosition = GameObject.Find("Start").transform.position;

        endPosition = GameObject.Find("Finish").transform.position;

        SpawnVan();

        MoveAi();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Destroy(navmesh.gameObject);
        }
    }

    void MoveAi()
    {
        navmesh.SetDestination(endPosition);
    }

    void SpawnVan()
    {
        Instantiate(miniVanGO, startPosition, Quaternion.identity);
    }


}

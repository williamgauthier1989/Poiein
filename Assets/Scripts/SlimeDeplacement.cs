using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class SlimeDeplacement : MonoBehaviour
{
    private NavMeshAgent _agent;
    public int Speed;
    private GameObject[] Arrival;
    private int _rand;
    //private Poyoyoyo _poyo;

    // Start is called before the first frame update
    void Start()
    {
            Arrival = GameObject.FindGameObjectsWithTag("Arrival");
            Debug.Log(Arrival.Length);

        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = Speed;
        _rand = Random.Range(0, Arrival.Length - 1);
        _agent.SetDestination(Arrival[_rand].transform.position);

    }

    // Update is called once per frame
    void Update()
    {

    }
}

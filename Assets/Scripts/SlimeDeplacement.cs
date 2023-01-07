using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class SlimeDeplacement : MonoBehaviour
{
    private NavMeshAgent _agent;
    public int Speed;
    public Transform[] Arrival;
    private int _rand;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = Speed;
        _rand = Random.Range(0, Arrival.Length - 1);
        // Debug.Log(_rand);
        _agent.SetDestination(Arrival[_rand].position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("other : " + other.gameObject.name);
        if (other.tag == "Arrival")
        {
            // Debug.Log("coucou");
            _rand = Random.Range(0, Arrival.Length - 1);
            // Debug.Log(_rand);
            _agent.SetDestination(Arrival[_rand].position);
        }
    }
}

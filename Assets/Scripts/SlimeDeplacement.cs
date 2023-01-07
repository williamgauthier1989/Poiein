using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;
using System.Linq;

public class SlimeDeplacement : MonoBehaviour
{
    private NavMeshAgent _agent;
    public int Speed;
    private GameObject[] Arrival;
    private int _rand;

    private GameObject _currentPath = null;

    private bool _idle;

    private void Awake()
    {
        Arrival = GameObject.FindGameObjectsWithTag("Arrival");
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = Speed;
    }
    void Start()
    {
        GetPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent.pathPending && _agent.enabled)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_idle)
                    StartCoroutine(SearchForPath());
            }
        }
    }

    IEnumerator SearchForPath()
    {
        _idle = true;
        yield return new WaitForSecondsRealtime(Random.Range(3, 16));
        GetPath();
    }

    private void GetPath()
    {
        if (_agent.enabled)
        {
            var availables = Arrival.Where(x => x.gameObject != _currentPath).ToArray();
            _rand = Random.Range(0, availables.Length);
            _agent.SetDestination(availables[_rand].transform.position);
            _currentPath = availables[_rand].gameObject;
            _idle = false;
        } else
        {
            StartCoroutine(SearchForPath());
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.AI;

public class SlimeScinde : MonoBehaviour
{
    [SerializeField] private int _niveau = 1;
    public int Niveau
    {
        get
        {
            return _niveau;
        }
        set
        {
            _niveau = value;
        }
    }
    private bool _hasExploded = false;
    public GameObject Child;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Scinder()
    {
        if (_niveau > 0 && !_hasExploded && Child != null)
        {
            for (var i = 0; i < 2; i++)
            {
                var child = Instantiate(Child);
                child.transform.position = transform.position;
                child.GetComponent<Poyoyoyo>().Element = GetComponent<Poyoyoyo>().Element;
                child.GetComponent<Poyoyoyo>().Spawn(UnityEngine.Random.onUnitSphere);
                child.GetComponent<NavMeshAgent>().agentTypeID = GetComponent<NavMeshAgent>().agentTypeID;

            }
            transform.localScale = Vector3.one * 0.5f;
            _hasExploded = true;
        }
    }


}

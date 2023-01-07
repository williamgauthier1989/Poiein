using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeScinde : MonoBehaviour
{
    [SerializeField] private int _niveau = 2;
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
                Instantiate(Child);
                Child.GetComponent<Poyoyoyo>().Element = GetComponent<Poyoyoyo>().Element;
            }
            transform.localScale = Vector3.one * 0.5f;
            _hasExploded = true;
        }
    }
}

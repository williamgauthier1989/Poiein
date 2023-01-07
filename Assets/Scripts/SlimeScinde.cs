using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScinde : MonoBehaviour
{
    [SerializeField] private int _niveau = 2;
    private bool _hasExploded = false;

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
        if (_niveau > 0 && !_hasExploded)
        {
            for (var i = 0; i < 2; i++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = transform.position;
                if (_niveau == 2)
                    cube.transform.localScale = Vector3.one * 0.5f;
                else if (_niveau == 1)
                    cube.transform.localScale = Vector3.one * 0.25f;
                cube.layer = 7;

                // REMPLACER PAR PREFAB
               var rb= cube.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
                var outline = cube.AddComponent<Outline>();
                var scinde = cube.AddComponent<SlimeScinde>();
                var poyoyo = cube.AddComponent<Poyoyoyo>();
                outline.OutlineColor = new Color32(197, 46, 46, 255);
                scinde._niveau = _niveau - 1;
                poyoyo.AspirationTime = 0.15f;
                cube.transform.parent = GameObject.Find("Plane").transform;
            }
            transform.localScale = Vector3.one * 0.25f;
            _hasExploded = true;
        }
    }
}

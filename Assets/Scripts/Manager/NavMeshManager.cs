using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    public NavMeshSurface[] AllNavMeshSurface;
    private GameObject[] Tiles;
    private int[] _layersTiles;
    private int _len;
    private bool _change;

    private void Awake()
    {
        Tiles = GameObject.FindGameObjectsWithTag("Ground");
    }
    // Start is called before the first frame update
    void Start()
    {
        _len = Tiles.Length;
        _layersTiles = new int[_len];
        for (int i = 0; i < _len; i++)
        {
            _layersTiles[i] = Tiles[i].layer;
        }

        foreach (NavMeshSurface surface in AllNavMeshSurface)
        {
            surface.BuildNavMesh();
        }
        StartCoroutine(CheckLayers());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator CheckLayers()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            _change = false;
            for (int i = 0; i < _len; i++)
            {
                if (Tiles[i].layer != _layersTiles[i])
                {
                    _layersTiles[i] = Tiles[i].layer;
                    _change = true;
                }
            }

            if (_change)
            {
                RefreshNavMesh();
            }
        }
    }

    private void RefreshNavMesh()
    {
        foreach (NavMeshSurface surface in AllNavMeshSurface)
        {
            surface.BuildNavMesh();
        }
    }
}

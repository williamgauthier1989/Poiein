using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{
    public NavMeshSurface[] AllNavMeshSurface;
    public GameObject[] Tiles;
    private int[] _layersTiles;
    private int _len;
    private bool _change;

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
            Debug.Log("NavMesh Update");
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckLayers());
    }

    private IEnumerator CheckLayers()
    {
        Debug.Log("Wait");
        yield return new WaitForSeconds(3f);
        Debug.Log("check begin");
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
        Debug.Log("Check end");
    }

    private void RefreshNavMesh()
    {
        foreach (NavMeshSurface surface in AllNavMeshSurface)
        {
            surface.BuildNavMesh();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundBehavior : MonoBehaviour
{
    public NavMeshSurface surface;
    private int _rand;
    private int _previousLayer;

    // Start is called before the first frame update
    void Start()
    {
        _previousLayer = gameObject.layer;
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.layer != _previousLayer)
        {
            surface.BuildNavMesh();
        }
        // StartCoroutine(ChangeLayer());
    }

    private IEnumerator ChangeLayer()
    {
        yield return new WaitForSeconds(5f);
        _rand = Random.Range(1, 3);
        Debug.Log("rand : " + _rand);
        if (_rand == 1)
        {
            if (gameObject.layer != 8)
            {
                gameObject.layer = 8;
                surface.BuildNavMesh();
                Debug.Log("Change to Ground");
            }
        }
        else
        {
            if (gameObject.layer != 6)
            {
                gameObject.layer = 6;
                surface.BuildNavMesh();
                Debug.Log("Change to Obstacle");
            }
        }
    }
}

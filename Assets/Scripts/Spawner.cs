using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0)
        {
            // Utiliser un pool ?? + Ajouter un max ???
            GameObject cube = Instantiate(_prefab);
            cube.transform.parent = GameObject.Find("Plane").transform;
            cube.GetComponent<Poyoyoyo>().Spawn(Random.onUnitSphere);
            var scinder = cube.GetComponent<SlimeScinde>();
            var rand = Random.value;
            if (rand <= 0.70)
                scinder.Niveau = 0;
            else if (rand <= 0.9)
                scinder.Niveau = 1;
            else
                scinder.Niveau = 2;

            if (scinder.Niveau == 1)
                cube.transform.localScale = Vector3.one * 0.5f;
            if (scinder.Niveau == 0)
                cube.transform.localScale = Vector3.one * 0.25f;
            _timer = Random.Range(5, 15);
        }

        _timer -= Time.deltaTime;
    }
}

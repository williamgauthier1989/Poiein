using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabBig;
    [SerializeField] private GameObject _prefabMedium;
    [SerializeField] private GameObject _prefabSmall;
    public string Element;
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
            GameObject cube = null;
            // Utiliser un pool ?? + Ajouter un max ???
            var rand = Random.value;
            if (rand <= 0.8)
            {
                cube = Instantiate(_prefabSmall);

            }
            else if (rand <= 0.95)
            {
                cube = Instantiate(_prefabMedium);
            }
            else
            {
                cube = Instantiate(_prefabBig);
            }
            cube.GetComponent<Poyoyoyo>().Spawn(Random.onUnitSphere);
            cube.GetComponent<Poyoyoyo>().Element = Element;
            cube.transform.position = transform.position;
            _timer = Random.Range(6, 20);
        }

        _timer -= Time.deltaTime;
    }
}

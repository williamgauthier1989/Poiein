using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScinde : MonoBehaviour
{
    public GameObject[] Children;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        foreach (GameObject child in Children)
        {
            child.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScinde : MonoBehaviour
{
    public GameObject[] Children;
    private int _niveau;

    // Start is called before the first frame update
    void Start()
    {
        _niveau = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (_niveau > 0)
        {
            foreach (GameObject child in Children)
            {
                child.SetActive(true);
                child.GetComponent<SlimeScinde>()._niveau -= 1;
            }
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnviro : MonoBehaviour
{
    public bool Catch;
    public string Element;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.layer);
        // Debug.Log(collision.gameObject.name);
        if (Catch && (collision.gameObject.layer == 13))
        {
            switch (Element)
            {
                case "Fire":
                    collision.gameObject.layer = 9;
                    break;
                case "Water":
                    collision.gameObject.layer = 10;
                    break;
                case "Ground":
                    collision.gameObject.layer = 8;
                    break;
                case "Veget":
                    collision.gameObject.layer = 11;
                    break;
                case "Rock":
                    collision.gameObject.layer = 12;
                    break;
                default:
                    break;
            }
        }
    }
}

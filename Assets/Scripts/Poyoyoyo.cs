using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.UI.GridLayoutGroup;

public class Poyoyoyo : MonoBehaviour
{
    private Outline _outline;
    private Rigidbody _rb;
    private SlimeScinde _scinde;
    private SlimeEnviro _enviro;

    public float AspirationTime = 0.15f;
    private float _currentTime = 0;
    private Vector3 _position;
    private bool _grabed = false;
    private Transform _owner;
    
    public bool Catch;
    public string Element;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _outline = GetComponent<Outline>();
        _scinde = GetComponent<SlimeScinde>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_grabed)
        {
            if (_currentTime == 0)
                _position = transform.position;
            if (_currentTime < AspirationTime)
            {
                transform.position = Vector3.Lerp(_position, _owner.position - Vector3.up, _currentTime / AspirationTime);
                _currentTime += Time.deltaTime;
            }
            else
            {
                transform.position = _owner.position;
            }
        } else
        {
            _currentTime = 0;
        }
    }

    public void OnArmIn()
    {
        _outline.OutlineWidth = 8;
    }
    public void OnArmOut()
    {
        _outline.OutlineWidth = 0;
    }
    public void OnGrabIn(Transform owner)
    {
        _scinde.Scinder();
        _rb.isKinematic = true;
        _grabed = true;
        _owner = owner;

    }
    public void OnGrabOut()
    {
        _rb.isKinematic = false;
        _grabed = false;
        _owner = null;
    }
    public void OnThrow()
    {
        _rb.isKinematic = false;
        _grabed = false;
        _owner = null;
        _rb.AddForce(transform.up * -10, ForceMode.Impulse);
        //gameObject.SetActive(false);
        Debug.Log("Throw");
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("coucocu");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Catch && (collision.gameObject.layer == 13))
        {
            Debug.Log("enter name : " + collision.gameObject.name + ", layer : " + collision.gameObject.layer);
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
            gameObject.SetActive(false);
        }
    }
}

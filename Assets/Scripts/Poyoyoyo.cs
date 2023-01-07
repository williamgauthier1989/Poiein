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

    public float AspirationTime = 1;
    private float _currentTime = 0;
    private Vector3 _position;
    private bool _grabed = false;
    private Transform _owner;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _outline = GetComponent<Outline>();
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
                transform.position = Vector3.Lerp(_position, _owner.position, _currentTime / AspirationTime);
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
}

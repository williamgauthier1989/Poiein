using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEngine.UI.GridLayoutGroup;

public class Poyoyoyo : MonoBehaviour
{
    private Outline _outline;
    private Rigidbody _rb;
    private SlimeScinde _scinde;
    private NavMeshAgent _agent;

    public float AspirationTime = 0.15f;
    private float _currentTime = 0;
    private Vector3 _position;
    private bool _grabed = false;
    private Transform _owner;

    public bool Catch;
    public string Element;

    private Vector3 _previousVelocity;
    private Vector3 _agentDestination;
    private bool _agentHadPath;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _outline = GetComponent<Outline>();
        _scinde = GetComponent<SlimeScinde>();
        _agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (Element)
        {
            case "Fire":
                GetComponent<MeshRenderer>().material.color = Color.red;
                break;
            case "Water":
                GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case "Ground":
                GetComponent<MeshRenderer>().material.color = Color.white;
                break;
            case "Veget":
                GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case "Rock":
                GetComponent<MeshRenderer>().material.color = Color.gray;
                break;
        }

        IEnumerator Jump()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(4, 16));
                if (_owner == null)
                {
                    _rb.velocity = _agent.velocity;
                    _agentHadPath = _agent.hasPath;
                    _agentDestination = _agent.destination;
                    _agent.enabled = false;
                    _rb.AddForce(Vector3.up * Random.Range(3, 8), ForceMode.Impulse);
                }
            }
        }
        StartCoroutine(Jump());
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
        }
        else
        {
            _currentTime = 0;
        }

    }

    private void FixedUpdate()
    {
        _previousVelocity = _rb.velocity;
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

        _rb.velocity = _agent.velocity;
        _agentHadPath = _agent.hasPath;
        _agentDestination = _agent.destination;
        _agent.enabled = false;

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
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!Catch && collision.transform.CompareTag("Ground") && _previousVelocity.y < 0)
        {
            _agent.enabled = true;
            if (_agentHadPath)
                _agent.destination = _agentDestination;
        }

        if (Catch && (collision.gameObject.layer == 13))
        {
            switch (Element)
            {
                case "Fire":
                    collision.gameObject.layer = 9;
                    collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                    break;
                case "Water":
                    collision.gameObject.layer = 10;
                    collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                    break;
                case "Ground":
                    collision.gameObject.layer = 8;
                    collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
                    break;
                case "Veget":
                    collision.gameObject.layer = 11;
                    collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                    break;
                case "Rock":
                    collision.gameObject.layer = 12;

                    collision.gameObject.GetComponent<MeshRenderer>().material.color = Color.grey;
                    break;
                default:
                    break;
            }
            collision.gameObject.GetComponent<Spawner>().enabled = true;
            collision.gameObject.GetComponent<Spawner>().Element = Element;
            gameObject.SetActive(false);
        }
    }
    public void Spawn(Vector3 direction)
    {
        GetComponent<Collider>().enabled = false;
        _agent.enabled = false;
        _rb.AddForce((Vector3.up * 6) + direction * Random.Range(2, 4), ForceMode.Impulse);
        IEnumerator RenableCollision()
        {
            yield return new WaitForSeconds(.1f);
            GetComponent<Collider>().enabled = true;
        }
        StartCoroutine(RenableCollision());

    }
}

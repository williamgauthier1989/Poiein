using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private float SmoothTime;
    [SerializeField] readonly int layerMask = (1 << 7 | 1 << 8 | 1 << 9 | 1 << 10 | 1 << 11 | 1 << 12 | 1 << 13);

    Poyoyoyo Highlighted = null;
    [SerializeField] bool has_one = false;
    bool must_throw = false;

    Vector3 MoveVelocity = Vector3.zero;

    private void Update()
    {
        if (Highlighted != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!has_one)
                {
                    Highlighted.Catch = true;
                    Highlighted.OnGrabIn(gameObject.transform);
                    has_one = true;
                }
                else
                {
                    Highlighted.Catch = false;
                    has_one = false;
                    Highlighted.OnGrabOut();
                    //Highlighted = null;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                must_throw = true;
            }
        }


    }

    private void FixedUpdate()
    {
        if (Highlighted != null)
            Highlighted.OnArmOut();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            transform.position = Vector3.SmoothDamp(transform.position, hit.point + Vector3.up * 2.5f, ref MoveVelocity, SmoothTime);
            if (hit.transform.gameObject.layer == 7 && hit.transform.TryGetComponent<Poyoyoyo>(out Poyoyoyo c) && !has_one)
            {
                Debug.Log("!!!");
                Highlighted = c;
                Highlighted.OnArmIn();
            } else if (!has_one)
            {
                Highlighted = null;
            }
        }

        if (Highlighted != null && must_throw && has_one)
        {
            has_one = false;
            Highlighted.OnThrow();
            must_throw = false;
        }
    }
}

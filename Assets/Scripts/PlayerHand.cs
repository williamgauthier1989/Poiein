using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private float SmoothTime;
    [SerializeField] readonly int layerMask = (1 << 7 | 1 << 8);

    Poyoyoyo Highlighted = null;
    bool has_one = false;
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
                    Highlighted.OnGrabIn(gameObject.transform);
                    has_one = true;
                }
                else
                {
                    has_one = false;
                    Highlighted.OnGrabOut();
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
                Highlighted = c;
                Highlighted.OnArmIn();
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

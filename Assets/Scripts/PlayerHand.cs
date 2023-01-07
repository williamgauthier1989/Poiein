using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private float SmoothTime;
    [SerializeField] readonly int layerMask = (1 << 7 | 1 << 30);

    Poyoyoyo Highlighted = null;
    bool has_one = false;

    Vector3 MoveVelocity = Vector3.zero;

    private void Update()
    {
        if (Highlighted != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!has_one)
                    Highlighted.OnGrabIn(gameObject.transform);
                has_one = true;
                //Highlighted.transform.position = transform.position;
            } else
            {
                has_one = false;
                Highlighted.OnGrabOut();
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
    }
}

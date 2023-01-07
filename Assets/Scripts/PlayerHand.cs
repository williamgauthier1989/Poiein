using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private float SmoothTime;
    [SerializeField] readonly int layerMask = (1 << 7 | 1 << 30);
    readonly List<Outline> Highlighted = new List<Outline>();

    Vector3 MoveVelocity = Vector3.zero;


    private void FixedUpdate()
    {
        Highlighted.ForEach(h => h.OutlineWidth = 0);
        Highlighted.Clear();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            transform.position = Vector3.SmoothDamp(transform.position, hit.point + Vector3.up * 10, ref MoveVelocity, SmoothTime);
            if (hit.transform.gameObject.layer == 7 && hit.transform.TryGetComponent<Outline>(out Outline c))
            {
                c.OutlineWidth = 8;
                Highlighted.Add(c);
            }
        }
    }
}

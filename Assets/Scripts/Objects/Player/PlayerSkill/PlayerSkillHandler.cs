using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerSkillHandler : MonoBehaviour
{
    private Vector2 _destPos;

    private void Awake()
    {

    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClicked();
        }
    }
    void OnMouseClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red);
        int _mask = LayerMask.GetMask("Monster");
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, 100.0f, _mask))
        {
            _destPos = _hit.point;
            Debug.Log(_destPos);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerSkillHandler : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;

    private float interval = 0.25f;
    private float doubleClickedTime = -1.0f;
    private bool monsterTarget = false;

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
        if (target != null)
        {
            Debug.Log(target.transform.position);
        }
        if (monsterTarget)
        {
            target = null;
            monsterTarget = false;
        }
    }
    private void OnMouseClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red);
        int _mask = LayerMask.GetMask("Enemy");
        RaycastHit _hit;
        if (Physics.Raycast(ray, out _hit, 100.0f, _mask))
        {
            target = _hit.transform.gameObject;
            Debug.Log(target);
        }
        else
        {
            OnMouseDoubleClicked();
        }
    }
    private void OnMouseDoubleClicked()
    {
        if ((Time.time - doubleClickedTime) < interval)
        {
            monsterTarget = true;
            doubleClickedTime = -1.0f;
        }
        else
        {
            monsterTarget = false;
            doubleClickedTime = Time.time;
        }
    }
    public void SkillSolt1()
    {
        GameObject instance = Instantiate(Resources.Load("",typeof(GameObject))) as GameObject;
    }
}

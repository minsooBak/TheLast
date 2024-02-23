using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private AudioSource _source;
    private ObjectPooling _objectPooling;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _objectPooling = transform.parent.GetComponent<ObjectPooling>();
    }

    private void Update()
    {
        if (!_source.isPlaying) gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        transform.position = Vector3.zero;
        _objectPooling.ReturnObject(gameObject);
    }

    private void OnDisable()
    {
        transform.position = Vector3.zero;
        _objectPooling.ReturnObject(gameObject);
    }
}

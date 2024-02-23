using System.Collections.Generic;
using UnityEngine;

public class UIManager : ScriptableObject
{
    private readonly List<UIBase> _uiBases = new();
    public Transform _uiCanvas;

    private readonly ResourceManager resource = GameManager.ResourceManager;

    public T ShowUI<T>() where T : UIBase
    {
        foreach(UIBase u in _uiBases) 
        {
            if (u is T)
            {
                u.Active();
                return u as T;
            }
        }

        if (_uiCanvas == null)
        {
            _uiCanvas = Instantiate(resource.Instantiate("UICanvas")).transform;
        }

        T ui = resource.Instantiate(typeof(T).Name) as T;
        _uiBases.Add(ui);

        return ui;
    }

    public void Clear()
    {
        _uiCanvas = null;
        _uiBases.Clear();
    }

    public void RemoveUI<T>()
    {
        for(int i = 0; i < _uiBases.Count; i++)
        {
            if (_uiBases[i] is T)
            {
                _uiBases.RemoveAt(i);
                return;
            }
        }
        Debug.LogError($"{typeof(T)} is not used");
    }
}

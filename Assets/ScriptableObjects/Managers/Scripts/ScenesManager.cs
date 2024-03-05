using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Enums;

public abstract class IBaseScene
{
    public abstract void Init();
    public virtual void Exit()
    {
        GameManager.ResourceManager.UnloadUnusedAssets();
    }
}

public class ScenesManager : ScriptableObject
{
    private readonly UIManager _uiManager = GameManager.UIManager;
    private readonly IBaseScene[] _scenes = new IBaseScene[Enum.GetValues(typeof(SceneState)).Length];
    private IBaseScene _currentScene;
    private IBaseScene _lateScene;
    private int _lodingSceneNumber = (int)SceneState.LodingScene;

    public SceneState CurrentState { get; private set; } = SceneState.IntroScene;
    
    private void Awake()
    {
        
        SceneManager.sceneLoaded += ChangScene;
        for(int i = 0; i < _scenes.Length; i++)
        {
            Type type = Type.GetType(((SceneState)i).ToString());
            Assert.IsNotNull(type);
            _scenes[i] = Activator.CreateInstance(type) as IBaseScene;
            Assert.IsNotNull(_scenes[i], $"{type.Name} is Not IBaseScene");
        }
        _currentScene = _scenes[0];
    }

    public void ChangeScene(SceneState state)
    {
        _lateScene = _currentScene;
        _currentScene = _scenes[(int)state];
        CurrentState = state;
        SceneManager.LoadScene(_lodingSceneNumber);
    }

    private void ChangScene(Scene scene, LoadSceneMode mode)
    {
        _uiManager.Init();
        if (scene.buildIndex != (int)CurrentState) return;
        else if(scene.buildIndex > (int)SceneState.CharaterSelectScene)
            GameManager.CharacterLoadData.SaveUserData();

        _lateScene?.Exit();
        _currentScene?.Init();
    }
}

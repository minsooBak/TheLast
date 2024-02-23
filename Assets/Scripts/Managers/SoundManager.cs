using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance {  get { return _instance; } }

    [SerializeField] private ObjectPooling _bgmPooling;
    [SerializeField] private ObjectPooling _sfxPooling;

    public void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClip clip, Vector3 position, float minDir, float maxDir)
    {
        {
            AudioSource source = _bgmPooling.GetObject(position).GetComponent<AudioSource>();
            source.clip = clip;
            source.minDistance = minDir;
            source.maxDistance = maxDir;
            source.Play();
        }
    }

    public void PlaySFX(AudioClip clip, Vector3 position, float minDir, float maxDir)
    {
        AudioSource source = _sfxPooling.GetObject(position).GetComponent<AudioSource>();
        source.minDistance = minDir;
        source.maxDistance = maxDir;
        source.PlayOneShot(clip);
    }
}

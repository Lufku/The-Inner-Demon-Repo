using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip level1Music;
    public AudioClip bossFightMusic;

    private AudioSource audioSource;

    private static MusicManager instance;

    void Awake()
    {
        // Evitar duplicados
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level1")
            PlayMusic(level1Music);

        if (scene.name == "SampleScene")
            PlayMusic(bossFightMusic);
    }

    void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

        if (audioSource.clip == clip)
            return; // Ya está sonando

        audioSource.clip = clip;
        audioSource.Play();
    }
}

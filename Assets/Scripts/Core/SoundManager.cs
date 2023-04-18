using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    private AudioSource musicSource;
    [SerializeField] private AudioClip[] audioClips= null;
    private void Awake()
    {
        source= GetComponent<AudioSource>();
        musicSource = GameObject.Find("MusicSource").GetComponent<AudioSource>();

        int random =Random.Range(0,audioClips.Length);
        musicSource.clip = audioClips[random];
        musicSource.Play();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else if(instance != null && instance !=this)
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
    public void ChangeSoundVolume(float value)
    {
        ChangeSourceVolume("soundVolume", source, value);
    }
    public void ChangeMusicVolume(float value)
    {
        ChangeSourceVolume("musicVolume", musicSource, value);
    }

    private void ChangeSourceVolume(string volumeName, AudioSource source,float value)
    {
        float finalVolume =value;
        source.volume = finalVolume;

        PlayerPrefs.SetFloat(volumeName, finalVolume);
    }

    public float GetSoundVolume()
    {
        return source.volume;
    }
    public float GetMusicVolume()
    {
        return musicSource.volume;
    }
}

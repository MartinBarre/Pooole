using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer audioMixer;
    public AudioMixerGroup soundMixerGroup;
    public AudioMixerGroup soundUIMixerGroup;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetVolumeMusic(GetVolumeMusic());
        SetVolumeEffects(GetVolumeEffects());
        SetVolumeUI(GetVolumeUI());
    }

    public float GetVolumeMusic()
    {
        return PlayerPrefs.GetFloat("VolumeMusic", 0.7f);
    }

    public float GetVolumeEffects()
    {
        return PlayerPrefs.GetFloat("VolumeEffects", 0.7f);
    }

    public float GetVolumeUI()
    {
        return PlayerPrefs.GetFloat("VolumeUI", 0.7f);
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("VolumeMusic", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("VolumeMusic", volume);
    }

    public void SetVolumeEffects(float volume)
    {
        audioMixer.SetFloat("VolumeEffects", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("VolumeEffects", volume);
    }

    public void SetVolumeUI(float volume)
    {
        audioMixer.SetFloat("VolumeUI", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("VolumeUI", volume);
    }

    public void PlaySound(AudioClip clip, float vol)
    {
        var tmpAudio = new GameObject("TmpAudio");
        var source = tmpAudio.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = soundMixerGroup;
        source.volume = vol;
        source.Play();
        Destroy(tmpAudio, clip.length);
    }

    public void PlaySoundUI(AudioClip clip)
    {
        var tmpAudio = new GameObject("TmpAudioUI");
        var source = tmpAudio.AddComponent<AudioSource>();
        source.clip = clip;
        source.outputAudioMixerGroup = soundUIMixerGroup;
        source.Play();
        Destroy(tmpAudio, clip.length);
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioChicken : MonoBehaviour
{
    public AudioClip jump;
    public List<AudioClip> flap;
    public List<AudioClip> hurt;
    public List<AudioClip> walk;

    private void Flap()
    {
        var audioClip = flap[Random.Range(0, flap.Count)];
        AudioManager.Instance.PlaySound(audioClip, 0.2f);
    }

    private void Jump()
    {
        AudioManager.Instance.PlaySound(jump, 0.2f);
    }

    private void Walk()
    {
        var audioClip = walk[Random.Range(0, walk.Count)];
        AudioManager.Instance.PlaySound(audioClip, 0.3f);
    }

    public void Hurt()
    {
        var audioClip = hurt[Random.Range(0, hurt.Count)];
        AudioManager.Instance.PlaySound(audioClip, 0.3f);
    }
}

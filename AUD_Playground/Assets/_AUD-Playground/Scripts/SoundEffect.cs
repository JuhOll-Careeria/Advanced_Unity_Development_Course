using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEffect
{
    [Range(0f, 1f), Tooltip("The volume level of the sound effects")]
    public float volume;
    [Range(0f, 1f), Tooltip("Set the Spatial Blend for the sound effect (0f = 2D, 1f = 3D")]
    public float spatialBlend;
    public AudioMixerGroup Mixer; 
    [Tooltip("Add the sound effect clips you want to use")]
    public AudioClip[] clips;

    public AudioClip GetClip()
    {
        if (clips.Length <= 0)
            return null;

        int rand = Random.Range(0, clips.Length);

        return clips[rand];
    }
}

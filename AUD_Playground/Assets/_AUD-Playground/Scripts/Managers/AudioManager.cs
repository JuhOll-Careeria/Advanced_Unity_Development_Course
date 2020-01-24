using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private SoundEffect Music;

    [Header("Collision Impact Sounds")]
    [SerializeField] private SoundEffect MetalImpact;
    [SerializeField] private SoundEffect WoodImpact;
    [SerializeField] private SoundEffect StoneImpact;

    private AudioSource AS;

    private void Awake()
    {
        AS = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayMusicTrack(Music);
    }

    /// <summary>
    /// Toistaa jonkin ääni effektin vain kerran annetulla SoundEffect datalla, ja datan äänenvoimakkuus lisätään audiosourceen
    /// </summary>
    /// <param name="effect"></param>
    public void PlayClipOnce(SoundEffect effect)
    {
        AS.PlayOneShot(effect.GetClip(), effect.volume);
    }

    /// <summary>
    /// Toistaa jonkin ääni effektin vain kerran annetulla SoundEffect datalla, annetun GameObjektin AudioSourcesta
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="source"></param>
    public void PlayClipOnce(SoundEffect effect, GameObject source)
    {
        // Hae source -GameObjectista "AudioSource"
        AudioSource SourceAS = source.GetComponent<AudioSource>();

        // Mikäli AudioSource komponenttia ei ole olemassa "source" objektissa, luo AudioSource komponentti sille
        if (SourceAS == null)
            SourceAS = source.AddComponent<AudioSource>();

        // Aseta GameObjektin AudioSourcelle spatialBlend samaan, mitä "effect":tiin on asetettu
        SourceAS.spatialBlend = effect.spatialBlend;

        // Toista ääni effekti source - GameObjektin AudioSource komponentista
        SourceAS.PlayOneShot(effect.GetClip(), effect.volume);
    }

    public void PlayMusicTrack(SoundEffect track)
    {
        AS.clip = track.GetClip();
        AS.volume = track.volume;
        AS.loop = true;
        AS.Play();
    }

    public void PlayPropCollision(CollisionType colType, GameObject root)
    {
        switch (colType)
        {
            case CollisionType.None:
                break;
            case CollisionType.Wood:
                PlayClipOnce(WoodImpact, root);
                break;
            case CollisionType.Metal:
                PlayClipOnce(MetalImpact, root);
                break;
            case CollisionType.Stone:
                PlayClipOnce(StoneImpact, root);
                break;
        }
    }
}

public enum CollisionType
{
    None,
    Wood,
    Metal,
    Stone
}

using System.Collections.Generic;
using System.Linq;
using SGOP.Util.Type;
using UnityEngine;

namespace SGOP.Manager
{
  public class AudioManager : MonoBehaviour
  {
    #region Singleton
    public static AudioManager Instance { get; private set; }
    #endregion


    #region Component Fields
    #endregion


    #region Private Fields
    private List<AudioSource> musicList_AS;
    private List<AudioSource> effectList_AS;
    #endregion


    #region Internal Fields
    #endregion


    #region Unity Methods
    void Awake()
    {
      if (Instance != null && Instance != this)
      {
        Destroy(this);
        return;
      }
      Instance = this;
      musicList_AS = new List<AudioSource>();
      effectList_AS = new List<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {

    }
    #endregion


    #region Private Methods
    #endregion


    #region Internal Methods

    public void Mute(bool mute = true)
    {
      var volume = mute ? 0 : 1;

      musicList_AS.ForEach(x => x.volume = volume);
      effectList_AS.ForEach(x => x.volume = volume);
    }

    public void SetVolume(float volume)
    {
      musicList_AS.ForEach(x => x.volume = volume);
      effectList_AS.ForEach(x => x.volume = volume);
    }

    internal void PlayAudioClip(AudioClip audioClip, AudioClipType audioClipType, bool loop = true, bool stopAll = true)
    {
      AudioSource audioSource;
      switch (audioClipType)
      {
        case AudioClipType.Music:
          if (stopAll) musicList_AS.ForEach(x => x.Stop());
          audioSource = musicList_AS.First(x => !x.isPlaying);
          break;
        default:
        case AudioClipType.Effect:
          if (stopAll) effectList_AS.ForEach(x => x.Stop());
          audioSource = effectList_AS.First(x => !x.isPlaying);
          break;
      }

      if (audioSource == null)
      {
        var newGO = Instantiate(new GameObject(), transform);
        var newAS = newGO.AddComponent<AudioSource>();

        switch (audioClipType)
        {
          case AudioClipType.Music:
            musicList_AS.Add(newAS);
            break;
          default:
          case AudioClipType.Effect:
            effectList_AS.Add(newAS);
            break;
        }
        audioSource = newAS;
      }

      audioSource.loop = loop;
      audioSource.clip = audioClip;
      audioSource.Play();
    }
    #endregion
  }
}

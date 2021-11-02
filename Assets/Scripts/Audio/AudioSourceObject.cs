using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tzaik.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public partial class AudioSourceObject: MonoBehaviour
    {
        #region Fields
        [Header("Audio Source Properties")]
        [SerializeField] AudioType type;
        [SerializeField] float maxPitch, minPitch;
        [SerializeField] List<AudioClip> audioClips;
        [SerializeField] bool playOnAwake = false; 
        AudioSource audioSource;
        #endregion

        #region Properties
        public AudioType Type { get => type; }
        public List<AudioClip> AudioClips { get => audioClips; set => audioClips = value; }
        public void setVolume(float vol) => audioSource.volume = vol;
        public void setClip(AudioClip clip) => audioSource.clip = clip;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            audioClips ??= new List<AudioClip>();
            audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            setVolume(AudioManager.Instance.Volumes[type]);

            if (!AudioManager.Instance.AudioSettings.Contains(this))
                AudioManager.Instance.AudioSettings.Add(this);

            if (playOnAwake) 
                    PlayClip();
        }
        #endregion

        #region Methods
        public void PlayClip()
        {
            if (audioClips.Count > 0)
            {
                audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
                audioSource.pitch = Random.Range(minPitch, maxPitch);
                audioSource.Play();
            }
            else
                Debug.LogWarning($"<color=yellow>Warning</color>, no clips available for <color=green>{this.name}</color>");
        } 
        #endregion

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tzaik.Audio
{
    [RequireComponent(typeof(AudioSourceObject))]
    public class FootstepAudio : MonoBehaviour
    {
        #region Fields
        AudioSourceObject sourceSettings;
        [SerializeField] List<AudioClip> footstepWoodClips;
        [SerializeField] List<AudioClip> footstepStoneClips; 
        string currentTag;
        #endregion

        #region Properties
        public string CurrentTag => currentTag;
        #endregion

        #region Unity Methods
        void Start() => sourceSettings = GetComponent<AudioSourceObject>();
        #endregion

        #region Methods
        void SetCurrentClips()
        {
            switch(currentTag)
            {
                case "Ground/Wood":
                    sourceSettings.AudioClips = footstepWoodClips;
                    break;
                case "Ground/Stone":
                    sourceSettings.AudioClips = footstepStoneClips;
                    break;
                default:
                    sourceSettings.AudioClips = footstepStoneClips;
                    break;
            }
        }

       public void ChangeTag(string tag)
       {
            currentTag = tag;
            SetCurrentClips();
       }

        #endregion
    }
}

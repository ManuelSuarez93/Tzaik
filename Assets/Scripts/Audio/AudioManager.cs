using System.Collections.Generic;
using UnityEngine;
using Tzaik.SaveSystem;
using System;
using System.Linq;
using Tzaik.General;

namespace Tzaik.Audio
{
    public class AudioManager : MonoBehaviour
    {
        #region Singleton
        private static AudioManager _instance;
        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<AudioManager>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("AudioManager");
                        _instance = container.AddComponent<AudioManager>();
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Fields
        [SerializeField] PrefabPool audioPool;
        List<AudioSourceObject> audioSettings;
        Dictionary<AudioType, float> volumes;
        AudioType currentAudioType;
        #endregion

        #region Properties
        public Dictionary<AudioType, float> Volumes { get => volumes; } 
        public List<AudioSourceObject> AudioSettings { get => audioSettings; set => audioSettings = value; }

        public readonly static string filename = "AudioData";
        #endregion

        #region Sound settings
        public void ChangeSound(float vol)
        {
            volumes[currentAudioType] = vol > 0 ? vol / 100 : 0;
            SetAudioVolume();
        }

        public void SelectAudio(AudioType type) => currentAudioType = type; 
        public double GetSound(AudioType type) => volumes[type];

        public void InitializeAudioSettings(List<AudioSlider> ao)
        {
            foreach (AudioSlider o in ao)
            { 
                o.Initialize(volumes[o.Type]); 
                o.Slider.onValueChanged.AddListener((float f) => SelectAudio(o.Type));
                o.Slider.onValueChanged.AddListener((float f) => ChangeSound(f));
                o.Slider.onValueChanged.AddListener((float f) => SaveSoundOptions());
            }
        }
        #endregion

        #region Save Audio settings
        public void SaveSoundOptions()
        {
            if(volumes != null)
            {
                var save = new SaveAudioObject(){  Volumes = volumes };
                SaveSystem.SaveManager.Save(save, filename);
            }
            else
                Debug.LogWarning("<color=yellow>Warning, volumes isn't initialized</color>");
            

        }
        #endregion

        #region Initializations
        public void InitializeManager()
        {
            audioSettings = new List<AudioSourceObject>();
            if (volumes == null)
                InitializeVolumes(); 
        } 
        void InitializeVolumes()
        {
            var so = new SaveAudioObject();
            so = SaveSystem.SaveManager.Load(so, filename);

            if (volumes == null)
            {
                if (so == null)
                {
                    volumes = new Dictionary<AudioType, float>();

                    foreach (AudioType t in Enum.GetValues(typeof(AudioType)))
                    {
                        volumes.Add(t, 0.5f);
                    }
                }
                else
                {
                    volumes = new Dictionary<AudioType, float>();

                    foreach (AudioType t in Enum.GetValues(typeof(AudioType)))
                    {
                        if (so.Volumes.ContainsKey(t))
                            volumes.Add(t, so.Volumes[t]);
                        else
                            Debug.LogError($"Missing {t} volume in loaded object");
                    }

                }
            }

        }
        #endregion

        #region Methods
        void GetAudioSources() => audioSettings = GameObject.FindObjectsOfType<AudioSourceObject>().ToList();
        void SetAudioVolume()
        {
            foreach(AudioSourceObject ao in audioSettings)
            {
                if (ao.Type == currentAudioType)
                    ao.setVolume(volumes[currentAudioType]);
            }
        } 
        public void InstantiateAudioOjbect(Vector3 pos, int id)
        {
            var audio = audioPool.InstanceSpecificObject(pos, id);
            audio.GetComponent<AudioSourceObject>().PlayClip();
        }


        #endregion

    }


}


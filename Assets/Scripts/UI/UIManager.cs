using Tzaik.General;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Tzaik.Audio;
using System.Collections.Generic;
using Tzaik.Player; 

namespace Tzaik.UI
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton
        private static UIManager _instance;
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<UIManager>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("UIManager");
                        _instance = container.AddComponent<UIManager>();
                    }
                }

                return _instance;
            }
        }
        #endregion 

        #region Fields 
        public Text InfoText;

        [Header("Menus")]
        [SerializeField] GameObject Menu;
        [SerializeField] GameObject PlayerUI;

        [Header("UI Elements")]
        [SerializeField] UIHealth health;
        [SerializeField] UISpecial special;
        [SerializeField] UIInventory inventory;
        [SerializeField] UIAmmo ammo;
        [SerializeField] UICoins coins;
        [SerializeField] UIKillCount kills;
        [SerializeField] UICurrentLevel currentLevel;
        [SerializeField] Image crosshair;

        [Header("Loading")]
        [SerializeField] GameObject loadingScreen;
        [SerializeField] Image loadingBar;

        [Header("Options")]
        [SerializeField] List<AudioSlider> audioOptions;
        [Header("Timer")]
        [SerializeField] float timer = 1f;
        float currentTime = 0f;
        #endregion

        #region Prpoerties
        public UIInventory UIInv => inventory; 
        public UIAmmo UIAmmo => ammo; 
        public GameObject LoadingScreen => loadingScreen;
        public Image LoadingBar => loadingBar; 
        public Image Crosshair => crosshair; 
        #endregion

        #region Unity Methods 

        private void Awake()
        {
            health.Health = GameManager.Instance.Player.GetComponent<PlayerController>().Health;
            special.Special = GameManager.Instance.Player.GetComponent<PlayerSpecial>();
            coins.PlayerCoins = GameManager.Instance.Player.GetComponent<PlayerCoins>(); 
            inventory.inventory = GameManager.Instance.Player.GetComponent<PlayerController>().Inventory; 
        }

        private void Start() 
            => SetUIAudioSettings();

        public void Update()
        {
            ammo.ShowAmmo();
            coins.ShowCoins();
            health.ShowHealth();
            kills.ShowKillCount(); 
            currentLevel.ShowCurrentLevel();
            Timer();
        }
        #endregion

        #region Methods
        public void ShowInfoText(string text)
        {
            if(InfoText != null)
            {
                currentTime = timer;
                InfoText.text = text;
            }
        }
        public void Timer()
        {
           if(currentTime > 0)
           {
                currentTime -= Time.deltaTime;
           }
           else
           {
                InfoText.text = "";
           }
        }
        public void SetUIAudioSettings() 
            => AudioManager.Instance.InitializeAudioSettings(audioOptions);  
        public void Pause(bool paused)
        {
            Menu.SetActive(paused);
            PlayerUI.SetActive(!paused);
        } 
        
        #endregion


    }
}

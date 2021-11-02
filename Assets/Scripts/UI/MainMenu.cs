using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Tzaik.Audio;
using System.Collections;
using UnityEngine.UI; 
using Tzaik.SaveSystem;
using static Tzaik.Items.Misc.CoinItem;
using System;
using Tzaik.Items.Weapons;

namespace Tzaik.UI
{
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] GameObject AudioOptions;
        [SerializeField] GameObject Menu;
        [SerializeField] GameObject Options;
        [SerializeField] List<AudioSlider> audioSliders;

        [Header("Loading")]
        [SerializeField] GameObject loadingScreen;
        [SerializeField] Image loadingBar;
        public void GoToLevel(string scene) => SceneManager.LoadScene(scene);
        public void ExitGame() => Application.Quit();

        private void Awake()
        {
            AudioManager.Instance.InitializeManager();
            AudioManager.Instance.InitializeAudioSettings(audioSliders);
            Menu.SetActive(true);
            Options.SetActive(false);
        }

        IEnumerator LoadSceneAsync(string scene)
        { 
            loadingScreen.SetActive(true);
            var loading = SceneManager.LoadSceneAsync(scene);
            while (!loading.isDone)
            {
                loadingBar.fillAmount = loading.progress; yield return null;
            }
            loadingScreen.SetActive(false);
            yield return null;
        } 
        public void Load()
        {
            var load = new SavePlayerObject();
            load = SaveSystem.SaveManager.Load<SavePlayerObject>(load, "Player");

            //foreach(WeaponType u in Enum.GetValues(typeof(WeaponType)))
            //    if(load.weapons[u])


            PlayerPrefs.SetString("PlayerName", load.name);
            PlayerPrefs.SetInt("CoinsAmountJade", load.coinsAmount[CoinType.JadeCoin]);
            PlayerPrefs.SetInt("CoinsAmountTablet", load.coinsAmount[CoinType.CrystalTablet]);
            PlayerPrefs.SetInt("CoinsAmountIdol", load.coinsAmount[CoinType.GoldenIdol]);
            PlayerPrefs.SetInt("EnemiesKilled", load.enemiesKilled);
            GoToLevel("StartingHub");
        } 
        public void New(string playerName)
        {
            var newGame = new SavePlayerObject()
            {
                coinsAmount = new Dictionary<CoinType, int>
                {
                    { CoinType.JadeCoin, 0 },
                    { CoinType.GoldenIdol, 0 },
                    { CoinType.CrystalTablet, 0 }
                },
                enemiesKilled = 0,
                name = playerName,
                weapons = new List<WeaponType>()
            };
             
            SaveManager.Save(newGame, $"Player"); 
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.SetInt("CoinsAmount", 0);
            PlayerPrefs.SetInt("EnemiesKilled", 0);

            GoToLevel("StartingHub");
        }

    }
}

using Tzaik.Player;
using UnityEngine;
using UnityEngine.UI;
using Tzaik.Audio;
using UnityEngine.Events;
using Tzaik.Level;
using UnityEngine.SceneManagement;
using System.Collections;
using Tzaik.UI;
using Tzaik.SaveSystem; 
using System.Collections.Generic;
using static Tzaik.Items.Misc.CoinItem; 
using Tzaik.Items.Weapons;
using System;
using Tzaik.Enemy;
using System.Linq;

namespace Tzaik.General
{
    public static class Levels
    {
        public readonly static string DUNGEON_LEVEL = "DungeonLevel";
        public readonly static string STARTING_HUB = "StartingHub";
        public readonly static string MAIN_MENU = "MainMenu";
    }
    
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<GameManager>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("GameManager");
                        _instance = container.AddComponent<GameManager>();
                    }
                }

                return _instance;
            }
        }
        #endregion

        #region Fields
        [SerializeField] Text debug;
        [SerializeField] Text stateText;
        [SerializeField] UnityEvent pauseEvent;
        [SerializeField] UnityEvent unPauseEvent;
        [SerializeField] UnityEvent levelCompleteEvent;
        [SerializeField] GameObject player;
        [SerializeField] LevelCreator levelCreator;
        [SerializeField] Area currentArea;
        [SerializeField] int levelsPerArea;
        [SerializeField] List<string> bosses;
        [SerializeField] string currentScene;

        int enemiesKilled;
        int totalEnemies;

        string playerName;
        static bool startAgain;
        static int totalEnemiesKilled;
        static Dictionary<CoinType, int> coinsAmount;
        int currentLevel;

        #endregion

        #region Properties 
        public GameObject Player  => player; 
        public int TotalEnemiesKilled  => totalEnemiesKilled; 
        public int CurrentLevel => currentLevel; 
        public Vector3 playerPosition => player.transform.position;

        #endregion

        #region Unity Methods
        private void Awake()
        {
            currentScene = SceneManager.GetActiveScene().name; 
            playerName = PlayerPrefs.GetString("PlayerName");
            player.GetComponent<PlayerCoins>().InitializeDictionary();
            coinsAmount = player.GetComponent<PlayerCoins>().CoinsAmount;
            AudioManager.Instance.InitializeManager(); 
            UIManager.Instance.LoadingScreen.SetActive(true);
            if (startAgain && currentScene != Levels.DUNGEON_LEVEL) 
                currentLevel = 0;
            
            Load(false);
            if(levelCreator != null)
                levelCreator.Initialize();
            else 
                UIManager.Instance.LoadingScreen.SetActive(false);

        }  
        #endregion

        #region Methods
        void Coins() => coinsAmount = Player.GetComponent<PlayerCoins>().CoinsAmount;
        public void DebugText(string debugText)
        {
            if(debug != null) 
                debug.text = debugText + "\n" + debug.text;
        }
        public void StateText(string state)
        {
            if(stateText != null) 
                stateText.text = state;
        }
        public void AddKill()
        { 
            enemiesKilled++;
            if (LevelComplete)
                levelCompleteEvent.Invoke();
        }
        public void GetAllEnemies() 
            => totalEnemies = FindObjectsOfType(typeof(EnemyAttack)).Length;

        public bool LevelComplete 
            => enemiesKilled >= totalEnemies / 2;
        public void Pause(bool isPaused)
            => Time.timeScale = isPaused ? 0 : 1; 
        public void GoToNextLevel()
        { 
            if(currentLevel < levelsPerArea)
            { 
                currentLevel++;
                RestartLevel();
            }
            else
            {
                currentLevel = 0;
                LoadScene(currentArea == Area.Temple ? bosses[0] :
                    currentArea == Area.Jungle ? bosses[1] :
                    currentArea == Area.CrystalCave ? bosses[2] : bosses[0]); 
            }
        }
        public void SaveAndGoToLevel(string level)
        {
            Save();
            LoadScene(level);
        }

        public void Save()
        {
            var playerWeapons = new List<WeaponType>();
            foreach (Weapon w in player.GetComponent<PlayerController>().Inventory.Weapons)
                if(w != null)
                { 
                    playerWeapons.Add(w.Type);
                    w.SaveWeaponObject();
                }

            var save = new SavePlayerObject()
            {
                coinsAmount = coinsAmount,
                enemiesKilled = totalEnemiesKilled,
                weapons = playerWeapons,
                name = playerName 
            };
            SaveManager.Save(save, $"Player");
        }  
        public void Load(bool goToLevel)
        { 
            if(goToLevel) LoadScene("StartingHub");

            var load = new SavePlayerObject();
            load = SaveManager.Load<SavePlayerObject>(load, "Player");

            load.weapons ??= new List<WeaponType>();

            Player.GetComponent<PlayerController>().Inventory.LoadInventory(load.weapons);
            if(load != null)
            { 
                PlayerPrefs.SetString("PlayerName", load.name);
                coinsAmount = load.coinsAmount;
                totalEnemiesKilled = load.enemiesKilled;
            } 
        } 
        public void LoadScene(string level) => StartCoroutine(LoadSceneAsync(level));
        public void RestartLevel() => StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().name));
        IEnumerator LoadSceneAsync(string scene)
        { 
            Coins(); 
            PlayerPrefs.SetInt("CoinsAmountJade", coinsAmount[CoinType.JadeCoin]);
            PlayerPrefs.SetInt("CoinsAmountTablet", coinsAmount[CoinType.CrystalTablet]);
            PlayerPrefs.SetInt("CoinsAmountIdol",  coinsAmount[CoinType.GoldenIdol]);
            PlayerPrefs.SetInt("EnemiesKilled", totalEnemiesKilled);
            startAgain = scene != SceneManager.GetActiveScene().name;
            SceneManager.LoadSceneAsync(scene);
            startAgain = scene == SceneManager.GetActiveScene().name;
            yield return null;
        }
        #endregion
         
    }
}

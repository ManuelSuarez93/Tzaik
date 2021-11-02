using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tzaik.SaveSystem;
using Tzaik.Level;

namespace Tzaik
{
    public class SaveGameObject
    {
        public GameObject level;
    }
    public class SaveLevel : MonoBehaviour
    {
        #region Singleton
        private static SaveLevel _instance;
        public static SaveLevel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<SaveLevel>();

                    if (_instance == null)
                    {
                        GameObject container = new GameObject("SaveLevel");
                        _instance = container.AddComponent<SaveLevel>();
                    }
                }

                return _instance;
            }
        }

        #endregion

        [SerializeField] LevelCreator creator;
        public static readonly string filename = "LevelData";

        public void save()
        {
            SaveSystem.SaveManager.Save(creator.StartPiece, filename);   
        }

        public void load()
        {
            var o = new GameObject();
            o = SaveSystem.SaveManager.Load(o, filename);
            Instantiate(o);
        }

    }
}

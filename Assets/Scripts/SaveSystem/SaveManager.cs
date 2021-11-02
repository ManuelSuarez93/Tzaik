using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Tzaik.LevelObjects;
using Tzaik.Items.Weapons;

namespace Tzaik.SaveSystem
{
    public static class SaveManager
    {
        public static string directory = "/SaveData/"; 

        public static void Save(object org, string filename)
        {
            string dir = Application.persistentDataPath + directory;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string json = JsonConvert.SerializeObject(org);
            File.WriteAllText(dir + filename, json);
        }

        public static T Load<T>(T so, string filename)
        {
            string fullpath = Application.persistentDataPath + directory + filename;
 

            if (File.Exists(fullpath))
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(fullpath));
            else
                Debug.LogWarning($"<color=yellow>Filepath {fullpath} does not exist, not able to load file</color>");

            return default(T);
            
        }
    }
}

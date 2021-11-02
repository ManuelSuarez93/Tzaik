using System.IO;
using Tzaik.Items.Weapons;
using Tzaik.Player;
using UnityEditor;
using UnityEngine;

namespace Tzaik.Level
{
    public static class ScriptableObjectUtility
    {
        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static void CreateAsset<T>() where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }

    public class LevelPieceObjectAsset
    {
        [MenuItem("Assets/Create/LevelPieceObject")]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<LevelPieceObject>();
        }
    }
    public class ListOfWeaponsAsset
    {
        [MenuItem("Assets/Create/ListOfWeapons")]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<ListOfWeapons>();
        }
    }

    public class WeaponObjectAsset
    {
        [MenuItem("Assets/Create/WeaponObject")]
        public static void CreateAsset()
        {
            ScriptableObjectUtility.CreateAsset<WeaponObject>();
        }
    }

}

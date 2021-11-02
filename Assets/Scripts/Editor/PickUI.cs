using System.Collections.Generic;
using Tzaik.Items.Pickups;
using Tzaik.Player;
using UnityEditor;
using UnityEngine;

namespace Tzaik.Level
{
    [CustomEditor(typeof(PickupItem))]
    public class PickUI : Editor
    {
        SerializedProperty interactable;
        SerializedProperty layerMask;
        private void OnEnable()
        {
            interactable = serializedObject.FindProperty("interactable");
            layerMask = serializedObject.FindProperty("layerMask");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(interactable);
            EditorGUILayout.PropertyField(layerMask);

            serializedObject.ApplyModifiedProperties();
            if (interactable.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox($"There is an empty item in{this.target.name}", MessageType.Error);
                Debug.LogError($"<color=red>EMPTY ITEM: </color>There is an empty item in {this.target.name}");
                EditorApplication.ExitPlaymode();
            }
        }
    }

    #region PlayerMovement
    //[CustomPropertyDrawer(typeof(PlayerMovement))]
    //public class PlayerMovementPropertyDrawer: PropertyDrawer
    //{
    //    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    //    {
    //        // The 6 comes from extra spacing between the fields (2px each)
    //        return EditorGUIUtility.singleLineHeight * 4 + 6;
    //    }
    //    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //    {   
    //        EditorGUI.BeginProperty(position, label, property);  
    //        var speedRect = new Rect(position.x, position.y + 18, position.width, 16);
    //        var sprintSpeedRect = new Rect(position.x, position.y + 36, position.width, 16);
    //        EditorGUI.PropertyField(speedRect, property.FindPropertyRelative("speed"));
    //        EditorGUI.PropertyField(sprintSpeedRect, property.FindPropertyRelative("sprintSpeed"));
    //        EditorGUI.EndProperty();
    //    }
    //} 
    #endregion

    [CustomEditor(typeof(PlayerController)), CanEditMultipleObjects] 
    public class PlayerContextUI : Editor
    { 

    }
     
}

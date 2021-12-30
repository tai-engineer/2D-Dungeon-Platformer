using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace DP2D
{
    [CustomEditor(typeof(Room))]
    public class RoomEditor: Editor
    {
        SerializedProperty _leftExitProp;
        SerializedProperty _rightExitProp;
        SerializedProperty _topExitProp;
        SerializedProperty _bottomExitProp;
        void OnEnable()
        {
            _leftExitProp = serializedObject.FindProperty("_leftExit");
            _rightExitProp = serializedObject.FindProperty("_rightExit");
            _topExitProp = serializedObject.FindProperty("_topExit");
            _bottomExitProp = serializedObject.FindProperty("_bottomExit");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_leftExitProp);
            EditorGUILayout.PropertyField(_rightExitProp);
            EditorGUILayout.PropertyField(_topExitProp);
            EditorGUILayout.PropertyField(_bottomExitProp);
        }
    }
}

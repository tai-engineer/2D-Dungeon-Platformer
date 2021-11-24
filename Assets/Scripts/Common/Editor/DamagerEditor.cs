using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace DP2D
{
    [CustomEditor(typeof(Damager))]
    public class DamagerEditor : Editor
    {
        Damager _damager;

        SerializedProperty _damageProp;

        SerializedProperty _typeProp;

        SerializedProperty _offsetProp;
        SerializedProperty _sizeProp;

        SerializedProperty _angleProp;
        SerializedProperty _radiusProp;

        SerializedProperty _canHitTriggerProp;
        SerializedProperty _disableAfterHitProp;
        SerializedProperty _hittableLayerProp;
        SerializedProperty _OnDamageableHitProp;
        SerializedProperty _OnNonDamageableHitProp;

        BoxBoundsHandle _boxBoundsHandle = new BoxBoundsHandle();
        ArcHandle _arcHandle = new ArcHandle();
        Color _enableColor = Color.green + Color.gray;

        void OnEnable()
        {
            _damager = (Damager)target;

            _damageProp = serializedObject.FindProperty("damage");

            _typeProp = serializedObject.FindProperty("type");

            _offsetProp = serializedObject.FindProperty("offset");
            _sizeProp = serializedObject.FindProperty("size");

            _angleProp = serializedObject.FindProperty("angle");
            _radiusProp = serializedObject.FindProperty("radius");

            _canHitTriggerProp = serializedObject.FindProperty("canHitTrigger");
            _disableAfterHitProp = serializedObject.FindProperty("disableDamageAfterHit");
            _hittableLayerProp = serializedObject.FindProperty("hittableLayer");
            _OnDamageableHitProp = serializedObject.FindProperty("onDamageableHit");
            _OnNonDamageableHitProp = serializedObject.FindProperty("onNonDamageableHit");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_damageProp);

            EditorGUILayout.PropertyField(_typeProp);

            if (_typeProp.enumValueIndex == (int)Damager.RaycastType.Rectangular)
            {
                EditorGUILayout.PropertyField(_offsetProp);
                EditorGUILayout.PropertyField(_sizeProp);
            }
            else if (_typeProp.enumValueIndex == (int)Damager.RaycastType.Circle)
            {
                EditorGUILayout.PropertyField(_angleProp);
                EditorGUILayout.PropertyField(_radiusProp);
            }
            EditorGUILayout.PropertyField(_canHitTriggerProp);
            EditorGUILayout.PropertyField(_disableAfterHitProp);
            EditorGUILayout.PropertyField(_hittableLayerProp);
            EditorGUILayout.PropertyField(_OnDamageableHitProp);
            EditorGUILayout.PropertyField(_OnNonDamageableHitProp);

            serializedObject.ApplyModifiedProperties();
        }

        void OnSceneGUI()
        {
            if (!_damager.enabled)
                return;

            Matrix4x4 handleMatrix;
            if (_typeProp.enumValueIndex == (int)Damager.RaycastType.Rectangular)
            {
                handleMatrix = _damager.transform.localToWorldMatrix;
                //handleMatrix.SetRow(0, Vector4.Scale(handleMatrix.GetRow(0), new Vector4(1f, 1f, 0f, 1f)));
                //handleMatrix.SetRow(1, Vector4.Scale(handleMatrix.GetRow(1), new Vector4(1f, 1f, 0f, 1f)));
                //handleMatrix.SetRow(2, new Vector4(0f, 0f, 1f, _damager.transform.position.z));
                using (new Handles.DrawingScope(handleMatrix))
                {
                    _boxBoundsHandle.center = _damager.offset;
                    _boxBoundsHandle.size = _damager.size;
                    _boxBoundsHandle.SetColor(_enableColor);

                    EditorGUI.BeginChangeCheck();
                    _boxBoundsHandle.DrawHandle();
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(_damager, "Modify Box Damager");

                        _damager.size = _boxBoundsHandle.size;
                        _damager.offset = _boxBoundsHandle.center;
                    }
                }
            }
            else if (_typeProp.enumValueIndex == (int)Damager.RaycastType.Circle)
            {
                Vector3 handleDirection = _damager.viewDirection;
                Vector3 handleNormal = Vector3.Cross(handleDirection, Vector3.up);
                handleMatrix = Matrix4x4.TRS(
                    _damager.transform.position,
                    Quaternion.LookRotation(handleDirection, handleNormal),
                    Vector3.one);
                using (new Handles.DrawingScope(handleMatrix))
                {
                    _arcHandle.angle = _damager.angle;
                    _arcHandle.radius = _damager.radius;

                    EditorGUI.BeginChangeCheck();
                    _arcHandle.DrawHandle();
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(_damager, "Modify Arc Damager");

                        if (_arcHandle.angle > 360f)
                            _arcHandle.angle = 360f;
                        else if (_arcHandle.angle < 0f)
                            _arcHandle.angle = 0f;
                        _damager.angle = _arcHandle.angle;
                        _damager.radius = _arcHandle.radius;
                    }
                }
            }
        }
    }
}

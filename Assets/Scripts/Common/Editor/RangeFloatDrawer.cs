using UnityEngine;
using UnityEditor;
namespace DP2D
{
    [CustomPropertyDrawer(typeof(RangeFloat), true)]
    public class RangeFloatDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            SerializedProperty minProp = property.FindPropertyRelative("minValue");
            SerializedProperty maxProp = property.FindPropertyRelative("maxValue");

            float minValue = minProp.floatValue;
            float maxValue = maxProp.floatValue;

            float rangeMin = 0;
            float rangeMax = 1;

            var ranges = (MinMaxRangeAttribute[])fieldInfo.GetCustomAttributes(typeof(MinMaxRangeAttribute), true);
            if(ranges.Length > 0)
            {
                rangeMin = ranges[0].Min;
                rangeMax = ranges[0].Max;
            }

            const float rangeBoundsLabelWidth = 40f;

            var label1 = new Rect(position);
            label1.width = rangeBoundsLabelWidth;
            GUI.Label(label1, new GUIContent(minValue.ToString("F2")));
            position.xMin += rangeBoundsLabelWidth;

            var label2 = new Rect(position);
            label2.xMin = label2.xMax - rangeBoundsLabelWidth;
            GUI.Label(label2, new GUIContent(maxValue.ToString("F2")));
            position.xMax -= rangeBoundsLabelWidth;

            EditorGUI.BeginChangeCheck();
            EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
            if(EditorGUI.EndChangeCheck())
            {
                minProp.floatValue = minValue;
                maxProp.floatValue = maxValue;
            }
            EditorGUI.EndProperty();
        }
    }
}

using UnityEditor;
using UnityEngine;

namespace ErgoOption.Editor
{
    [CustomPropertyDrawer(typeof(ValueOption<>))]
    public sealed class ValueOptionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var hasValueProp = property.FindPropertyRelative("hasValue");
            var valueProp = property.FindPropertyRelative("value");

            var valueRect = new Rect(position.x, position.y, position.width - 25, position.height);
            var hasValueRect = new Rect(position.x + position.width - 20, position.y, 20, position.height);

            if (hasValueProp.boolValue)
                EditorGUI.PropertyField(valueRect, valueProp, label, true);
            else
                EditorGUI.PrefixLabel(position, label);

            hasValueProp.boolValue = EditorGUI.Toggle(hasValueRect, hasValueProp.boolValue);
        }
    }
}
namespace ErgoOption
{
    using UnityEditor;
    using UnityEngine;

    [CustomPropertyDrawer(typeof(Option<>), true)]
    public sealed class OptionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("value");
            EditorGUI.PropertyField(position, valueProperty, label, true);
        }
    }
}
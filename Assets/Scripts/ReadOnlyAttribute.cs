using UnityEditor;
using UnityEngine;

// Создаем кастомный атрибут ReadOnlyAttribute
public class ReadOnlyAttribute : PropertyAttribute { }

// Создаем расширение для запрета редактирования полей, помеченных атрибутом ReadOnlyAttribute
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false; // Запрещаем редактирование поля
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif
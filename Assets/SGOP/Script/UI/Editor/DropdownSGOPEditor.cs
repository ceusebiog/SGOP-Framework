using UnityEditor;
using UnityEditor.UI;

namespace SGOP.UI
{
  [CustomEditor(typeof(DropdownSGOP), true)]
  [CanEditMultipleObjects]
  public class DropdownSGOPEditor : SelectableEditor
  {
    SerializedProperty m_Template;
    SerializedProperty m_templateImage;
    SerializedProperty m_title;
    SerializedProperty m_tooltip;
    SerializedProperty m_CaptionText;
    SerializedProperty m_arrowImage;
    SerializedProperty m_ItemText;
    SerializedProperty m_ItemImage;
    SerializedProperty m_itemCheckmark;
    SerializedProperty m_Value;
    SerializedProperty m_AlphaFadeSpeed;
    SerializedProperty m_Options;
    SerializedProperty m_OnValueChanged;

    protected override void OnEnable()
    {
      base.OnEnable();
      m_Template = serializedObject.FindProperty("m_Template");
      m_templateImage = serializedObject.FindProperty("m_templateImage");
      m_title = serializedObject.FindProperty("m_title");
      m_tooltip = serializedObject.FindProperty("m_tooltip");
      m_CaptionText = serializedObject.FindProperty("m_CaptionText");
      m_arrowImage = serializedObject.FindProperty("m_arrowImage");
      m_ItemText = serializedObject.FindProperty("m_ItemText");
      m_ItemImage = serializedObject.FindProperty("m_ItemImage");
      m_itemCheckmark = serializedObject.FindProperty("m_itemCheckmark");
      m_Value = serializedObject.FindProperty("m_Value");
      m_AlphaFadeSpeed = serializedObject.FindProperty("m_AlphaFadeSpeed");
      m_Options = serializedObject.FindProperty("m_Options");
      m_OnValueChanged = serializedObject.FindProperty("m_OnValueChanged");
    }

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      serializedObject.Update();
      EditorGUILayout.PropertyField(m_Template);
      EditorGUILayout.PropertyField(m_templateImage);

      EditorGUILayout.PropertyField(m_tooltip);

      EditorGUILayout.PropertyField(m_title);
      EditorGUILayout.PropertyField(m_CaptionText);
      EditorGUILayout.PropertyField(m_arrowImage);

      EditorGUILayout.PropertyField(m_ItemText);
      EditorGUILayout.PropertyField(m_ItemImage);
      EditorGUILayout.PropertyField(m_itemCheckmark);

      EditorGUILayout.PropertyField(m_Value);
      EditorGUILayout.PropertyField(m_AlphaFadeSpeed);
      EditorGUILayout.PropertyField(m_Options);
      EditorGUILayout.PropertyField(m_OnValueChanged);

      serializedObject.ApplyModifiedProperties();
    }
  }
}
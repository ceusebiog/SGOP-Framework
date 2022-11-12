using UnityEditor;
using UnityEditor.UI;

namespace SGOP.UI
{
  [CustomEditor(typeof(ButtonSGOP), true)]
  [CanEditMultipleObjects]
  public class ButtonSGOPEditor : SelectableEditor
  {
    SerializedProperty m_text;
    SerializedProperty m_OnClickProperty;

    protected override void OnEnable()
    {
      base.OnEnable();
      m_text = serializedObject.FindProperty("m_text");
      m_OnClickProperty = serializedObject.FindProperty("m_OnClick");
    }

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      serializedObject.Update();

      EditorGUILayout.PropertyField(m_text);
      EditorGUILayout.Space();
      EditorGUILayout.PropertyField(m_OnClickProperty);

      serializedObject.ApplyModifiedProperties();
    }
  }
}
using TMPro;
using UnityEngine;

namespace SGOP.UI
{
  [AddComponentMenu("UI/InputField (SGOP)", 31)]
  public class InputFieldSGOP : TMP_InputField
  {
    [Space]

    [SerializeField]
    private TMP_Text m_title;
    public TMP_Text title { get { return m_title; } set { m_title = value; } }

    [SerializeField]
    private TMP_Text m_tooltip;
    public TMP_Text tooltip { get { return m_tooltip; } set { m_tooltip = value; } }
  }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGOP.UI
{
  [AddComponentMenu("UI/Button (SGOP)", 30)]
  public class ButtonSGOP : Button
  {
    [SerializeField]
    private TMP_Text m_text;
    public TMP_Text text { get { return m_text; } set { m_text = value; } }
  }
}
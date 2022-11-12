using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGOP.UI
{
  [AddComponentMenu("UI/Dropdown (SGOP)", 31)]
  [RequireComponent(typeof(RectTransform))]
  public class DropdownSGOP : TMP_Dropdown
  {
    [Space]

    [SerializeField]
    private TMP_Text m_title;
    public TMP_Text title { get { return m_title; } set { m_title = value; } }

    [SerializeField]
    private TMP_Text m_tooltip;
    public TMP_Text tooltip { get { return m_tooltip; } set { m_tooltip = value; } }

    [SerializeField]
    private Image m_templateImage;
    public Image templateImage { get { return m_templateImage; } set { m_templateImage = value; } }

    [SerializeField]
    private Image m_arrowImage;
    public Image arrowImage { get { return m_arrowImage; } set { m_arrowImage = value; } }

    [SerializeField]
    private Image m_itemCheckmark;
    public Image itemCheckmark { get { return m_itemCheckmark; } set { m_itemCheckmark = value; } }
  }
}

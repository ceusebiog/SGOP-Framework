using UnityEngine;

namespace SGOP.Util
{
  public class TextColorize
  {
    #region Colors
    public static TextColorize Default = new TextColorize(Color.white);
    public static TextColorize Warning = new TextColorize(Color.yellow);
    public static TextColorize Error = new TextColorize(Color.magenta);
    #endregion


    #region Private Constants
    private readonly string prefix;
    private const string Suffix = "</color>";
    #endregion


    #region Private Methods
    private TextColorize(Color color) => prefix = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>";
    private TextColorize(string hexColor) => prefix = $"<color={hexColor}>";
    #endregion


    #region Public Operators
    public static string operator %(string text, TextColorize color) => color.prefix + text + Suffix;
    #endregion
  }
}

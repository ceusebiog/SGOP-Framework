using Newtonsoft.Json;
using UnityEngine;
using SGOP.Util;

namespace SGOP.Manager
{
  public class DebugManager : MonoBehaviour
  {
    #region Singleton
    public static DebugManager Instance { get; private set; }
    #endregion


    #region Component Fields
    [SerializeField]
    private GameObject InGameDebugConsolePrefab;
    #endregion


    #region Unity Methods
    void Awake()
    {
      if (Instance != null && Instance != this)
      {
        Destroy(this);
        return;
      }
      Instance = this;
    }
    #endregion


    #region Public Methods
    public void Log(string origin, string message = null, object obj = null)
    {
      if (!AppLaunchHandler.Instance.IsProdBuild)
        Debug.Log(MakeLogString(origin, 0, message, obj));
    }

    public void Warning(string origin, string message = null, object obj = null)
    {
      if (!AppLaunchHandler.Instance.IsProdBuild)
        Debug.LogWarning(MakeLogString(origin, 1, message, obj));
    }

    public void Exception(string origin, string message = null, object obj = null)
    {
      if (!AppLaunchHandler.Instance.IsProdBuild)
        Debug.LogError(MakeLogString(origin, 2, message, obj));
    }

    public void Error(string origin, string message = null, object obj = null)
    {
      if (!AppLaunchHandler.Instance.IsProdBuild)
        Debug.LogError(MakeLogString(origin, 2, message, obj));
    }
    #endregion


    #region Private Methods
    private string MakeLogString(string origin, int defType, string message = null, object obj = null)
    {
      var originDebug = $"{GetTextColorize("Origin: ", defType)}{origin}";
      var messageDebug = (message != null ? $"{GetTextColorize(" | Message: ", defType)}{message}" : "");
      var objectDebug = (obj != null ? $"{GetTextColorize($" | obj({obj}): ", defType)}{JsonConvert.SerializeObject(obj, Formatting.None)}" : "");

      return (originDebug + messageDebug + objectDebug);
    }

    private string GetTextColorize(string message, int debugType)
    {
      if (debugType == 1)
        return message % TextColorize.Warning;
      else if (debugType == 2)
        return message % TextColorize.Error;
      else
        return message % TextColorize.Default;
    }
    #endregion
  }
}

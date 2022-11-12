using SGOP.Util;
using UnityEngine;
using UnityEngine.Events;

namespace SGOP
{
  [DefaultExecutionOrder(-1)]
  public class AppLaunchHandler : MonoBehaviour
  {
    #region Singleton
    public static AppLaunchHandler Instance { get; private set; }
    #endregion


    #region Component Fields
    private bool isProdBuild;
    #endregion


    #region Private Fields
    #endregion


    #region Internal Fields
    internal bool IsProdBuild => isProdBuild;
    internal UnityAction BeforeStartingApplication;
    internal UnityAction StartingApplication;
    internal UnityAction AfterStartingApplication;
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

    void Start()
    {
      Loading.Instance.Show();

      BeforeStartingApplication?.Invoke();

      StartingApplication?.Invoke();

      AfterStartingApplication?.Invoke();
    }
    #endregion


    #region Private Methods
    #endregion


    #region Internal Methods
    #endregion
  }
}
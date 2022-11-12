using System.Collections;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine.Android;
#elif UNITY_IOS
using UnityEngine.iOS;
#endif

namespace SGOP.Manager
{
  public class PermissionManager : MonoBehaviour
  {
    #region Singleton
    public static PermissionManager Instance { get; private set; }
    #endregion


    #region Component Fields
    #endregion


    #region Private Fields
    #endregion


    #region Internal Fields
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

    }

    void Update()
    {

    }
    #endregion


    #region Private Methods
    #endregion


    #region Internal Methods
    internal IEnumerator ValidateMicrophonePermission()
    {
#if UNITY_ANDROID
    if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
    {
      Permission.RequestUserPermission(Permission.Microphone);
      yield return null;
    }
#elif UNITY_IOS
    yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
    if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
    {
      yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
    }
#endif
      yield return null;
    }
    #endregion
  }
}
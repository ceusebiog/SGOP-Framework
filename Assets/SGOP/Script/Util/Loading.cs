using UnityEngine;

namespace SGOP.Util
{
  public class Loading : MonoBehaviour
  {
    #region Singleton
    public static Loading Instance { get; private set; }
    #endregion


    #region Component Fields
    [SerializeField]
    private Animator animator;
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
    internal void Show()
    {
      animator.SetBool("Show", true);
    }
    internal void Hide()
    {
      animator.SetBool("Show", false);
    }
    #endregion
  }
}

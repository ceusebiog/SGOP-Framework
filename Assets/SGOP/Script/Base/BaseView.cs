using System.Collections;
using System.Collections.Generic;
using SGOP.Manager;
using SGOP.Util;
using UnityEngine;

namespace SGOP.Base
{
  public class BaseView : MonoBehaviour
  {
    #region Component Fields
    [SerializeField]
    private Animator containerAnim;
    #endregion


    #region Private Fields
    private List<IEnumerator> ListImagesToDownload;
    private List<IEnumerator> ListAudiosToDownload;
    #endregion


    #region Internal Fields
    #endregion


    #region Unity Methods
    public virtual void Awake()
    {
      ListImagesToDownload = new List<IEnumerator>();
      ListAudiosToDownload = new List<IEnumerator>();
    }
    #endregion


    #region Internal Methods
    internal void Initialization(string jsonData = "")
    {
      BeforeBuilView();
      BuildView();
      AfterBuildView();

      BeforeDownload();
      StartDownload();
    }

    internal void FinishView(float delay = 1f)
    {
      Hide();
      Destroy(gameObject, delay);
      ViewManager.Instance.CloseView(name);
    }
    #endregion

    #region Virtual Methods
    #region Build View
    public virtual void BeforeBuilView() { }

    public void BuildView()
    {
      BuildTexts();
      BuildAudios();
      BuildImages();
      BuildButtons();
      BuildDropDowns();
      BuildInputFields();
      BuildToggleItems();
    }

    public virtual void BuildTexts() { }
    public virtual void BuildAudios() { }
    public virtual void BuildImages() { }
    public virtual void BuildButtons() { }
    public virtual void BuildDropDowns() { }
    public virtual void BuildInputFields() { }
    public virtual void BuildToggleItems() { }

    public virtual void AfterBuildView() { }
    #endregion

    #region Download
    public virtual void BeforeDownload() { }

    public void StartDownload()
    {
      if (ListImagesToDownload.Count == 0 && ListAudiosToDownload.Count == 0)
      {
        AfterDownload();
        return;
      }

      foreach (var item in ListImagesToDownload)
      {
        CoroutineManager.Instance.AddCoroutine(item, "GetTexture");
      }
      foreach (var item in ListAudiosToDownload)
      {
        CoroutineManager.Instance.AddCoroutine(item, "GetAudio");
      }

      ListImagesToDownload.Clear();
      ListAudiosToDownload.Clear();
    }

    public virtual void AfterDownload()
    {
      BeforeShow();
    }
    #endregion

    #region Show
    public virtual void BeforeShow()
    {
      Show();
    }

    public virtual void Show()
    {
      containerAnim.SetBool("Show", true);
      Loading.Instance.Hide();
    }

    public virtual void Hide()
    {
      containerAnim.SetBool("Show", false);
    }

    public virtual void AfterShow() { }
    #endregion
    #endregion
  }
}
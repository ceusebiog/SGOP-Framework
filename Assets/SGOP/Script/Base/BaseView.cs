using System;
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
    protected Animator containerAnim;
    #endregion


    #region Protected Fields
    protected List<IEnumerator> ListImagesToDownload = new List<IEnumerator>();
    protected List<IEnumerator> ListAudiosToDownload = new List<IEnumerator>();
    protected bool skipNextBeforeBuildView;
    #endregion


    #region Internal Fields
    #endregion


    #region Unity Methods
    #endregion


    #region Internal Methods
    internal void Initialization(string jsonData = "")
    {
      try
      {
        BuildView();

        BeforeDownload();
        StartDownload();
      }
      catch (Exception e)
      {
        print($"catch: {e.Message}");
      }
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
    protected void BuildView()
    {
      if (!skipNextBeforeBuildView)
      {
        BeforeBuildView();
      }
      skipNextBeforeBuildView = false;

      BuildTexts();
      BuildAudios();
      BuildImages();
      BuildButtons();
      BuildDropDowns();
      BuildInputFields();
      BuildToggleItems();

      AfterBuildView();
    }

    internal virtual void BeforeBuildView() { }

    internal virtual void BuildTexts() { }
    internal virtual void BuildAudios() { }
    internal virtual void BuildImages() { }
    internal virtual void BuildButtons() { }
    internal virtual void BuildDropDowns() { }
    internal virtual void BuildInputFields() { }
    internal virtual void BuildToggleItems() { }

    internal virtual void AfterBuildView() { }
    #endregion


    #region Download
    internal virtual void BeforeDownload() { }

    protected void StartDownload()
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

    // Call when end downloads
    internal virtual void AfterDownload()
    {
      Show();
    }
    #endregion


    #region Show
    internal void Show()
    {
      BeforeShow();
      containerAnim.SetBool("Show", true);
      AfterShow();
      Loading.Instance.Hide();
    }

    internal virtual void BeforeShow() { }

    internal virtual void AfterShow() { }
    #endregion


    #region Hide
    internal void Hide()
    {
      BeforeHide();
      containerAnim.SetBool("Show", false);
      AfterHide();
    }

    internal virtual void BeforeHide() { }

    internal virtual void AfterHide() { }
    #endregion
    #endregion
  }
}
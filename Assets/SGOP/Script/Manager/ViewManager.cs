using System;
using System.Collections.Generic;
using SGOP.Base;
using SGOP.Util;
using SGOP.Util.Type;
using UnityEngine;

namespace SGOP.Manager
{
  public class ViewManager : MonoBehaviour
  {
    #region Singleton
    public static ViewManager Instance { get; private set; }
    #endregion


    #region Component Fields
    [SerializeField]
    private GameObject loadingPrefab;
    #endregion


    #region Private Fields
    private RectTransform debugViewContainer;
    private RectTransform pageViewContainer;
    private RectTransform menuViewContainer;
    private RectTransform popUpViewContainer;
    private List<string> listPreviousPageNames;
    private Dictionary<string, GameObject> openViewsGO;
    private bool isOnFinishPagesAfter;
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
      isOnFinishPagesAfter = false;

      (debugViewContainer = new GameObject("Debug View Container", typeof(RectTransform)).transform as RectTransform).SetParent(transform);
      (pageViewContainer = new GameObject("Page View Container", typeof(RectTransform)).transform as RectTransform).SetParent(transform);
      (menuViewContainer = new GameObject("Menu View Container", typeof(RectTransform)).transform as RectTransform).SetParent(transform);
      (popUpViewContainer = new GameObject("Pop-Up View Container", typeof(RectTransform)).transform as RectTransform).SetParent(transform);
      Instantiate(loadingPrefab, transform).name = "Loading";

      debugViewContainer.anchorMin = Vector2.zero;
      debugViewContainer.anchorMax = Vector2.one;
      debugViewContainer.anchoredPosition3D = Vector3.zero;
      debugViewContainer.offsetMin = Vector2.zero;
      debugViewContainer.offsetMax = Vector2.zero;
      debugViewContainer.localScale = Vector3.one;

      pageViewContainer.anchorMin = Vector2.zero;
      pageViewContainer.anchorMax = Vector2.one;
      pageViewContainer.anchoredPosition3D = Vector3.zero;
      pageViewContainer.offsetMin = Vector2.zero;
      pageViewContainer.offsetMax = Vector2.zero;
      pageViewContainer.localScale = Vector3.one;

      menuViewContainer.anchorMin = Vector2.zero;
      menuViewContainer.anchorMax = Vector2.one;
      menuViewContainer.anchoredPosition3D = Vector3.zero;
      menuViewContainer.offsetMin = Vector2.zero;
      menuViewContainer.offsetMax = Vector2.zero;
      menuViewContainer.localScale = Vector3.one;

      popUpViewContainer.anchorMin = Vector2.zero;
      popUpViewContainer.anchorMax = Vector2.one;
      popUpViewContainer.anchoredPosition3D = Vector3.zero;
      popUpViewContainer.offsetMin = Vector2.zero;
      popUpViewContainer.offsetMax = Vector2.zero;
      popUpViewContainer.localScale = Vector3.one;

      listPreviousPageNames = new List<string>();
      openViewsGO = new Dictionary<string, GameObject>();
    }

    void Start()
    {

    }

    void Update()
    {

    }
    #endregion


    #region Private Methods
    private void AddToPreviousPages(string pageName)
    {
      if (listPreviousPageNames.Count > 0 && listPreviousPageNames.Contains(pageName))
      {
        FinishPagesAfter(pageName);
      }
      else
      {
        HidePreviousPages(pageName);
        listPreviousPageNames.Add(pageName);
      }
    }

    private void FinishPagesAfter(string crrPageName)
    {
      isOnFinishPagesAfter = true;
      var ind = listPreviousPageNames.IndexOf(crrPageName);

      for (int i = ind + 1; i < listPreviousPageNames.Count; i++)
      {
        if (openViewsGO.TryGetValue(listPreviousPageNames[i], out var go))
        {
          go.GetComponent<BaseView>().FinishView();
        }
      }

      listPreviousPageNames.RemoveRange(ind + 1, listPreviousPageNames.Count - (ind + 1));
      isOnFinishPagesAfter = false;
    }

    private void HidePreviousPages(string pageName)
    {
      foreach (var page in openViewsGO)
      {
        if (page.Key != pageName && listPreviousPageNames.Contains(page.Key))
        {
          page.Value.GetComponent<BaseView>().Hide();
        }
      }
    }

    private void EndPageOnBack()
    {
      var len = listPreviousPageNames.Count - 1;
      var pageName = listPreviousPageNames[len];
      listPreviousPageNames.RemoveAt(len);

      openViewsGO[pageName].GetComponent<BaseView>().FinishView();
    }
    #endregion


    #region Internal Methods
    internal GameObject OpenView(string viewName, string urlJson = "", ViewType viewType = ViewType.Page, bool withoutContent = false)
    {
      Loading.Instance.Show();
      if (string.IsNullOrWhiteSpace(viewName))
      {
        DebugManager.Instance.Error($"{name}.OpenView", $"Try to open view with empty ViewName");

        Loading.Instance.Hide();
        return null;
      }

      Transform parentI;
      switch (viewType)
      {
        default:
        case ViewType.Debug:
          parentI = debugViewContainer;
          break;
        case ViewType.Page:
          parentI = pageViewContainer;
          break;
        case ViewType.Menu:
          parentI = menuViewContainer;
          break;
        case ViewType.PopUp:
          parentI = popUpViewContainer;
          break;
      }

      if (viewType.Equals(ViewType.Page))
      {
        AddToPreviousPages(viewName);
      }

      if (openViewsGO.TryGetValue(viewName, out var view) && view.transform.GetSiblingIndex() != parentI.childCount - 1)
      {
        DebugManager.Instance.Log($"{name}.OpenView", $"Show view opened: {viewName} |");

        view.transform.SetSiblingIndex(parentI.childCount);
        view.GetComponent<BaseView>().Show();
        return view;
      }
      else
      {
        DebugManager.Instance.Log($"{name}.OpenView", $"Load view: {viewName} | urlJson: {urlJson}");

        view = Instantiate(Resources.Load($"Prefabs/View/{viewType}/{viewName}", typeof(GameObject)), parentI) as GameObject;
        view.name = viewName;
        openViewsGO.Add(viewName, view);

        if (withoutContent)
        {
          var baseview = view.GetComponent<BaseView>();
          if (baseview)
            baseview.Initialization();
        }
        else
        {
          DownloadManager.Instance.DownloadJson(
            urlJson,
            (jsonData) => view.GetComponent<BaseView>().Initialization(jsonData)
          );
        }
      }
      return view;
    }

    internal void CloseView(string viewName)
    {
      if (openViewsGO.Remove(viewName, out var go))
      {
        var pageNameInd = listPreviousPageNames.FindIndex(x => x.Equals(viewName, StringComparison.InvariantCultureIgnoreCase));
        if (pageNameInd != -1 && !isOnFinishPagesAfter)
          listPreviousPageNames.RemoveAt(pageNameInd);
        DebugManager.Instance.Log($"{name}.CloseView", $"Removed view {viewName}");
      }
    }

    internal void EndAllViews()
    {
      var basePageList = new List<BaseView>();
      foreach (var item in openViewsGO)
      {
        basePageList.Add(item.Value.GetComponent<BaseView>());
      }

      foreach (var item in basePageList)
      {
        item.FinishView(0);
      }

      listPreviousPageNames.Clear();
    }

    internal void EndAllPages()
    {
      var basePageList = new List<BaseView>();
      foreach (var item in openViewsGO)
      {
        if (listPreviousPageNames.Contains(item.Key))
          basePageList.Add(item.Value.GetComponent<BaseView>());
      }

      foreach (var item in basePageList)
      {
        item.FinishView(0);
      }

      listPreviousPageNames.Clear();
    }

    internal void GoToBack()
    {
      var len = listPreviousPageNames.Count - 2;
      if (len >= 0)
      {
        DebugManager.Instance.Log($"{name}.GoToBack", $"Back to page: {listPreviousPageNames[len]}");
        EndPageOnBack();
        OpenView(listPreviousPageNames[len]);
      }
    }
    #endregion
  }
}

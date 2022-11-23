using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SGOP.Manager
{
  public class CoroutineManager : MonoBehaviour
  {
    #region Singleton
    public static CoroutineManager Instance { get; private set; }
    #endregion


    #region Component Fields
    [SerializeField]
    private int maxCoroutinesAtATime = 5;
    #endregion


    #region Private Fields
    private List<IEnumerator> listCoroutinesToExecute;
    private List<string> listCoroutineNamesToExecute;
    private int currentCoroutinesRunning;
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
      listCoroutinesToExecute = new List<IEnumerator>();
      listCoroutineNamesToExecute = new List<string>();
      currentCoroutinesRunning = 0;
    }

    void Start()
    {

    }

    void Update()
    {
      var length = listCoroutinesToExecute.Count;
      for (int i = 0; i < length; i++)
      {
        if (currentCoroutinesRunning < maxCoroutinesAtATime)
        {
          currentCoroutinesRunning++;
          var coroutine = listCoroutinesToExecute[i];
          listCoroutinesToExecute.RemoveAt(i);
          length--;
          StartCoroutine(coroutine);
        }
      }
    }

    void OnApplicationQuit()
    {
      StopAllCoroutines();
    }
    #endregion


    #region Private Methods
    #endregion


    #region Internal Methods
    internal void AddCoroutine(IEnumerator coroutine, string name, bool ToList = true)
    {
      if (ToList)
      {
        listCoroutinesToExecute.Add(coroutine);
        listCoroutineNamesToExecute.Add(name);
      }
      else
        StartCoroutine(coroutine);
    }

    internal void EndCoroutine(string name)
    {
      listCoroutineNamesToExecute.Remove(name);
      currentCoroutinesRunning--;
    }
    
    internal void SetMaxCoroutinesAtATime(int max)
    {
      maxCoroutinesAtATime = max;
    }
    #endregion
  }
}

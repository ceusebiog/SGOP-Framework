using System.Collections;
using System.IO;
using SGOP.Model;
using SGOP.Util;
using SGOP.Util.Type;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace SGOP.Manager
{
  public class DownloadManager : MonoBehaviour
  {
    #region Singleton
    public static DownloadManager Instance { get; private set; }
    #endregion


    #region Component Fields
    #endregion


    #region Private Fields
    private string imageFolderPath;
    private string audioFolderPath;
    private string cacheFilePath;
    private CacheItems cacheItems;
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

      imageFolderPath = Path.Combine(Application.persistentDataPath, Constants.FOLDER_IMAGE);
      audioFolderPath = Path.Combine(Application.persistentDataPath, Constants.FOLDER_AUDIO);
      cacheFilePath = Path.Combine(Application.persistentDataPath, "Cache.json");

      Directory.CreateDirectory(imageFolderPath);
      Directory.CreateDirectory(audioFolderPath);
    }

    void Start()
    {
      LoadSave.LoadJson<CacheItems>(ref cacheItems, cacheFilePath);
    }

    void Update()
    {

    }
    #endregion


    #region Private Methods
    private bool SaveCacheFile(string fileName, FileType fileType)
    {
      var res = false;
      switch (fileType)
      {
        default:
          break;
        case FileType.Image:
          if (!cacheItems.Images.Contains(fileName))
          {
            cacheItems.Images.Add(fileName);
            res = true;
          }
          break;
        case FileType.Audio:
          if (!cacheItems.Audios.Contains(fileName))
          {
            cacheItems.Audios.Add(fileName);
            res = true;
          }
          break;
      }

      if (res) LoadSave.SaveJson<CacheItems>(cacheItems, cacheFilePath);

      return res;
    }

    private void SaveItemToCache(string fileName, byte[] data, FileType fileType)
    {
      if (SaveCacheFile(fileName, fileType))
      {
        var filePath = "";
        switch (fileType)
        {
          default:
            break;
          case FileType.Image:
            filePath = imageFolderPath;
            break;
          case FileType.Audio:
            filePath = audioFolderPath;
            break;
        }
        filePath = Path.Combine(filePath, fileName);

        File.WriteAllBytes(filePath, data);

        DebugManager.Instance.Log($"{name}.SaveToCache", filePath);
      }
    }

    private string GetBasePath(bool download)
    {
      if (download)
      {
        return Constants.BUCKET_CONTENT_PATH;
      }
      else
      {
        var prefix = "";
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
          prefix = Constants.FILE_PREF_UNIX;

        return $"{prefix}{Application.persistentDataPath}";
      }
    }

    private IEnumerator ExecuteUWR(UnityWebRequest uwr, UnityAction<UnityWebRequest> callback = null, UnityAction callbackError = null)
    {
      yield return uwr.SendWebRequest();

      var time = 0f;
      while (uwr.result == UnityWebRequest.Result.InProgress && time < 5f)
      {
        time += Time.deltaTime;
        yield return null;
      }

      if (uwr.result != UnityWebRequest.Result.Success)
      {
        callbackError?.Invoke();
      }
      else
      {
        callback?.Invoke(uwr);
      }

      CoroutineManager.Instance.EndCoroutine("ExecuteUWR");
    }
    #endregion


    #region Internal Methods
    internal void ClearCache()
    {
      cacheItems = new CacheItems();
      LoadSave.SaveJson<CacheItems>(cacheItems, cacheFilePath);
    }

    internal void DownloadJson(string url, UnityAction<string> callback, UnityAction callbackError = null)
    {
      DebugManager.Instance.Log($"{name}.DownloadJson", url);

      var uwr = UnityWebRequest.Get(url);

      CoroutineManager.Instance.AddCoroutine(ExecuteUWR(uwr, (uwr) =>
      {
        var json = uwr.downloadHandler.text;

        DebugManager.Instance.Log($"{name}.DownloadJson", url, obj: json);

        callback?.Invoke(json);
      }, () =>
      {
        DebugManager.Instance.Error($"{name}.DownloadJson", $"url: {url} | uwr.result: {uwr.result} | text: {uwr.downloadHandler.text}", uwr.error);

        callbackError?.Invoke();
      }), "ExecuteUWR");
    }

    internal void DownloadTexture(string fileName, string origin, UnityAction<Texture2D> callback, UnityAction callbackError = null)
    {
      if (string.IsNullOrWhiteSpace(fileName))
      {
        DebugManager.Instance.Error($"DownloadTexture | {origin}", "Error on downloaded texture (fileName => IsNullOrWhiteSpace)");

        callbackError?.Invoke();
      }
      else
      {
        fileName = System.IO.Path.GetFileName(fileName);

        var download = !cacheItems.Images.Contains(fileName);
        var url = $"{GetBasePath(download)}/{Constants.FOLDER_IMAGE}/{fileName}";
        var debugTitle = download ? "DownloadTexture" : "LoadTextureFromCache";

        var uwr = UnityWebRequestTexture.GetTexture(url);

        CoroutineManager.Instance.AddCoroutine(ExecuteUWR(uwr, (uwr) =>
        {
          DebugManager.Instance.Log(debugTitle, $"{origin} downloaded {fileName} from {url}");

          callback(DownloadHandlerTexture.GetContent(uwr));

          SaveItemToCache(fileName, uwr.downloadHandler.data, FileType.Image);
        }, () =>
        {
          DebugManager.Instance.Error(debugTitle, $"{origin} error on downloaded {fileName} from {url} | text: {uwr.downloadHandler.text}", uwr.error);

          callbackError?.Invoke();
        }), "ExecuteUWR");
      }
    }

    internal void DownloadAudio(string fileName, string origin, UnityAction<AudioClip> callback, UnityAction callbackError = null)
    {
      if (string.IsNullOrWhiteSpace(fileName))
      {
        DebugManager.Instance.Error($"GetTexture | {origin}", "Error on downloaded audio (fileName => IsNullOrWhiteSpace)");

        callbackError?.Invoke();
      }
      else
      {
        fileName = System.IO.Path.GetFileName(fileName);

        var download = !cacheItems.Images.Contains(fileName);
        var url = $"{GetBasePath(download)}/{Constants.FOLDER_IMAGE}/{fileName}";
        var debugTitle = download ? "DownloadAudio" : "LoadAudioFromCache";

        var uwr = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS);

        // TODO: startcoroutine
        CoroutineManager.Instance.AddCoroutine(ExecuteUWR(uwr, (uwr) =>
        {
          DebugManager.Instance.Log(debugTitle, $"{origin} downloaded {fileName} from {url}");

          callback(DownloadHandlerAudioClip.GetContent(uwr));

          SaveItemToCache(fileName, uwr.downloadHandler.data, FileType.Audio);
        }, () =>
        {
          DebugManager.Instance.Error(debugTitle, $"{origin} error on downloaded {fileName} from {url} | text: {uwr.downloadHandler.text}", uwr.error);

          callbackError?.Invoke();
        }), "ExecuteUWR");
      }
    }
    #endregion
  }
}

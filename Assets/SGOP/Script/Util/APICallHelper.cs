using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.Events;
using Newtonsoft.Json;
using UnityEngine;
using System;
using SGOP.Manager;
using SGOP.Base;
using SGOP.Model.Response;

namespace SGOP.Util
{
  public class APICallHelper
  {
    public static void Post<TReq, TRes>(string url, TReq request, UnityAction<BaseResponse> callback, UnityAction<BaseResponse> errorCallback = null) where TReq : BaseRequest where TRes : BaseResponse
    {
      DebugManager.Instance.Log("APICallHelper.Post", $"url: {url}", obj: request);

      var json = JsonConvert.SerializeObject(request, Formatting.None);
      var data = Encoding.UTF8.GetBytes(json);

      var uploadHandler = new UploadHandlerRaw(data);
      uploadHandler.contentType = "application/x-www-form-urlencoded";

      var uwr = new UnityWebRequest(url, "POST");

      uwr.uploadHandler = uploadHandler;
      uwr.downloadHandler = new DownloadHandlerBuffer();
      uwr.timeout = 15;
      uwr.SetRequestHeader("Content-Type", "application/json");

      CoroutineManager.Instance.AddCoroutine(Send<TRes>(uwr, callback, errorCallback), "Send");
    }

    public static void Post<TRes>(string url, WWWForm request, UnityAction<BaseResponse> callback, UnityAction<BaseResponse> errorCallback = null) where TRes : BaseResponse
    {
      DebugManager.Instance.Log("APICallHelper.Post", $"url: {url}", obj: request);

      var uwr = UnityWebRequest.Post(url, request);

      CoroutineManager.Instance.AddCoroutine(Send<TRes>(uwr, callback, errorCallback), "Send");
    }

    public static void Get<T>(string url, Dictionary<string, string> Params, UnityAction<BaseResponse> callback, UnityAction<BaseResponse> errorCallback = null) where T : BaseResponse
    {
      if (Params != null && Params.Count > 0)
      {
        var fparam = Params.First();
        url += $"?{fparam.Key}={fparam.Value}";
        Params.Remove(fparam.Key);

        foreach (var param in Params)
        {
          url += $"&{param.Key}={param.Value}";
        }
      }

      DebugManager.Instance.Log("APICallHelper.Get", $"url: {url}");

      var uwr = UnityWebRequest.Get(url);
      CoroutineManager.Instance.AddCoroutine(Send<T>(uwr, callback, errorCallback), "Send");
    }

    private static IEnumerator Send<T>(UnityWebRequest uwr, UnityAction<BaseResponse> callback, UnityAction<BaseResponse> errorCallback = null) where T : BaseResponse
    {
      yield return uwr.SendWebRequest();
      var time = 0f;
      while (uwr.result == UnityWebRequest.Result.InProgress && time < 5f)
      {
        time += Time.deltaTime;
        yield return null;
      }

      try
      {
        if (uwr.result != UnityWebRequest.Result.Success)
        {
          var jsonData = uwr.downloadHandler.text;
          DebugManager.Instance.Error("APICallHelper.Send", $"Response: {jsonData}", uwr.error);

          var data = JsonConvert.DeserializeObject<MessageResponse>(jsonData);

          errorCallback?.Invoke(data);
        }
        else
        {
          var jsonData = uwr.downloadHandler.text;

          DebugManager.Instance.Log("APICallHelper.Send", $"Response: {jsonData}");

          var data = JsonConvert.DeserializeObject<T>(jsonData);

          callback?.Invoke(data);
        }
      }
      catch (Exception e)
      {
        DebugManager.Instance.Error("APICallHelper.Send", $"Catch: {e.Message}");
        errorCallback?.Invoke(null);
      }

      CoroutineManager.Instance.EndCoroutine("Send");
    }
  }
}

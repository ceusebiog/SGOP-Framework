using System;
using System.IO;
using SGOP.Manager;
using Newtonsoft.Json;

namespace SGOP.Util
{
  public static class LoadSave
  {
    internal static void LoadJson<T>(ref T refVar, string path) where T : new()
    {
      if (File.Exists(path))
      {
        try
        {
          var json = File.ReadAllText(path);
          refVar = JsonConvert.DeserializeObject<T>(json);
          DebugManager.Instance.Log("IOJsonData.Load", $"path: {path}", obj: refVar);
        }
        catch (Exception e)
        {
          DebugManager.Instance.Exception("IOJsonData.Load", obj: e);
        }
      }
      else
      {
        refVar = new T();
        SaveJson(refVar, path);
      }
    }

    internal static void SaveJson<T>(T dataToSave, string path)
    {
      try
      {
        DebugManager.Instance.Log("Save", $"path: {path}", obj: dataToSave);
        var json = JsonConvert.SerializeObject(dataToSave, Formatting.Indented);
        File.WriteAllText(path, json);
      }
      catch (Exception e)
      {
        DebugManager.Instance.Exception("Save", obj: e);
      }
    }
  }
}

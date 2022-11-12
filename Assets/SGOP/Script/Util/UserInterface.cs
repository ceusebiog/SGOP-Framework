using System;
using System.Collections.Generic;
using SGOP.Manager;
using SGOP.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SGOP.Util
{
  public class UserInterface
  {
    #region Private Methods
    private static void ImageDownloadFinished(IImageItem imageItem, GameObject imageGO, Texture2D texture, UnityAction onComplete = null, UnityAction callbackError = null)
    {
      try
      {
        if (imageGO == null)
        {
          DebugManager.Instance.Error($"UserInterface.ImageDownloadFinished", "object To Set Image it is destroyed");
          return;
        }

        var rawImage = imageGO.GetComponent<RawImage>();
        var image = imageGO.GetComponent<Image>();

        if (texture == null)
        {
          if (rawImage != null)
            rawImage.enabled = false;
          else if (image != null)
            image.enabled = false;

          return;
        }

        onComplete?.Invoke();
        if (rawImage != null)
        {
          rawImage.texture = texture;
          rawImage.color = imageItem.GetColor();
          rawImage.enabled = imageItem.IsVisible;
        }
        else if (image != null)
        {
          image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
          image.color = imageItem.GetColor();
          image.enabled = imageItem.IsVisible;
        }
        else
        {
          DebugManager.Instance.Warning($"UserInterface.ImageDownloadFinished", "object To Set Image dont have Image component");

          imageGO.SetActive(false);
          return;
        }
      }
      catch (Exception e)
      {
        DebugManager.Instance.Exception($"UserInterface.ImageDownloadFinished", obj: e);
        callbackError?.Invoke();
      }
    }

    private static void AudioDownloadFinished(IMediaItem audioItem, AudioClip audioClip, UnityAction<AudioClip> onComplete = null, UnityAction callbackError = null)
    {
      onComplete?.Invoke(audioClip);
    }
    #endregion


    #region Internal Methods
    public static TMP_Text SetTextItem(ITextItem textItem, TMP_Text text, RectTransform FitToObject = null)
    {
      // TODO: verify for empty string  && !string.IsNullOrWhiteSpace(textItem.text)
      if (textItem != null && text != null)
      {
        text.text = textItem.Text;
        text.color = textItem.GetColor();
        text.fontSize = textItem.FontSize;
        text.fontSizeMin = textItem.MinFontSize;
        text.fontSizeMax = textItem.MaxFontSize;
        text.enableAutoSizing = textItem.AutoSize;
        text.horizontalAlignment = (HorizontalAlignmentOptions)Enum.Parse(typeof(HorizontalAlignmentOptions), textItem.HorizontalAlign);
        text.verticalAlignment = (VerticalAlignmentOptions)Enum.Parse(typeof(VerticalAlignmentOptions), textItem.VerticalAlign);

        text.gameObject.SetActive(textItem.IsVisible);

        return text;
      }
      else if (text != null)
      {
        text.gameObject.SetActive(false);
        return text;
      }
      else
      {
        DebugManager.Instance.Warning($"UserInterface.SetTextItem", "components null");
        return null;
      }
    }

    public static void SetImageItem(IImageItem imageItem, GameObject imageGO, UnityAction onComplete = null, UnityAction callbackError = null)
    {
      if (imageItem != null && imageGO != null)
      {
        if (string.IsNullOrWhiteSpace(imageItem.Url))
        {
          ImageDownloadFinished(imageItem, imageGO, null, onComplete, callbackError);
        }
        else
        {
          DownloadManager.Instance.DownloadTexture(imageItem.Url, "SetImageItem", (texture) =>
          {
            ImageDownloadFinished(imageItem, imageGO, texture, onComplete, callbackError);
          }, callbackError);
        }
        return;
      }
      else if (imageGO != null)
      {
        RawImage rawImage = imageGO.GetComponent<RawImage>();
        Image image = imageGO.GetComponent<Image>();

        if (rawImage != null)
          rawImage.enabled = false;
        else if (image != null)
          image.enabled = false;
      }
      else
      {
        DebugManager.Instance.Warning($"UserInterface.SetImageItem", "components null");
        callbackError?.Invoke();
      }
    }

    public static void SetImageItem(GameObject fromImageGO, GameObject toImageGO)
    {
      try
      {
        if (fromImageGO == null)
        {
          DebugManager.Instance.Error($"UserInterface.SetImageItem", "object From Get Image it is destroyed");
          return;
        }
        if (toImageGO == null)
        {
          DebugManager.Instance.Error($"UserInterface.SetImageItem", "object To Set Image it is destroyed");
          return;
        }
        var rawImageFrom = fromImageGO.GetComponent<RawImage>();
        var imageFrom = fromImageGO.GetComponent<Image>();

        var rawImageTo = toImageGO.GetComponent<RawImage>();
        var imageTo = toImageGO.GetComponent<Image>();

        Texture texture;
        Color color;

        if (rawImageFrom != null)
        {
          texture = rawImageFrom.texture;
          color = rawImageFrom.color;
        }
        else if (imageFrom != null)
        {
          texture = imageFrom.sprite.texture;
          color = imageFrom.color;
        }
        else
        {
          DebugManager.Instance.Warning($"UserInterface.SetImageItem", "object To Get Image dont have Image component");

          fromImageGO.SetActive(false);
          return;
        }

        if (rawImageTo != null)
        {
          rawImageTo.texture = texture;
          rawImageTo.color = color;
        }
        else if (imageTo != null)
        {
          imageTo.sprite = Sprite.Create(texture as Texture2D, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
          imageTo.color = color;
        }
        else
        {
          DebugManager.Instance.Warning($"UserInterface.SetImageItem", "object To Set Image dont have Image component");

          toImageGO.SetActive(false);
          return;
        }
      }
      catch (Exception e)
      {
        DebugManager.Instance.Exception($"UserInterface.SetImageItem", obj: e);
      }
    }

    public static void SetAudioItem(IMediaItem mediaItem, UnityAction<AudioClip> onComplete = null, UnityAction callbackError = null)
    {
      if (mediaItem != null)
      {
        if (string.IsNullOrWhiteSpace(mediaItem.Url))
        {
          AudioDownloadFinished(mediaItem, null, onComplete, callbackError);
          return;
        }
        else
        {
          DownloadManager.Instance.DownloadAudio(mediaItem.Url, "SetAudioItem", (audioClip) =>
          {
            AudioDownloadFinished(mediaItem, audioClip, onComplete, callbackError);
          }, callbackError);
          return;
        }
      }
      else
      {
        DebugManager.Instance.Warning($"UserInterface.SetAudioItem", "components null");
        callbackError?.Invoke();
      }
    }

    public static Button SetButtonExtendedItem(IButtonItem buttonItem, Button button, UnityAction onClicAction = null, UnityAction callbackError = null)
    {
      if (buttonItem != null && button != null)
      {
        SetImageItem(buttonItem.BackgroundImage, button.image?.gameObject);

        // SetTextItem(buttonItem.ButtonText, button.text);

        button.onClick.RemoveAllListeners();

        if (onClicAction != null) button.onClick.AddListener(onClicAction);

        button.gameObject.SetActive(buttonItem.IsVisible);
        return button;
      }
      else if (button != null)
      {
        button.gameObject.SetActive(false);
        return button;
      }
      else
      {
        DebugManager.Instance.Warning($"UserInterface.SetButtonItem", "components null");
        return null;
      }
    }

    public static Dropdown SetDropDownItem(IDropDownItem dropDownItem, Dropdown dropdown, List<string> items, UnityAction<int> onChangeValue = null, int selectedItem = -1)
    {
      if (dropDownItem != null && dropdown != null)
      {
        SetImageItem(dropDownItem.BackgroundImage, dropdown.gameObject);

        dropdown.ClearOptions();
        dropdown.AddOptions(items);

        if (onChangeValue != null)
          dropdown.onValueChanged.AddListener(onChangeValue);

        if (selectedItem != -1)
          dropdown.SetValueWithoutNotify(selectedItem);

        return dropdown;
      }
      else if (dropdown != null)
      {
        dropdown.gameObject.SetActive(false);
        return dropdown;
      }
      else
      {
        DebugManager.Instance.Warning($"UserInterface.SetDropDownItem", "components null");
        return null;
      }
    }

    public static InputField SetInputFieldItem(IInputFieldItem inputFieldItem, InputField inputField)
    {
      if (inputFieldItem != null && inputField != null)
      {
        SetImageItem(inputFieldItem.BackgroundImage, inputField.gameObject);

        return inputField;
      }
      else if (inputField != null)
      {
        inputField.gameObject.SetActive(false);
        return inputField;
      }
      else
      {
        DebugManager.Instance.Warning($"UserInterface.SetInputFieldItem", "components null");
        return null;
      }
    }

    public static Toggle SetToggleItem(IToggleItem toggleItem, Toggle toggle, UnityAction<bool> onChangeAction = null)
    {
      if (toggleItem != null && toggle != null)
      {
        SetImageItem(toggleItem.CheckmarkImage, toggle.image.gameObject);

        toggle.onValueChanged.RemoveAllListeners();

        if (onChangeAction != null)
          toggle.onValueChanged.AddListener(onChangeAction);

        return toggle;
      }
      else if (toggle != null)
      {
        toggle.gameObject.SetActive(false);
        return toggle;
      }
      else
      {
        DebugManager.Instance.Warning($"UserInterface.SetToggleItem", "components null");
        return null;
      }
    }
    #endregion
  }
}
using UnityEngine;

namespace SGOP.Model
{
  public interface IViewData
  {
    string PageName { get; set; }
    IImageItem BackgroundImage { get; set; }
    ITextItem PageTitleText { get; set; }
  }

  public interface IPageData : IViewData
  {
    IMediaItem BackgroundAudio { get; set; }
    ITextItem PageSubtitleText { get; set; }
  }

  public interface IItem
  {
    bool IsVisible { get; set; }
  }

  public interface IInterectable
  {
    bool IsInteractable { get; set; }
  }

  public interface IColor
  {
    string Color { get; set; }

    public Color GetColor()
    {
      if (ColorUtility.TryParseHtmlString(Color, out var newColor))
        return newColor;

      return new Color(1, 1, 1, 1);
    }
  }

  public interface IToolTip
  {
    ITextItem ToolTipText { get; set; }
  }

  public interface IErrorMessage
  {
    ITextItem ErrorText { get; set; }
  }


  #region Base Items
  public interface ITextItem : IItem, IColor
  {
    string Text { get; set; }
    string HorizontalAlign { get; set; }
    string VerticalAlign { get; set; }
    float MinFontSize { get; set; }
    float MaxFontSize { get; set; }
    float FontSize { get; set; }
    bool AutoSize { get; set; }
  }

  public interface IMediaItem
  {
    string Url { get; set; }
  }

  public interface IImageItem : IMediaItem, IItem, IColor
  { }
  #endregion


  #region Fields
  public interface IButtonItem : IItem, IInterectable
  {
    IImageItem BackgroundImage { get; set; }
    ITextItem ButtonText { get; set; }
    // string OnClicAction { get; set; }
    // string[] OnClicActionData { get; set; }
  }

  public interface IDropDownItem : IItem, IInterectable, IToolTip, IErrorMessage
  {
    IImageItem BackgroundImage { get; set; }
    IImageItem ElementBackgroundImage { get; set; }
    ITextItem TitleText { get; set; }
    ITextItem DropDownText { get; set; }
  }

  public interface IInputFieldItem : IItem, IInterectable, IToolTip, IErrorMessage
  {
    IImageItem BackgroundImage { get; set; }
    ITextItem TitleText { get; set; }
    ITextItem PlaceholderText { get; set; }
    ITextItem InputFieldText { get; set; }
  }

  public interface IToggleItem : IItem, IInterectable, IToolTip, IErrorMessage
  {
    IImageItem BackgroundImage { get; set; }
    IImageItem CheckmarkImage { get; set; }
    ITextItem ToggleText { get; set; }
  }

  public interface ISliderItem : IItem, IInterectable
  {
    IImageItem BackgroundImage { get; set; }
    IImageItem FillImage { get; set; }
    IImageItem HandlerImage { get; set; }
  }
  #endregion
}

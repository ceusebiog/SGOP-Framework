using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SGOP.Manager;
using SGOP.UI;
using System.Collections.Generic;
using TMPro;

namespace SGOP
{
  public class MenuSGOP : MonoBehaviour
  {
    // Serious Games On Pages
    [MenuItem("GameObject/SGOP/Create Managers", false, -1)]
    static void CreateManagers()
    {
      var mT = new GameObject("Managers").transform;
      mT.gameObject.AddComponent<AppLaunchHandler>();

      // Create App Manager
      // var amGO = new GameObject("App Manager");
      // amGO.transform.SetParent(mT);
      // amGO.AddComponent<AppManager>();
      // Create App Manager

      // Create Audio Manager
      var aumGO = new GameObject("Audio Manager");
      aumGO.transform.SetParent(mT);
      aumGO.AddComponent<AudioManager>();
      // Create Audio Manager

      // Create Coroutine Manager
      var cmGO = new GameObject("Coroutine Manager");
      cmGO.transform.SetParent(mT);
      cmGO.AddComponent<CoroutineManager>();
      // Create Coroutine Manager

      // Create Debug Manager
      var dmGO = new GameObject("Debug Manager");
      dmGO.transform.SetParent(mT);
      dmGO.AddComponent<DebugManager>();
      // Create Debug Manager

      // Create Download Manager
      var domGO = new GameObject("Download Manager");
      domGO.transform.SetParent(mT);
      domGO.AddComponent<DownloadManager>();
      // Create Download Manager

      // Create Download Manager
      var pmGO = new GameObject("Permission Manager");
      pmGO.transform.SetParent(mT);
      pmGO.AddComponent<PermissionManager>();
      // Create Download Manager

      // Create View Manager
      var vmGO = new GameObject("View Manager");
      vmGO.transform.SetParent(mT);
      var vmC = vmGO.AddComponent<Canvas>();
      vmC.renderMode = RenderMode.ScreenSpaceCamera;
      vmC.worldCamera = Camera.main;
      vmC.pixelPerfect = false;
      var vmCS = vmGO.AddComponent<CanvasScaler>();
      vmCS.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
      vmCS.referenceResolution = new Vector2(1920, 1080);
      vmCS.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
      vmCS.matchWidthOrHeight = .5f;
      vmCS.referencePixelsPerUnit = 100;
      var vmGR = vmGO.AddComponent<GraphicRaycaster>();
      vmGR.ignoreReversedGraphics = true;
      vmGR.blockingObjects = GraphicRaycaster.BlockingObjects.None;
      vmGR.blockingMask = ~0;
      vmGO.AddComponent<ViewManager>();
      // Create View Manager

      // Create EventSystem
      var esGO = new GameObject("EventSystem");
      esGO.AddComponent<EventSystem>();
      esGO.AddComponent<StandaloneInputModule>();
      // Create EventSystem
    }

    [MenuItem("GameObject/UI/Button (SGOP)", false, -1)]
    static void CreateButtonSGOP()
    {
      ColorUtility.TryParseHtmlString("#323232", out var color);

      var bGO = new GameObject("Button SGOP", typeof(RectTransform));
      var bT = bGO.transform;
      bGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var bSGOP = bGO.AddComponent<ButtonSGOP>();

      var bbiGO = new GameObject("Background", typeof(RectTransform));
      bbiGO.transform.SetParent(bT);
      bbiGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var bbI = bbiGO.AddComponent<Image>();
      bbI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
      bbI.type = Image.Type.Sliced;
      var bbiRT = bbiGO.transform as RectTransform;
      bbiRT.anchorMin = Vector2.zero;
      bbiRT.anchorMax = Vector2.one;
      bbiRT.offsetMin = Vector2.zero;
      bbiRT.offsetMax = Vector2.zero;

      var tGO = new GameObject("Text");
      tGO.transform.SetParent(bT);
      tGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var tTx = tGO.AddComponent<TextMeshProUGUI>();
      tTx.text = "Button";
      tTx.color = color;
      tTx.fontSize = 42;
      tTx.alignment = TextAlignmentOptions.Center;
      var tRT = tGO.transform as RectTransform;
      tRT.anchorMin = Vector2.zero;
      tRT.anchorMax = Vector2.one;
      tRT.offsetMin = Vector2.zero;
      tRT.offsetMax = Vector2.zero;

      bSGOP.targetGraphic = bbI;
      bSGOP.text = tTx;

      bT.SetParent(Selection.activeTransform);

      var bRT = bGO.GetComponent<RectTransform>();
      bRT.anchoredPosition3D = Vector3.zero;
      bRT.localScale = Vector3.one;
      bRT.sizeDelta = new Vector2(480, 90);
    }

    [MenuItem("GameObject/UI/Dropdown (SGOP)", false, -1)]
    static void CreateDropdownSGOP()
    {
      ColorUtility.TryParseHtmlString("#323232", out var color);

      var ddGO = new GameObject("Dropdown SGOP", typeof(RectTransform));
      var ddT = ddGO.transform;
      ddGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ddI = ddGO.AddComponent<Image>();
      ddI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
      ddI.type = Image.Type.Sliced;
      var ddSGOP = ddGO.AddComponent<DropdownSGOP>();

      var ttbGO = new GameObject("Tooltip Button", typeof(RectTransform));
      ttbGO.transform.SetParent(ddT);
      var ttbT = ttbGO.transform;
      ttbGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ttbSGOP = ttbGO.AddComponent<ButtonSGOP>();
      var ttbRT = ttbGO.GetComponent<RectTransform>();
      ttbRT.anchorMin = new Vector2(1, 1);
      ttbRT.anchorMax = new Vector2(1, 1);
      ttbRT.pivot = new Vector2(1, 0);
      ttbRT.anchoredPosition3D = new Vector3(-10, 0);
      ttbRT.sizeDelta = new Vector2(50, 50);

      var ttbbGO = new GameObject("Background", typeof(RectTransform));
      ttbbGO.transform.SetParent(ttbT);
      ttbbGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ttbbI = ttbbGO.AddComponent<Image>();
      ttbbI.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/SGOP/Extra Plugins/IngameDebugConsole/Sprites/Unused/IconInfoHighRes.psd");
      var ttbbRT = ttbbGO.transform as RectTransform;
      ttbbRT.anchorMin = Vector2.zero;
      ttbbRT.anchorMax = Vector2.one;
      ttbbRT.offsetMin = Vector2.zero;
      ttbbRT.offsetMax = Vector2.zero;

      var ttbiGO = new GameObject("Tooltip Background", typeof(RectTransform));
      ttbiGO.transform.SetParent(ttbT);
      ttbiGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ttbI = ttbiGO.AddComponent<Image>();
      ttbI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
      ttbI.type = Image.Type.Sliced;
      var ttbiRT = ttbiGO.transform as RectTransform;
      ttbiRT.anchorMin = new Vector2(1, 1);
      ttbiRT.anchorMax = new Vector2(1, 1);
      ttbiRT.pivot = new Vector2(0, 0);
      ttbiRT.sizeDelta = new Vector2(300, 150);
      ttbiRT.anchoredPosition3D = Vector2.zero;

      var ttlGO = new GameObject("Tooltip Label");
      ttlGO.transform.SetParent(ttbiGO.transform);
      ttlGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ttlTx = ttlGO.AddComponent<TextMeshProUGUI>();
      ttlTx.text = "Tooltip";
      ttlTx.color = color;
      ttlTx.fontSize = 42;
      ttlTx.alignment = TextAlignmentOptions.Center;
      var ttlRT = ttlGO.transform as RectTransform;
      ttlRT.anchorMin = Vector2.zero;
      ttlRT.anchorMax = Vector2.one;
      ttlRT.offsetMin = new Vector2(10, 5);
      ttlRT.offsetMax = new Vector2(-10, -5);

      ttbiGO.SetActive(false);
      ttbSGOP.targetGraphic = ttbbI;

      var tlGO = new GameObject("Title Label");
      tlGO.transform.SetParent(ddT);
      tlGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var tlTx = tlGO.AddComponent<TextMeshProUGUI>();
      tlTx.text = "Title";
      tlTx.color = color;
      tlTx.fontSize = 42;
      tlTx.alignment = TextAlignmentOptions.MidlineLeft;
      var tlRT = tlGO.transform as RectTransform;
      tlRT.anchorMin = new Vector2(0, 1);
      tlRT.anchorMax = Vector2.one;
      tlRT.pivot = Vector2.zero;
      tlRT.offsetMin = new Vector2(10, 0);
      tlRT.offsetMax = new Vector2(-60, 50);

      var lGO = new GameObject("Label");
      lGO.transform.SetParent(ddT);
      lGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var lTx = lGO.AddComponent<TextMeshProUGUI>();
      lTx.color = color;
      lTx.fontSize = 42;
      lTx.alignment = TextAlignmentOptions.MidlineLeft;
      var lRT = lGO.transform as RectTransform;
      lRT.anchorMin = Vector2.zero;
      lRT.anchorMax = Vector2.one;
      lRT.offsetMin = new Vector2(10, 5);
      lRT.offsetMax = new Vector2(-60, -5);

      var aGO = new GameObject("Arrow");
      aGO.transform.SetParent(ddT);
      aGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var aI = aGO.AddComponent<Image>();
      aI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/DropdownArrow.psd");
      aI.type = Image.Type.Sliced;
      var aRT = aGO.transform as RectTransform;
      aRT.anchorMin = new Vector2(1, .5f);
      aRT.anchorMax = new Vector2(1, .5f);
      aRT.pivot = new Vector2(1, .5f);
      aRT.anchoredPosition = new Vector2(-10, 0);
      aRT.sizeDelta = new Vector2(50, 50);

      // var vGO = new GameObject("Validation", typeof(RectTransform));
      // vGO.transform.SetParent(ddT);
      // vGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      // var vI = vGO.AddComponent<Image>();
      // // vI.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Packages/com.unity.collab-proxy/Editor/PlasticSCM/Assets/Images/d_stepok@2x.png");
      // var vRT = vGO.transform as RectTransform;
      // vRT.anchorMin = new Vector2(1, .5f);
      // vRT.anchorMax = new Vector2(1, .5f);
      // vRT.pivot = new Vector2(0, .5f);
      // vRT.anchoredPosition3D = new Vector3(5, 0);
      // vRT.sizeDelta = new Vector2(10, 10);
      // vGO.SetActive(false);

      var tmpGO = new GameObject("Template");
      tmpGO.transform.SetParent(ddT);
      tmpGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var tmpI = tmpGO.AddComponent<Image>();
      tmpI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
      tmpI.type = Image.Type.Sliced;
      var tmpSR = tmpGO.AddComponent<ScrollRect>();
      var tmpRT = tmpGO.transform as RectTransform;
      tmpRT.anchorMin = Vector2.zero;
      tmpRT.anchorMax = new Vector2(1, 0);
      tmpRT.pivot = new Vector2(.5f, 1);
      tmpRT.anchoredPosition = new Vector2(0, 2);
      tmpRT.sizeDelta = new Vector2(0, 300);

      var vpGO = new GameObject("Viewport");
      vpGO.transform.SetParent(tmpRT);
      vpGO.AddComponent<Mask>().showMaskGraphic = false;
      vpGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var vpI = vpGO.AddComponent<Image>();
      vpI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
      vpI.type = Image.Type.Sliced;
      var vpRT = vpGO.transform as RectTransform;
      vpRT.anchorMin = Vector2.zero;
      vpRT.anchorMax = Vector2.one;
      vpRT.pivot = new Vector2(0, 1);
      vpRT.offsetMin = Vector2.zero;
      vpRT.offsetMax = new Vector2(-25, 0);

      var cGO = new GameObject("Content", typeof(RectTransform));
      cGO.transform.SetParent(vpRT);
      var cRT = cGO.transform as RectTransform;
      cRT.anchorMin = new Vector2(0, 1);
      cRT.anchorMax = Vector2.one;
      cRT.pivot = new Vector2(.5f, 1);
      cRT.offsetMin = Vector2.zero;
      cRT.offsetMax = Vector2.zero;
      cRT.sizeDelta = new Vector2(0, 50);

      var iGO = new GameObject("Item", typeof(RectTransform));
      iGO.transform.SetParent(cRT);
      var iTgg = iGO.AddComponent<Toggle>();
      var iRT = iGO.transform as RectTransform;
      iRT.anchorMin = new Vector2(0, .5f);
      iRT.anchorMax = new Vector2(1, .5f);
      iRT.pivot = new Vector2(.5f, .5f);
      iRT.offsetMin = Vector2.zero;
      iRT.offsetMax = Vector2.zero;
      iRT.sizeDelta = new Vector2(0, 60);

      var ibGO = new GameObject("Item Background", typeof(RectTransform));
      ibGO.transform.SetParent(iRT);
      ibGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ibI = ibGO.AddComponent<Image>();
      var ibRT = ibGO.transform as RectTransform;
      ibRT.anchorMin = Vector2.zero;
      ibRT.anchorMax = Vector2.one;
      ibRT.pivot = new Vector2(.5f, .5f);
      ibRT.offsetMin = Vector2.zero;
      ibRT.offsetMax = Vector2.zero;

      var icGO = new GameObject("Item Checkmark", typeof(RectTransform));
      icGO.transform.SetParent(iRT);
      icGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var icI = icGO.AddComponent<Image>();
      icI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Checkmark.psd");
      var icRT = icGO.transform as RectTransform;
      icRT.anchorMin = new Vector2(0, .5f);
      icRT.anchorMax = new Vector2(0, .5f);
      icRT.pivot = new Vector2(0, .5f);
      icRT.anchoredPosition = new Vector2(0, 0);
      icRT.sizeDelta = new Vector2(30, 30);

      var ilGO = new GameObject("Item Label", typeof(RectTransform));
      ilGO.transform.SetParent(iRT);
      ilGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ilTx = ilGO.AddComponent<TextMeshProUGUI>();
      ilTx.color = color;
      ilTx.fontSize = 40;
      ilTx.alignment = TextAlignmentOptions.MidlineLeft;
      var ilRT = ilGO.transform as RectTransform;
      ilRT.anchorMin = Vector2.zero;
      ilRT.anchorMax = Vector2.one;
      ilRT.pivot = new Vector2(.5f, .5f);
      ilRT.offsetMin = new Vector2(30, 5);
      ilRT.offsetMax = new Vector2(-10, -5);

      iTgg.targetGraphic = ibI;
      iTgg.graphic = icI;
      iTgg.isOn = true;

      var sbGO = new GameObject("Scrollbar");
      sbGO.transform.SetParent(tmpRT);
      sbGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var sbI = sbGO.AddComponent<Image>();
      sbI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
      sbI.type = Image.Type.Sliced;
      var sbSB = sbGO.AddComponent<Scrollbar>();
      var sbRT = sbGO.transform as RectTransform;
      sbRT.anchorMin = new Vector2(1, 0);
      sbRT.anchorMax = Vector2.one;
      sbRT.pivot = Vector2.one;
      sbRT.offsetMin = Vector2.zero;
      sbRT.offsetMax = Vector2.zero;
      sbRT.sizeDelta = new Vector2(20, 0);

      var saGO = new GameObject("Sliding Area", typeof(RectTransform));
      saGO.transform.SetParent(sbRT);
      var saRT = saGO.transform as RectTransform;
      saRT.anchorMin = Vector2.zero;
      saRT.anchorMax = Vector2.one;
      saRT.pivot = new Vector2(.5f, .5f);
      saRT.offsetMin = new Vector2(10, 10);
      saRT.offsetMax = new Vector2(-10, -10);

      var hGO = new GameObject("Handle");
      hGO.transform.SetParent(saRT);
      hGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var hI = hGO.AddComponent<Image>();
      hI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
      hI.type = Image.Type.Sliced;
      var hRT = hGO.transform as RectTransform;
      hRT.anchorMin = Vector2.zero;
      hRT.anchorMax = new Vector2(.2f, 1);
      hRT.pivot = new Vector2(.5f, .5f);
      hRT.offsetMin = new Vector2(-10, -10);
      hRT.offsetMax = new Vector2(10, 10);

      sbSB.targetGraphic = hI;
      sbSB.handleRect = hRT;
      sbSB.direction = Scrollbar.Direction.BottomToTop;

      tmpSR.horizontal = false;
      tmpSR.movementType = ScrollRect.MovementType.Clamped;
      tmpSR.viewport = vpGO.transform as RectTransform;
      tmpSR.content = cRT;
      tmpSR.verticalScrollbar = sbSB;
      tmpSR.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
      tmpSR.verticalScrollbarSpacing = -3;
      tmpGO.SetActive(false);

      ddSGOP.template = tmpRT;
      ddSGOP.captionText = lTx;
      ddSGOP.arrowImage = aI;
      ddSGOP.templateImage = tmpI;
      ddSGOP.itemText = ilTx;
      ddSGOP.itemImage = ibI;
      ddSGOP.itemCheckmark = icI;
      ddSGOP.title = tlTx;
      ddSGOP.tooltip = ttlTx;

      var optionList = new List<string>() { "Option A", "Option B", "Option C" };
      ddSGOP.AddOptions(optionList);

      ddGO.transform.SetParent(Selection.activeTransform);

      var ddRT = ddGO.GetComponent<RectTransform>();
      ddRT.anchoredPosition3D = Vector3.zero;
      ddRT.localScale = Vector3.one;
      ddRT.sizeDelta = new Vector2(480, 90);
    }

    [MenuItem("GameObject/UI/Input Field (SGOP)", false, -1)]
    static void CreateInputFieldSGOP()
    {
      ColorUtility.TryParseHtmlString("#323232", out var color);
      var phColor = color;
      phColor.a = .5f;

      var ifGO = new GameObject("InputField SGOP", typeof(RectTransform));
      var ifT = ifGO.transform;
      ifGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ifI = ifGO.AddComponent<Image>();
      ifI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/InputFieldBackground.psd");
      ifI.type = Image.Type.Sliced;
      var ifSGOP = ifGO.AddComponent<InputFieldSGOP>();

      var ttbGO = new GameObject("Tooltip Button", typeof(RectTransform));
      ttbGO.transform.SetParent(ifT);
      var ttbT = ttbGO.transform;
      ttbGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ttbSGOP = ttbGO.AddComponent<ButtonSGOP>();
      var ttbRT = ttbGO.GetComponent<RectTransform>();
      ttbRT.anchoredPosition3D = new Vector3(0, 8.5f);
      ttbRT.anchorMin = new Vector2(0, 1);
      ttbRT.anchorMax = new Vector2(0, 1);
      ttbRT.pivot = new Vector2(0, 0.5f);
      ttbRT.sizeDelta = new Vector2(10, 10);

      var ttbbGO = new GameObject("Background", typeof(RectTransform));
      ttbbGO.transform.SetParent(ttbT);
      ttbbGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ttbbI = ttbbGO.AddComponent<Image>();
      ttbbI.sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/SGOP/Extra Plugins/IngameDebugConsole/Sprites/Unused/IconInfoHighRes.psd");
      var ttbbRT = ttbbGO.transform as RectTransform;
      ttbbRT.anchorMin = Vector2.zero;
      ttbbRT.anchorMax = Vector2.one;
      ttbbRT.offsetMin = Vector2.zero;
      ttbbRT.offsetMax = Vector2.zero;

      var ttbiGO = new GameObject("Tooltip Background", typeof(RectTransform));
      ttbiGO.transform.SetParent(ttbT);
      ttbiGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ttbI = ttbiGO.AddComponent<Image>();
      ttbI.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
      ttbI.type = Image.Type.Sliced;
      var ttbiRT = ttbiGO.transform as RectTransform;
      ttbiRT.anchorMin = new Vector2(0, 1);
      ttbiRT.anchorMax = new Vector2(0, 1);
      ttbiRT.pivot = new Vector2(1, 0);
      ttbiRT.sizeDelta = new Vector2(100, 50);
      ttbiRT.anchoredPosition3D = Vector2.zero;

      var ttlGO = new GameObject("Tooltip Label");
      ttlGO.transform.SetParent(ttbiGO.transform);
      ttlGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var ttlTx = ttlGO.AddComponent<TextMeshProUGUI>();
      ttlTx.text = "Tooltip";
      ttlTx.color = color;
      ttlTx.fontSize = 42;
      ttlTx.alignment = TextAlignmentOptions.Center;
      var ttlRT = ttlGO.transform as RectTransform;
      ttlRT.anchorMin = Vector2.zero;
      ttlRT.anchorMax = Vector2.one;
      ttlRT.offsetMin = new Vector2(2, 1);
      ttlRT.offsetMax = new Vector2(-2, -1);

      ttbiGO.SetActive(false);
      ttbSGOP.targetGraphic = ttbbI;
      ttbSGOP.onClick.AddListener(() => { ttbiGO.SetActive(!ttbiGO.activeSelf); });

      var tlGO = new GameObject("Title Label");
      tlGO.transform.SetParent(ifT);
      tlGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var tlTx = tlGO.AddComponent<TextMeshProUGUI>();
      tlTx.text = "Title";
      tlTx.color = color;
      tlTx.fontSize = 42;
      tlTx.alignment = TextAlignmentOptions.MidlineLeft;
      var tlRT = tlGO.transform as RectTransform;
      tlRT.anchorMin = new Vector2(0, 1);
      tlRT.anchorMax = Vector2.one;
      tlRT.pivot = Vector2.zero;
      tlRT.offsetMin = new Vector2(10, 0);
      tlRT.offsetMax = new Vector2(-10, 17);

      var taGO = new GameObject("Text Area", typeof(RectTransform));
      taGO.transform.SetParent(ifT);
      var taT = taGO.transform;
      var taRM = taGO.AddComponent<RectMask2D>();
      taRM.padding = new Vector4(-8, -8, -5, -5);
      var taRT = taGO.GetComponent<RectTransform>();
      taRT.anchorMin = Vector2.zero;
      taRT.anchorMax = Vector2.one;
      taRT.offsetMin = new Vector2(10, 6);
      taRT.offsetMax = new Vector2(-10, -7);

      var phGO = new GameObject("Placeholder");
      phGO.transform.SetParent(taT);
      phGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var phTx = phGO.AddComponent<TextMeshProUGUI>();
      phTx.text = "Enter text...";
      phTx.fontSize = 42;
      phTx.color = phColor;
      phTx.fontStyle = FontStyles.Italic;
      phTx.enableWordWrapping = false;
      phGO.AddComponent<LayoutElement>().ignoreLayout = true;
      var phRT = phGO.GetComponent<RectTransform>();
      phRT.anchorMin = Vector2.zero;
      phRT.anchorMax = Vector2.one;
      phRT.offsetMin = Vector2.zero;
      phRT.offsetMax = Vector2.zero;

      var tGO = new GameObject("Text");
      tGO.transform.SetParent(taT);
      tGO.AddComponent<CanvasRenderer>().cullTransparentMesh = true;
      var tTx = tGO.AddComponent<TextMeshProUGUI>();
      tTx.fontSize = 42;
      tTx.color = color;
      var tRT = tGO.GetComponent<RectTransform>();
      tRT.anchorMin = Vector2.zero;
      tRT.anchorMax = Vector2.one;
      tRT.offsetMin = Vector2.zero;
      tRT.offsetMax = Vector2.zero;

      ifSGOP.title = tlTx;
      ifSGOP.tooltip = ttlTx;
      ifSGOP.targetGraphic = ifI;
      ifSGOP.textViewport = taRT;
      ifSGOP.textComponent = tTx;
      ifSGOP.placeholder = phTx;
      ifSGOP.fontAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset");

      ifGO.transform.SetParent(Selection.activeTransform);

      var ifRT = ifGO.GetComponent<RectTransform>();
      ifRT.anchoredPosition3D = Vector3.zero;
      ifRT.localScale = Vector3.one;
      ifRT.sizeDelta = new Vector2(160, 30);
    }
  }
}

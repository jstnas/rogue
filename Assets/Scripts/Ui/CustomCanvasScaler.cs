using System;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class CustomCanvasScaler : CanvasScaler
    {
        private const float KLogBase = 2;
        private Canvas _mRootCanvas;

        protected override void OnEnable()
        {
            _mRootCanvas = GetComponent<Canvas>();
            base.OnEnable();
        }

        protected override void HandleScaleWithScreenSize()
        {
            var worldCamera = _mRootCanvas.worldCamera;
            var screenSize = worldCamera != null
                ? new Vector2(worldCamera.pixelWidth, worldCamera.pixelHeight)
                : new Vector2(Screen.width, Screen.height);
            // Multiple display support only when not the main display. For display 0 the reported
            // resolution is always the desktops resolution since its part of the display API,
            // so we use the standard none multiple display method. (case 741751)
            var displayIndex = _mRootCanvas.targetDisplay;
            if (displayIndex > 0 && displayIndex < Display.displays.Length)
            {
                var display = Display.displays[displayIndex];
                screenSize = new Vector2(display.renderingWidth, display.renderingHeight);
            }

            float newScaleFactor;
            switch (m_ScreenMatchMode)
            {
                case ScreenMatchMode.MatchWidthOrHeight:
                {
                    // We take the log of the relative width and height before taking the average.
                    // Then we transform it back in the original space.
                    // the reason to transform in and out of logarithmic space is to have better behavior.
                    // If one axis has twice resolution and the other has half,
                    // it should even out if widthOrHeight value is at 0.5.
                    // In normal space the average would be (0.5 + 2) / 2 = 1.25
                    // In logarithmic space the average is (-1 + 1) / 2 = 0
                    var logWidth = Mathf.Log(screenSize.x / m_ReferenceResolution.x, KLogBase);
                    var logHeight = Mathf.Log(screenSize.y / m_ReferenceResolution.y, KLogBase);
                    var logWeightedAverage = Mathf.Lerp(logWidth, logHeight, m_MatchWidthOrHeight);
                    newScaleFactor = Mathf.Pow(KLogBase, logWeightedAverage);
                    break;
                }
                case ScreenMatchMode.Expand:
                {
                    newScaleFactor = Mathf.Min(screenSize.x / m_ReferenceResolution.x,
                        screenSize.y / m_ReferenceResolution.y);
                    break;
                }
                case ScreenMatchMode.Shrink:
                {
                    newScaleFactor = Mathf.Max(screenSize.x / m_ReferenceResolution.x,
                        screenSize.y / m_ReferenceResolution.y);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            SetScaleFactor(newScaleFactor);
            SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit);
        }
    }
}
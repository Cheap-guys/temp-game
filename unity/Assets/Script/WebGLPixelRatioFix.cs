using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLPixelRatioFix : MonoBehaviour
{
    #if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern float GetDevicePixelRatio();
    #endif

    void Start()
    {
        #if UNITY_WEBGL && !UNITY_EDITOR
        float dpr = GetDevicePixelRatio();
        Debug.Log($"Device Pixel Ratio: {dpr}");

        // 🛠 Unity 해상도를 devicePixelRatio 기준으로 조정
        int newWidth = Mathf.RoundToInt(Screen.width * dpr);
        int newHeight = Mathf.RoundToInt(Screen.height * dpr);
        Screen.SetResolution(newWidth, newHeight, FullScreenMode.Windowed);

        Debug.Log($"Updated Unity Resolution: {newWidth} x {newHeight}");
        #endif
    }
}

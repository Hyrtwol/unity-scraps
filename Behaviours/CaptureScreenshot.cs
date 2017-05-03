using System.IO;
using UnityEngine;

public class CaptureScreenshot : MonoBehaviour
{
    // http://docs.unity3d.com/ScriptReference/Application.CaptureScreenshot.html
    // http://answers.unity3d.com/questions/22954/how-to-save-a-picture-take-screenshot-from-a-camer.html

    public KeyCode ScreenshotKeyCode = KeyCode.F11;
    public int ScreenshotWidth = 1280;
    public int ScreenshotHeight = 720;
    public bool LogToDebug = false;

    private bool _captureScreenshot = false;

    public void Start()
    {
        if (LogToDebug) Debug.Log("Press " + ScreenshotKeyCode + " to capture a screenshot");
    }

    public void TakeScreenshot()
    {
        _captureScreenshot = true;
    }

    public void LateUpdate() 
    {
        if (Application.isEditor)
        {
            _captureScreenshot |= Input.GetKeyDown(ScreenshotKeyCode);
            if (_captureScreenshot)
            {
                _captureScreenshot = false;
                var filename = GetFilename();
				//todo auto detect color space
                CaptureScreenshotViaRenderTexture(filename);
                if (LogToDebug) Debug.Log(string.Format("Captured screenshot to: {0}", filename));
            }
        }
    }

    private static string GetFilename()
    {
        string screenshotPath;
        if (Application.isEditor)
        {
            screenshotPath = Path.GetFullPath(Application.dataPath + "/..");
        }
        else
        {
            screenshotPath = Application.persistentDataPath;
        }
        screenshotPath += "/Screenshots";
        Directory.CreateDirectory(screenshotPath);
        var filename = string.Format("{0}/screen_{1}.png",
            screenshotPath,
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        return filename;
    }

    private void CaptureScreenshotViaRenderTexture(string filename)
    {
        var cam = GetComponent<Camera>();
        var org = cam.targetTexture;
        var rt = new RenderTexture(ScreenshotWidth, ScreenshotHeight, 24);
        cam.targetTexture = rt;
        var screenShot = new Texture2D(ScreenshotWidth, ScreenshotHeight, TextureFormat.RGB24, false);
        cam.Render();
        //var org = RenderTexture.active;
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, ScreenshotWidth, ScreenshotHeight), 0, 0);
        RenderTexture.active = null;
        cam.targetTexture = org;
        Destroy(rt);
        var bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(filename, bytes);
    }

#if NOT_WORKING_IN_LINAR_COLOR_MODE
    private static void CaptureScreenshotViaApplication(string filename)
    {
        Application.CaptureScreenshot(filename);
    }
#endif
}

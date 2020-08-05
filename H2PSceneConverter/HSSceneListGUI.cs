// Decompiled with JetBrains decompiler
// Type: H2PConverter.HSSceneListGUI
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace H2PConverter
{
  internal class HSSceneListGUI : MonoBehaviour
  {
    private GameObject newCanvas = new GameObject("Canvas");
    private int holizontalSize = Screen.width / 330;
    private Dictionary<string, Texture> listTexture = new Dictionary<string, Texture>();
    private int windowID = 8755;
    private Rect windowRect = new Rect(0.0f, 0.0f, 500f, 450f);
    private string windowTitle = "Load(PH)";
    private Vector2 vscrollLight = new Vector2(0.0f, 0.0f);
    private Image blockImage;
    private string[] list;
    private const int panelWidth = 500;
    private const int panelHeight = 450;
    private bool initialized;
    public bool showGUI;

    private void OnLevelWasLoaded(int level)
    {
    }

    public void Init()
    {
      this.newCanvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
      this.newCanvas.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
      this.newCanvas.AddComponent<GraphicRaycaster>();
      GameObject gameObject = new GameObject("Panel");
      gameObject.AddComponent<CanvasRenderer>();
      this.blockImage = gameObject.AddComponent<Image>();
      this.blockImage.color = Color.gray;
      this.blockImage.transform.localScale = new Vector3(200f, 200f, 200f);
      gameObject.transform.SetParent(this.newCanvas.transform, false);
      this.blockImage.gameObject.SetActive(false);
      this.list = Directory.GetFiles(UserData.Create("studioHS"), "*.png");
    }

    private void OnGUI()
    {
      this.blockImage.gameObject.SetActive(this.showGUI);
      if (!this.showGUI)
        return;
      Singleton<Studio.Studio>.get_Instance().gameObject.transform.Find("Canvas Main Menu/01_Add");
      GUIStyle windowStyle = HSSceneListGUI.GetWindowStyle();
      if (!this.initialized)
      {
        this.windowRect = new Rect(0.0f, 0.0f, (float) Screen.width, (float) Screen.height);
        this.initialized = true;
      }
      this.windowRect = GUI.ModalWindow(this.windowID, this.windowRect, new GUI.WindowFunction(this.FuncWindowGUI), this.windowTitle, windowStyle);
    }

    private void FuncWindowGUI(int winID)
    {
      GUI.enabled = true;
      GUILayout.BeginVertical();
      if (GUILayout.Button("close", GUILayout.Width((float) Screen.width), GUILayout.Height(80f)))
        this.showGUI = false;
      this.vscrollLight = GUILayout.BeginScrollView(this.vscrollLight, false, false, GUILayout.MaxHeight((float) Screen.height));
      int num = 0;
      GUILayout.BeginHorizontal();
      foreach (string index in this.list)
      {
        if (num % this.holizontalSize == 0)
        {
          GUILayout.EndHorizontal();
          GUILayout.BeginHorizontal();
        }
        Texture image;
        if (!this.listTexture.ContainsKey(index))
        {
          image = this.ReadTexture(index, 320, 180);
          this.listTexture[index] = image;
        }
        else
          image = this.listTexture[index];
        if (GUILayout.Button(image, GUILayout.Width(320f), GUILayout.Height(180f)))
        {
          try
          {
            Converter.convert(index, UserData.Create("studioHS"));
            Singleton<Studio.Studio>.get_Instance().LoadScene(UserData.Create("studioHS") + "\\h2p.png");
          }
          catch (Exception ex)
          {
            HSSceneListGUI.logSave(ex.ToString());
          }
          this.showGUI = false;
          File.Delete(UserData.Create("studioHS") + "\\h2p.png");
        }
        ++num;
      }
      GUILayout.EndHorizontal();
      GUILayout.EndScrollView();
      GUILayout.EndVertical();
    }

    public static GUIStyle GetWindowStyle()
    {
      return new GUIStyle(GUI.skin.window);
    }

    private byte[] ReadPngFile(string path)
    {
      BinaryReader binaryReader = new BinaryReader((Stream) new FileStream(path, FileMode.Open, FileAccess.Read));
      byte[] numArray = binaryReader.ReadBytes((int) binaryReader.BaseStream.Length);
      binaryReader.Close();
      return numArray;
    }

    private Texture ReadTexture(string path, int width, int height)
    {
      byte[] numArray = this.ReadPngFile(path);
      Texture2D texture2D = new Texture2D(width, height);
      texture2D.LoadImage(numArray);
      return (Texture) texture2D;
    }

    public static void logSave(string txt)
    {
    }
  }
}

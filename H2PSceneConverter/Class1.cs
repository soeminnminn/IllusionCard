// Decompiled with JetBrains decompiler
// Type: H2PConverter.Class1
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using IllusionPlugin;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace H2PConverter
{
  public class Class1 : IEnhancedPlugin, IPlugin
  {
    private HSSceneListGUI gui;
    private Studio.Studio studio;

    public string[] Filter
    {
      get
      {
        return new string[2]
        {
          "PlayHomeStudio32bit",
          "PlayHomeStudio64bit"
        };
      }
    }

    public string Name
    {
      get
      {
        return "H2PSceneConverter";
      }
    }

    public string Version
    {
      get
      {
        return "1.0.1.0";
      }
    }

    public void OnApplicationQuit()
    {
    }

    public void OnFixedUpdate()
    {
    }

    public void OnLateUpdate()
    {
    }

    public void OnApplicationStart()
    {
    }

    public void OnLevelWasInitialized(int level)
    {
      Studio.Studio instance = Singleton<Studio.Studio>.get_Instance();
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      Transform transform1 = instance.gameObject.transform.Find("Canvas Main Menu/04_System/Viewport/Content");
      Class1.logSave("a");
      if (!((UnityEngine.Object) transform1.Find("HSSceneListGUI") == (UnityEngine.Object) null))
        return;
      Class1.logSave("b");
      if (!((UnityEngine.Object) transform1.Find("End") != (UnityEngine.Object) null))
        return;
      Class1.logSave("c");
      Transform transform2 = transform1.Find("End");
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(transform2.gameObject);
      gameObject.name = "HSSceneListGUI";
      Text component1 = gameObject.transform.Find("Text").GetComponent<Text>();
      component1.text = "Load(HS)";
      component1.SetLayoutDirty();
      gameObject.transform.SetParent(transform2.transform.parent);
      Button component2 = gameObject.GetComponent<Button>();
      component2.onClick = new Button.ButtonClickedEvent();
      component2.onClick.AddListener(new UnityAction(this.OnButtonClick));
      gameObject.transform.localPosition = transform2.transform.localPosition - new Vector3(0.0f, 60f, 0.0f);
      gameObject.transform.localScale = Vector3.one;
    }

    public void OnLevelWasLoaded(int level)
    {
      try
      {
        if ((UnityEngine.Object) this.studio == (UnityEngine.Object) null)
          this.studio = Singleton<Studio.Studio>.get_Instance();
        if ((UnityEngine.Object) this.studio == (UnityEngine.Object) null || !((UnityEngine.Object) this.gui == (UnityEngine.Object) null))
          return;
        Class1.logSave("1");
        this.gui = new GameObject("HSSceneListGUI").AddComponent<HSSceneListGUI>();
        this.gui.Init();
        this.gui.gameObject.SetActive(true);
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
      }
    }

    private void OnButtonClick()
    {
      this.gui.showGUI = !this.gui.showGUI;
    }

    public void OnUpdate()
    {
    }

    public static void logSave(string txt)
    {
    }
  }
}

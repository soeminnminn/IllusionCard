// Decompiled with JetBrains decompiler
// Type: SaveAssist.AssetBundleAssist
// Assembly: H2PSceneConverter, Version=1.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 1583ABF6-A697-4C49-B1E4-14D2C05BEFFF
// Assembly location: D:\Games\Illusion\PlayHome\Plugins\H2PSceneConverter.dll

using System.IO;

namespace SaveAssist
{
  public abstract class AssetBundleAssist
  {
    protected string savePath = string.Empty;
    protected string assetBundleName = string.Empty;
    protected string assetName = string.Empty;

    public AssetBundleAssist(string _savePath, string _assetBundleName, string _assetName)
    {
      this.savePath = _savePath;
      this.assetBundleName = _assetBundleName;
      this.assetName = _assetName;
    }

    public void Save()
    {
      using (FileStream fileStream = new FileStream(this.savePath, FileMode.Create, FileAccess.Write))
      {
        using (BinaryWriter bw = new BinaryWriter((Stream) fileStream))
          this.SaveFunc(bw);
      }
    }

    public abstract void SaveFunc(BinaryWriter bw);

    public abstract void LoadFunc(BinaryReader br);
  }
}

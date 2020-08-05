using System;
using System.IO;

namespace SexyBeachPR
{
    public class ColorSet
    {
        public HsvColor diffuseColor = new HsvColor(20f, 0.8f, 0.8f);
        public float alpha = 1f;
        public HsvColor specularColor = new HsvColor(0.0f, 0.0f, 0.8f);
        public float specularIntensity = 0.5f;
        public float specularSharpness = 3f;

        public void Copy(ColorSet src)
        {
            this.diffuseColor = new HsvColor(src.diffuseColor.H, src.diffuseColor.S, src.diffuseColor.V);
            this.alpha = src.alpha;
            this.specularColor = new HsvColor(src.specularColor.H, src.specularColor.S, src.specularColor.V);
            this.specularIntensity = src.specularIntensity;
            this.specularSharpness = src.specularSharpness;
        }

        public void Save(BinaryWriter writer)
        {
            writer.Write((double)this.diffuseColor.H);
            writer.Write((double)this.diffuseColor.S);
            writer.Write((double)this.diffuseColor.V);
            writer.Write((double)this.alpha);
            writer.Write((double)this.specularColor.H);
            writer.Write((double)this.specularColor.S);
            writer.Write((double)this.specularColor.V);
            writer.Write((double)this.specularIntensity);
            writer.Write((double)this.specularSharpness);
        }

        public void Load(BinaryReader reader, int version)
        {
            this.diffuseColor.H = (float)reader.ReadDouble();
            this.diffuseColor.S = (float)reader.ReadDouble();
            this.diffuseColor.V = (float)reader.ReadDouble();
            this.alpha = (float)reader.ReadDouble();
            this.specularColor.H = (float)reader.ReadDouble();
            this.specularColor.S = (float)reader.ReadDouble();
            this.specularColor.V = (float)reader.ReadDouble();
            this.specularIntensity = (float)reader.ReadDouble();
            this.specularSharpness = (float)reader.ReadDouble();
        }
    }
}

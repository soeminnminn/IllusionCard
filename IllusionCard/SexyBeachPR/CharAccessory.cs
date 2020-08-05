using System;
using System.IO;
using UnityEngine;

namespace SexyBeachPR
{
    [Serializable]
    public class CharAccessory
    {
        public int accessoryType = -1;
        public int accessoryId = -1;
        public string parentKey = string.Empty;
        public Vector3 plusPos = Vector3.zero;
        public Vector3 plusRot = Vector3.zero;
        public Vector3 plusScl = Vector3.one;

        public void Save(BinaryWriter writer)
        {
            writer.Write(this.accessoryType);
            writer.Write(this.accessoryId);
            writer.Write(this.parentKey);
            writer.Write((double)this.plusPos.x);
            writer.Write((double)this.plusPos.y);
            writer.Write((double)this.plusPos.z);
            writer.Write((double)this.plusRot.x);
            writer.Write((double)this.plusRot.y);
            writer.Write((double)this.plusRot.z);
            writer.Write((double)this.plusScl.x);
            writer.Write((double)this.plusScl.y);
            writer.Write((double)this.plusScl.z);
        }

        public void Load(BinaryReader reader)
        {
            this.accessoryType = reader.ReadInt32();
            this.accessoryId = reader.ReadInt32();
            this.parentKey = reader.ReadString();
            this.plusPos.x = (float)reader.ReadDouble();
            this.plusPos.y = (float)reader.ReadDouble();
            this.plusPos.z = (float)reader.ReadDouble();
            this.plusRot.x = (float)reader.ReadDouble();
            this.plusRot.y = (float)reader.ReadDouble();
            this.plusRot.z = (float)reader.ReadDouble();
            this.plusScl.x = (float)reader.ReadDouble();
            this.plusScl.y = (float)reader.ReadDouble();
            this.plusScl.z = (float)reader.ReadDouble();
        }
    }
}

using System;
using UnityEngine;

namespace SexyBeachPR
{
    [Serializable]
    public class CharCustom
    {
        public int personality = 14;
        public string name = string.Empty;
        public ColorSet skinColor = new ColorSet();
        public ColorSet eyeshadowColor = new ColorSet();
        public ColorSet cheekColor = new ColorSet();
        public ColorSet lipColor = new ColorSet();
        public ColorSet tattoo_fColor = new ColorSet();
        public ColorSet moleColor = new ColorSet();
        public ColorSet eyebrowColor = new ColorSet();
        public ColorSet eyelashesColor = new ColorSet();
        public ColorSet eyeLColor = new ColorSet();
        public ColorSet eyeRColor = new ColorSet();
        public ColorSet eyeHiColor = new ColorSet();
        public ColorSet eyeWColor = new ColorSet();
        public ColorSet beardColor = new ColorSet();
        public float faceDetailWeight = 0.5f;
        public ColorSet sunburnColor = new ColorSet();
        public ColorSet tattoo_bColor = new ColorSet();
        public ColorSet nipColor = new ColorSet();
        public ColorSet underhairColor = new ColorSet();
        public ColorSet nailColor = new ColorSet();
        public readonly int ShapeFaceNum;
        public readonly int ShapeBodyNum;
        protected CharBody bodyBase;
        protected CharFemaleBody femaleBody;
        protected CharMaleBody maleBody;
        public int headId;
        public int texFaceId;
        public int texEyeshadowId;
        public int texCheekId;
        public int texLipId;
        public int texTattoo_fId;
        public int texMoleId;
        public int matEyebrowId;
        public int matEyelashesId;
        public int matEyeLId;
        public int matEyeRId;
        public int matEyeHiId;
        public int beardId;
        public int texBodyId;
        public int texSunburnId;
        public int texTattoo_bId;
        public int matNipId;
        public int matUnderHairId;
        public float bodyDetailWeight;
        public float[] shapeFace;
        public float[] shapeBody;

        public bool customMode { get; private set; }

        public CharCustom(CharBody cha)
        {
            this.bodyBase = cha;
            if (cha.Sex == 0)
            {
                this.ShapeFaceNum = CharDefine.MaleShepeFaceNum;
                this.ShapeBodyNum = CharDefine.MaleShepeBodyNum;
                this.maleBody = cha as CharMale;
            }
            else
            {
                this.ShapeFaceNum = CharDefine.FemaleShepeFaceNum;
                this.ShapeBodyNum = CharDefine.FemaleShepeBodyNum;
                this.femaleBody = cha as CharFemale;
            }
            this.shapeFace = new float[this.ShapeFaceNum];
            for (int index = 0; index < this.shapeFace.Length; ++index)
                this.shapeFace[index] = 0.5f;
            this.shapeBody = new float[this.ShapeBodyNum];
            for (int index = 0; index < this.shapeBody.Length; ++index)
                this.shapeBody[index] = 0.5f;
            this.customMode = false;
            this.texFaceId = 0;
            this.skinColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.skinColor.specularColor = HsvColor.FromRgb(Color.white);
            this.texEyeshadowId = 0;
            this.eyeshadowColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.texCheekId = 0;
            this.cheekColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.texLipId = 0;
            this.lipColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.texTattoo_fId = 0;
            this.tattoo_fColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.texMoleId = 0;
            this.moleColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.texBodyId = 0;
            this.texSunburnId = 0;
            this.sunburnColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.texTattoo_bId = 0;
            this.tattoo_bColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.matEyebrowId = 0;
            this.eyebrowColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.eyebrowColor.diffuseColor.S = 1f;
            this.matEyelashesId = 0;
            this.eyelashesColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.eyelashesColor.diffuseColor.S = 1f;
            this.matEyeLId = 0;
            this.eyeLColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.eyeLColor.diffuseColor.S = 1f;
            this.matEyeRId = 0;
            this.eyeRColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.eyeRColor.diffuseColor.S = 1f;
            this.matEyeHiId = 0;
            this.eyeHiColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.eyeWColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.beardId = 0;
            this.beardColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.beardColor.diffuseColor.S = 1f;
            this.faceDetailWeight = 0.5f;
            this.matNipId = 0;
            this.nipColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.nipColor.diffuseColor.S = 1f;
            this.matUnderHairId = 0;
            this.underhairColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.underhairColor.diffuseColor.S = 1f;
            this.nailColor.diffuseColor = HsvColor.FromRgb(Color.white);
            this.nailColor.diffuseColor.S = 1f;
            this.bodyDetailWeight = 0.0f;
        }

        public float GetShapeBodyValue(int index)
        {
            return index >= this.shapeBody.Length ? 0.0f : this.shapeBody[index];
        }

        public virtual byte[] SaveBytes()
        {
            return null;
        }

        public virtual bool LoadBytes(byte[] data, int version)
        {
            return true;
        }

        public enum FaceSkinParts
        {
            face,
            eyeshadow,
            cheek,
            lip,
            tattoo_f,
            mole,
        }

        public enum BodySkinParts
        {
            body,
            sunburn,
            tattoo_b,
        }
    }
}

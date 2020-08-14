using System;
using MessagePack;
using UnityEngine;

namespace CharacterKK
{
    [MessagePackObject(true)]
    public class ChaFileParameter
    {
        [IgnoreMember]
        public static readonly string BlockName = "Parameter";

        public ChaFileParameter()
        {
            this.MemberInit();
        }

        public Version version { get; set; }

        public byte sex { get; set; }

        public int exType { get; set; }

        public string lastname { get; set; }

        public string firstname { get; set; }

        [IgnoreMember]
        public string fullname
        {
            get
            {
                return this.lastname + " " + this.firstname;
            }
        }

        public string nickname { get; set; }

        public int callType { get; set; }

        public int personality { get; set; }

        public byte bloodType { get; set; }

        public byte birthMonth { get; set; }

        public byte birthDay { get; set; }

        [IgnoreMember]
        public string strBirthDay
        {
            get
            {
                return this.birthMonth.ToString() + "月" + this.birthDay.ToString() + "日";
            }
        }

        public byte clubActivities { get; set; }

        public float voiceRate { get; set; }

        [IgnoreMember]
        public float voicePitch
        {
            get
            {
                return Mathf.Lerp(0.94f, 1.06f, this.voiceRate);
            }
        }

        public int weakPoint { get; set; }

        public Awnser awnser { get; set; }

        public Denial denial { get; set; }

        public Attribute attribute { get; set; }

        public int aggressive { get; set; }

        public int diligence { get; set; }

        public int kindness { get; set; }

        public void MemberInit()
        {
            this.version = ChaFileDefine.ChaFileParameterVersion;
            this.sex = 0;
            this.exType = 0;
            this.lastname = string.Empty;
            this.firstname = string.Empty;
            this.nickname = string.Empty;
            this.callType = -1;
            this.personality = 0;
            this.bloodType = 0;
            this.birthMonth = 1;
            this.birthDay = 1;
            this.clubActivities = 0;
            this.voiceRate = 0.5f;
            this.awnser = new Awnser();
            this.awnser.MemberInit();
            this.weakPoint = -1;
            this.denial = new Denial();
            this.denial.MemberInit();
            this.attribute = new Attribute();
            this.attribute.MemberInit();
            this.aggressive = 0;
            this.diligence = 0;
            this.kindness = 0;
        }

        public void Copy(ChaFileParameter src)
        {
            this.version = src.version;
            this.sex = src.sex;
            this.exType = src.exType;
            this.lastname = src.lastname;
            this.firstname = src.firstname;
            this.nickname = src.nickname;
            this.callType = src.callType;
            this.personality = src.personality;
            this.bloodType = src.bloodType;
            this.birthMonth = src.birthMonth;
            this.birthDay = src.birthDay;
            this.clubActivities = src.clubActivities;
            this.voiceRate = src.voiceRate;
            this.awnser.Copy(src.awnser);
            this.weakPoint = src.weakPoint;
            this.denial.Copy(src.denial);
            this.attribute.Copy(src.attribute);
            this.aggressive = src.aggressive;
            this.diligence = src.diligence;
            this.kindness = src.kindness;
        }

        public void ComplementWithVersion()
        {
            this.callType = -1;
            if (this.version.CompareTo(new Version("0.0.1")) == -1)
            {
                this.awnser = new Awnser();
                this.awnser.MemberInit();
            }
            if (this.version.CompareTo(new Version("0.0.2")) == -1)
            {
                this.denial = new Denial();
                this.denial.MemberInit();
            }
            if (this.version.CompareTo(new Version("0.0.3")) == -1)
            {
                this.attribute = new Attribute();
                this.attribute.MemberInit();
            }
            if (this.version.CompareTo(new Version("0.0.4")) == -1)
                this.voiceRate = 0.5f;
            this.version = ChaFileDefine.ChaFileParameterVersion;
        }

        [MessagePackObject(true)]
        public class Awnser
        {
            public Awnser()
            {
                this.MemberInit();
            }

            public bool animal { get; set; }

            public bool eat { get; set; }

            public bool cook { get; set; }

            public bool exercise { get; set; }

            public bool study { get; set; }

            public bool fashionable { get; set; }

            public bool blackCoffee { get; set; }

            public bool spicy { get; set; }

            public bool sweet { get; set; }

            public void MemberInit()
            {
                this.animal = false;
                this.eat = false;
                this.cook = false;
                this.exercise = false;
                this.study = false;
                this.fashionable = false;
                this.blackCoffee = false;
                this.spicy = false;
                this.sweet = false;
            }

            public void Copy(Awnser src)
            {
                this.animal = src.animal;
                this.eat = src.eat;
                this.cook = src.cook;
                this.exercise = src.exercise;
                this.study = src.study;
                this.fashionable = src.fashionable;
                this.blackCoffee = src.blackCoffee;
                this.spicy = src.spicy;
                this.sweet = src.sweet;
            }
        }

        [MessagePackObject(true)]
        public class Denial
        {
            public Denial()
            {
                this.MemberInit();
            }

            public bool kiss { get; set; }

            public bool aibu { get; set; }

            public bool anal { get; set; }

            public bool massage { get; set; }

            public bool notCondom { get; set; }

            public void MemberInit()
            {
                this.kiss = false;
                this.aibu = false;
                this.anal = false;
                this.massage = false;
                this.notCondom = false;
            }

            public void Copy(Denial src)
            {
                this.kiss = src.kiss;
                this.aibu = src.aibu;
                this.anal = src.anal;
                this.massage = src.massage;
                this.notCondom = src.notCondom;
            }
        }

        [MessagePackObject(true)]
        public class Attribute
        {
            public Attribute()
            {
                this.MemberInit();
            }

            public bool hinnyo { get; set; }

            public bool harapeko { get; set; }

            public bool donkan { get; set; }

            public bool choroi { get; set; }

            public bool bitch { get; set; }

            public bool mutturi { get; set; }

            public bool dokusyo { get; set; }

            public bool ongaku { get; set; }

            public bool kappatu { get; set; }

            public bool ukemi { get; set; }

            public bool friendly { get; set; }

            public bool kireizuki { get; set; }

            public bool taida { get; set; }

            public bool sinsyutu { get; set; }

            public bool hitori { get; set; }

            public bool undo { get; set; }

            public bool majime { get; set; }

            public bool likeGirls { get; set; }

            public void MemberInit()
            {
                this.hinnyo = false;
                this.harapeko = false;
                this.donkan = false;
                this.choroi = false;
                this.bitch = false;
                this.mutturi = false;
                this.dokusyo = false;
                this.ongaku = false;
                this.kappatu = false;
                this.ukemi = false;
                this.friendly = false;
                this.kireizuki = false;
                this.taida = false;
                this.sinsyutu = false;
                this.hitori = false;
                this.undo = false;
                this.majime = false;
                this.likeGirls = false;
            }

            public void Copy(Attribute src)
            {
                this.hinnyo = src.hinnyo;
                this.harapeko = src.harapeko;
                this.donkan = src.donkan;
                this.choroi = src.choroi;
                this.bitch = src.bitch;
                this.mutturi = src.mutturi;
                this.dokusyo = src.dokusyo;
                this.ongaku = src.ongaku;
                this.kappatu = src.kappatu;
                this.ukemi = src.ukemi;
                this.friendly = src.friendly;
                this.kireizuki = src.kireizuki;
                this.taida = src.taida;
                this.sinsyutu = src.sinsyutu;
                this.hitori = src.hitori;
                this.undo = src.undo;
                this.majime = src.majime;
                this.likeGirls = src.likeGirls;
            }
        }
    }
}

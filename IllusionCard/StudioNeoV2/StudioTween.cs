using System;
using UnityEngine;

namespace StudioNeoV2
{
    public class StudioTween
    {
        public enum EaseType
        {
            easeInQuad,
            easeOutQuad,
            easeInOutQuad,
            easeInCubic,
            easeOutCubic,
            easeInOutCubic,
            easeInQuart,
            easeOutQuart,
            easeInOutQuart,
            easeInQuint,
            easeOutQuint,
            easeInOutQuint,
            easeInSine,
            easeOutSine,
            easeInOutSine,
            easeInExpo,
            easeOutExpo,
            easeInOutExpo,
            easeInCirc,
            easeOutCirc,
            easeInOutCirc,
            linear,
            spring,
            easeInBounce,
            easeOutBounce,
            easeInOutBounce,
            easeInBack,
            easeOutBack,
            easeInOutBack,
            easeInElastic,
            easeOutElastic,
            easeInOutElastic,
        }

        public enum LoopType
        {
            none,
            loop,
            pingPong,
        }

        public enum NamedValueColor
        {
            _Color,
            _SpecColor,
            _Emission,
            _ReflectColor,
        }

        public static class Defaults
        {
            public static float time = 1f;
            public static float delay = 0.0f;

            public static NamedValueColor namedColorValue = NamedValueColor._Color;
            public static LoopType loopType = LoopType.none;
            public static EaseType easeType = EaseType.easeOutExpo;
            public static float lookSpeed = 3f;
            public static bool isLocal = false;
            // public static Space space = Space.Self;
            public static bool orientToPath = false;
            public static Color color = Color.white;
            public static float updateTimePercentage = 0.05f;
            public static float updateTime = 1f * updateTimePercentage;
            public static int cameraFadeDepth = 999999;
            public static float lookAhead = 0.05f;
            public static bool useRealTime = false;
            public static Vector3 up = Vector3.up;
        }
    }
}

using UnityEngine;

namespace Script
{
    public static class AnimTriggerHash
    {
        public static readonly int Appear = Animator.StringToHash("appear");
        public static readonly int Disappear = Animator.StringToHash("disappear");
        public static readonly int Switch = Animator.StringToHash("switch");
        public static readonly int Idle = Animator.StringToHash("idle");
        public static readonly int Walk = Animator.StringToHash("walk");
        public static readonly int Light = Animator.StringToHash("light");
        public static readonly int Float = Animator.StringToHash("float");
        public static readonly int Stay = Animator.StringToHash("stay");
    }
}
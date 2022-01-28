using UnityEngine;

namespace Script.UI.MiniGame
{
    public class IPTag : MonoBehaviour
    {
        // 可被玩家拾起
        private int _kind;

        public void SetKind(int kind)
        {
            _kind = kind;
        }
    }
}
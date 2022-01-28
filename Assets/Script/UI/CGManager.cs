using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public enum CGs
    {
        None = 0,
        WorkPlace = 1,
        WorkPlace_on =2,
    }
    
    public class CGManager : MonoBehaviour
    {
        public Sprite[] cgs;
        
        private CGs _curCG;
        private Image _image;
        private Image _cover;
        private Image _back;
        private Animator _animator;

        public static CGManager Instance { get; private set; }

        private void Start()
        {
            Instance = this;
            _curCG = CGs.None;
            _image = transform.Find("image").GetComponent<Image>();
            _cover = transform.Find("cover").GetComponent<Image>();
            _back = transform.Find("back").GetComponent<Image>();
            _animator = GetComponent<Animator>();
        }
        
        public void Show(CGs cg, Color color)
        {
            // CG入场（淡入）
            _cover.color = color;
            _curCG = cg;
            _animator.SetTrigger(AnimTriggerHash.Appear);
        }

        public void lighton(CGs cg, Color color)
        {
            _back.color = color;
            _animator.SetTrigger(AnimTriggerHash.Light);
        }

        public void Hide(Color color)
        {
            // CG退场（淡出）
            _cover.color = color;
            _animator.SetTrigger(AnimTriggerHash.Disappear);
        }

        public void SwitchTo(CGs cg, Color color)
        {
            // 替换CG图片（淡入）
            if (_curCG == cg) return;
            _cover.color = color;
            _curCG = cg;
            _animator.SetTrigger(AnimTriggerHash.Switch);
        }
        
        // 由动画器调用
        private void SwitchCG()
        {
            _image.sprite = cgs[(int)_curCG];
        }

        private void CGEventComplete()
        {
            PlotManager.Instance.ToNextEvent();
        }
    }
}
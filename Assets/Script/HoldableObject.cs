using Script.Checker;
using Script.Icon;
using Script.UI.MiniGame;
using UnityEngine;

namespace Script
{
    public class HoldableObject : MonoBehaviour
    {
        public bool executable;
        public bool foldable;
        
        [Space]
        public float poolStep;
        public string fileName;
        public string fileSize;

        private Rigidbody _rgBody;
        private Joint _joint;
        private SpriteRenderer _image;
        private bool _isSelected;
        private bool _isHolding;
        private bool _isInPool;
        private Animator _animator;

        private static readonly int LineWidth = Shader.PropertyToID("_lineWidth");

        private void Start()
        {
            _image = transform.Find("image").GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            _rgBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // 最后再invert if
            if (_isSelected)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    var s = UIManager.Instance.player.GetComponent<HoldableObjChecker>();
                    if (!_isHolding)
                    {
                        if (!s.isHolding)
                        {
                            // 拿起icon
                            transform.position = s.holdingPos.position;
                            transform.SetParent(s.holdingPos);
                            _isHolding = true;
                            s.isHolding = true;
                            s.blockingIcon = null;
                            _animator.SetTrigger(AnimTriggerHash.Stay);

                            if (_isInPool)
                            {
                                _isInPool = false;
                                _rgBody.constraints = RigidbodyConstraints.FreezeRotation;
                                MemPool.Instance.RemoveIcon(this);
                                switch (name)
                                {
                                    case "IPScanner":
                                        GetComponent<IPScanner>().Kill();
                                        break;
                                }
                            }

                            _joint = gameObject.AddComponent<FixedJoint>();
                            ((FixedJoint)_joint).connectedBody = UIManager.Instance.playerRgBody;
                            InteractiveBtn.Instance.Disappear();
                            
                            // 显示file info
                            FileInfoUI.Instance.ShowInfo(fileName, fileSize);
                        }
                    }
                    else
                    {
                        if (s.blockingIcon == null)
                        {
                            // 放下icon
                            if (!s.nearPool)
                            {
                                var pos = UIManager.Instance.player.transform.position;
                                pos.y -= 0.1f;
                                pos.z -= 0.3f;
                                transform.position = pos;
                            }
                            else
                            {
                                if (!MemPool.Instance.AddIcon(this))
                                {
                                    // 内存不足
                                    WarningMsg.Instance.ShowMsg("Memory overflow warning");
                                    return;
                                }

                                if (!executable)
                                {
                                    WarningMsg.Instance.ShowMsg("Operation not understood");
                                }
                                else
                                {
                                    switch (name)
                                    {
                                        case "IPScanner":
                                            GetComponent<IPScanner>().Execute();
                                            break;
                                    }
                                }
                                
                                _animator.SetTrigger(AnimTriggerHash.Float);
                                _isInPool = true;
                                _rgBody.constraints = RigidbodyConstraints.FreezeAll;
                            }

                            if (foldable)
                            {
                                transform.SetParent(UIManager.Instance.activeWindow == ActiveWindow.Folder 
                                    ? UIManager.Instance.folderUI.icons
                                    : UIManager.Instance.iconHome.transform);
                            }
                            else
                            {
                                transform.SetParent(UIManager.Instance.iconHome.transform);
                            }
                            
                            _isHolding = false;
                            s.isHolding = false;
                            s.blockingIcon = this;
                        
                            Destroy(_joint);
                        
                            var sc = GetComponent<InteractiveIcon>();
                            if (sc != null)
                            {
                                if (!InteractiveBtn.Instance.isInUse) InteractiveBtn.Instance.Appear(sc);
                            }
                            
                            // 隐藏file info
                            FileInfoUI.Instance.HideInfo();
                        }
                        else WarningMsg.Instance.ShowMsg("Some icon is blocking");
                    }
                }
                    
            }
        }

        public void Select()
        {
            _image.material.SetFloat(LineWidth, 8f);  // 描边
            _isSelected = true;
        }
        
        public void Release()
        {
            _image.material.SetFloat(LineWidth, 0f);
            _isSelected = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            // Debug.Log($"player entered {name}");
        }

        private void OnTriggerExit(Collider other)
        {
            // Debug.Log($"player leaved {name}");
        }
    
    }
}

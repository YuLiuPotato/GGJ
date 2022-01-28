using System;
using System.Collections.Generic;
using Script.Checker;
using Script.Icon;
using UnityEngine;

namespace Script
{
    public class MemPool : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private List<HoldableObject> _icons;
        
        public Transform surfaceTransform;
        public float capacity;
        public float surfaceMoveSpeed;

        private Vector3 posOri;
        private bool _isSurfaceMove;
        private float _targetSurfaceHeight;
        private float _maxHeight;
        
        public static MemPool Instance { get; private set; }
        
        private static readonly int LineWidth = Shader.PropertyToID("_lineWidth");

        private void Start()
        {
            Instance = this;
            _spriteRenderer = transform.Find("image").GetComponent<SpriteRenderer>();
            posOri = surfaceTransform.position;
            _targetSurfaceHeight = posOri.z;
            _maxHeight = posOri.z + capacity;
            _icons = new List<HoldableObject>();
        }

        public bool AddIcon(HoldableObject icon)
        {
            var target = _targetSurfaceHeight + icon.poolStep;
            if (target > _maxHeight) return false;
            _icons.Add(icon);
            _targetSurfaceHeight = target;
            _isSurfaceMove = true;
            return true;
        }
        
        public void RemoveIcon(HoldableObject icon)
        {
            _icons.Remove(icon);
            _targetSurfaceHeight -= icon.poolStep;
            _isSurfaceMove = true;
        }

        private void Update()
        {
            // 最后invert if
            if (_isSurfaceMove)
            {
                var p = surfaceTransform.position;
                if (p.z < _targetSurfaceHeight)
                {
                    p.z += Time.deltaTime * surfaceMoveSpeed;
                    if (p.z > _targetSurfaceHeight)
                    {
                        p.z = _targetSurfaceHeight;
                        _isSurfaceMove = false;
                    }
                }
                else if (p.z > _targetSurfaceHeight)
                {
                    p.z -= Time.deltaTime * surfaceMoveSpeed;
                    if (p.z < _targetSurfaceHeight)
                    {
                        p.z = _targetSurfaceHeight;
                        _isSurfaceMove = false;
                    }
                }
                surfaceTransform.position = new Vector3(p.x, p.y, p.z);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            _spriteRenderer.material.SetFloat(LineWidth, 8f);    // 描边
            var s = UIManager.Instance.player.GetComponent<HoldableObjChecker>();
            s.nearPool = true;

        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            _spriteRenderer.material.SetFloat(LineWidth, 0f);
            var s = UIManager.Instance.player.GetComponent<HoldableObjChecker>();
            s.nearPool = false;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class FileInfoUI : MonoBehaviour
    {
        private Text _info;
        
        public static FileInfoUI Instance { get; private set; }

        private void Start()
        {
            Instance = this;
            _info = transform.Find("Text").GetComponent<Text>();
        }

        public void ShowInfo(string fileName, string fileSize)
        {
            _info.text = "name: " + fileName + "\n" + "size: " + fileSize;
            _info.enabled = true;
        }
        
        public void HideInfo()
        {
            _info.enabled = false;
        }
        
    }
}
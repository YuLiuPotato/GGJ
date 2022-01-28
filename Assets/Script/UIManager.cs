using Script.UI;
using Script.UI.Dialogue;
using UnityEngine;

namespace Script
{
    public enum ActiveWindow
    {
        None = 0,
        Folder = 1,
        Email = 2,
        TrashBin = 3,
        Log = 4,
    }
    
    public class UIManager : MonoBehaviour
    {
        public GameObject player;
        public GameObject iconHome;
        public Rigidbody playerRgBody;

        public ActiveWindow activeWindow;

        [Space] [Space] public FolderUI folderUI;
        [Space] [Space] public EmailUI emailUI;
        [Space] [Space] public GameObject SettingButtonUI;
        public static UIManager Instance { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            Instance = this;
            activeWindow = ActiveWindow.None;
            playerRgBody = player.GetComponent<Rigidbody>();
            
            folderUI.gameObject.SetActive(true);
            folderUI.Init();
            emailUI.gameObject.SetActive(true);
            emailUI.Init();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OpenFolderUI()
        {
            if (activeWindow != ActiveWindow.None)
            {
                WarningMsg.Instance.ShowMsg("Multiple windows is not supported");
                return;
            }
            
            folderUI.gameObject.SetActive(true);
            folderUI.Appear();
            activeWindow = ActiveWindow.Folder;
        }

        public void CloseFolderUI()
        {
            activeWindow = ActiveWindow.None;
            folderUI.gameObject.SetActive(false);
        }
        
        public void OpenEmailUI()
        {
            if (activeWindow != ActiveWindow.None)
            {
                WarningMsg.Instance.ShowMsg("Multiple windows is not supported");
                return;
            }
            
            emailUI.gameObject.SetActive(true);
            emailUI.AppearEmail();
            activeWindow = ActiveWindow.Email;
        }

        public void CloseEmailUI()
        {
            activeWindow = ActiveWindow.None;
            emailUI.gameObject.SetActive(false);
        }

        public void SettingButtonClickExit()
        {
            SettingButtonUI.SetActive(true);
            PlotManager.Instance.ToNextEvent();
        }
    }
}

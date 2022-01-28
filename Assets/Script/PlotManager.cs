using Script.UI;
using Script.UI.Dialogue;
using UnityEngine;

namespace Script
{
    public class PlotManager : MonoBehaviour
    {
        private int _day;  // 当前天数（一天有多个事件）
        private int _eventIndex;  // 当前事件数（一天内的第几个事件）
        private bool _toNextEvent;  // 是否触发下一个事件，用于Update函数
        
        public static PlotManager Instance { get; private set; }
        
        private void Start()
        {
            Instance = this;
            _day = 1;
            _eventIndex = 0;
            _toNextEvent = false;
        }
        
        // void startGame()
        // {
        //     
        // }
        
        public void ToNextEvent()
        {
            // 供其他类调用。当完成一个事件时，调用此函数以开启下一个事件
            _toNextEvent = true;
        }
        
        private void Update()
        {
            if (!_toNextEvent) return;
            _toNextEvent = false;
            
            switch (_day)
            {
                case 1: StartDay1Event(); break;
                case 2: StartDay2Event(); break;
                case 3: StartDay3Event(); break;
            }

            // if (GameObject.Find("UI_Worldspace").activeSelf == true)
            // {
            //     ChatManager.ScrollTheBar(ChatBar,chatboxdistance);
            //     ChatManager.UpdateSpawnPosition(ChatBar);
            // }
            
            
        }

        private void StartDay1Event()
        {
            switch (_eventIndex)
            {
                case 0: CGManager.Instance.Show(CGs.WorkPlace, Color.black); break;
                case 1: DialogueBox.Instance.StartDialogue(Dialogues.Introduction); break;
                case 2: CGManager.Instance.lighton(CGs.WorkPlace_on,Color.black); break;
                case 3: CGManager.Instance.Hide(Color.white); break;
                case 4: UIManager.Instance.SettingButtonClickExit(); break;
                case 5:
                    ;
                    break;
                default:
                    Debug.Log("> Not implemented yet!");
                    break;
            }
            _eventIndex++;
        }
        
        private void StartDay2Event()
        {
            switch (_eventIndex)
            {
                
            }
            _eventIndex++;
        }
        
        private void StartDay3Event()
        {
            switch (_eventIndex)
            {
                
            }
            _eventIndex++;
        }
        
    }
}

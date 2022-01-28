using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Dialogue
{
    public class DialogueBox : MonoBehaviour
    {
        public bool isDisplaying;
        public float textCharShownInterval = 0.2f;  // 字符间的显示时间间隔， 单位：s
        public Text dialogueTextObj;  // 对话Text对象
        public Text speakerTextObj;  // 讲话人Text对象
        public Color[] txtColors;  // 字母颜色

        private Dialogues _curDialogueIndex;  // 当前对话段的角标
        private int _curSegmentIndex;  // 当前对话角标
        private float _nextCharAppearTime;  // 显示下一个字的时间
        private string _curText = "";  // 当前对话的所有字符
        private int _curTextLength;  // 当前对话字符总数
        private int _curDialogueLength;  // 当前对话局数总数
        private bool _isCurDialogueEnd;  // 当前对话是否显示玩了
        private int _curCharIndex;  // 当前显示字符的角标
        
        public static DialogueBox Instance { get; private set; }
        
        // private Animator _animator;
        
        public void StartDialogue(Dialogues dialogueIndex)
        {
            // 开始一场对话
            _curDialogueIndex = dialogueIndex;
            _curSegmentIndex = -1;
            var info = DialogueManager.GetDialogue(dialogueIndex, 0);
            _curDialogueLength = DialogueManager.GetDialogueLength(dialogueIndex);
            speakerTextObj.text = info[0];
            // _animator.SetTrigger(AnimTriggerHash.Appear);
            isDisplaying = true;
            ToNextDialogue();
        }

        private void ToNextDialogue()
        {
            // 当前对话播完，获取下一条对话
            _curSegmentIndex++;
            if (_curSegmentIndex == _curDialogueLength)
            {
                EndDialogue();  // 对话播完，结束该对话
                return;
            }
            var info = DialogueManager.GetDialogue(_curDialogueIndex, _curSegmentIndex);
            speakerTextObj.text = info[0];
            _curText = info[1];
            dialogueTextObj.text = "";
            dialogueTextObj.color = info[2] is null ? txtColors[0] : txtColors[int.Parse(info[2])];
            _curCharIndex = 0;
            _curTextLength = _curText.Length;
            _isCurDialogueEnd = false;
            _nextCharAppearTime = Time.time + textCharShownInterval;
        }

        private void EndDialogue()
        {
            // 结束该对话，关闭对话框，待完善----------------------------------------------------------
            // _animator.SetTrigger(AnimTriggerHash.Disappear);
            isDisplaying = false;
            PlotManager.Instance.ToNextEvent();
        }

        private void Start()
        {
            Instance = this;
            // _animator = GetComponent<Animator>();
            // gameObject.SetActive(false);  // 初始化后active设为false
            // StartDialogue(0);
        }

        private void QuickEndCurDialogue()
        {
            // 对话字符未显示完时瞬间显示所有对话字符
            dialogueTextObj.text = _curText;
            _isCurDialogueEnd = true;
        }

        private void Update()
        {
            if (!isDisplaying) return;
            if (!_isCurDialogueEnd)
            {
                if (Time.time > _nextCharAppearTime)
                {
                    // _displayingText里增加下一个字符
                    dialogueTextObj.text += _curText[_curCharIndex++];
                    if (_curCharIndex == _curTextLength)
                    {
                        // 当前对话全部显示
                        _isCurDialogueEnd = true;
                        // Debug.Log("current dialogue shown");
                    }
                    else _nextCharAppearTime = Time.time + textCharShownInterval;  // 计算显示下个字符的时间
                }
            }
            
            // 回车键或鼠标左键或设定好的下句对话的按键（默认空格）事件
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                if (_isCurDialogueEnd) ToNextDialogue();
                else QuickEndCurDialogue();  // 对话字符未显示完时瞬间显示所有对话字符
            }
        }

        // // ==================== 由动画器调用 ======================
        // private void SetInactive()
        // {
        //     gameObject.SetActive(false);
        // }
        //
        // private void FetchFirstDialogue()
        // {
        //     FetchNextDialogue();
        //     isDisplaying = true;
        // }
    }
}
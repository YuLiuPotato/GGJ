using System.Collections.Generic;
using UnityEngine;

namespace Script.UI.Dialogue
{
    public enum Dialogues
    {
        Introduction = 0,
    }

    public static class DialogueManager
    {
        private static readonly List<List<string[]>> Dialogues = new List<List<string[]>>
        {
            // new List<string[]>  // 一段对话
            // {
            //     new [] {"speaker0", "It's time for work!"},  // 一句对话 {speaker, text content, color}, length为3
            //     new [] {"speaker1", "I don't want to work."},
            // }
            new List<string[]>  // Introduction
            {
                new [] {"", "     ", null},  // 空对话（不能为empty string，否则bug），展示cg
                new [] {"", "9:00 pm", null},
                new [] {"", "In Bicent IT Company", null},
                new [] {"???", "(Someone )", "1"},
                new [] {"Programmer", "Night work time", null},
                new [] {"Programmer", "I don't want to work adasopdasodapsoasdadasdasdasdasdasadsada -testing.", "2"},
                new [] {"System", "省略剧情", "1"},
                new [] {"", "< WASD to move (this is tip) >", "3"},
                new [] {"", "< C to pick up or put down icons >", "3"},
                new [] {"", "< E to interactive >", "3"},
            },
            new List<string[]>
            {
                new [] {"speaker0", "It's time for work!"},
                new [] {"speaker1", "I don't want to work."},
            },
        };

        public static string[] GetDialogue(Dialogues dialogue, int segmentIndex)
        {
            // 载入对话到ui
            return Dialogues[(int)dialogue][segmentIndex];
        }
        
        public static int GetDialogueLength(Dialogues dialogue)
        {
            return Dialogues[(int)dialogue].Count;
        }
        
    }
}
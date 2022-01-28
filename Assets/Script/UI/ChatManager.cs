using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

namespace Script.UI
{
    
    public class ChatManager : MonoBehaviour
    {
        // Start is called before the first frame update
        //public TextMeshProUGUI ;
        private GameObject[] Chatboxpool;
        
        [SerializeField]
        private Sprite[] ChatSprite;

        [SerializeField] private GameObject ChatBar;
        [SerializeField] private GameObject chatUiPrefab;
        [SerializeField] private GameObject Parent;

        private float chatboxdistance;
        private int checkboxshowingrange;
        public float upperboundary;
        public float lowerboundary;

        private Vector3 ChatBarOriginalPosition;
        public static ChatManager Instance { get; private set; }
        // read the text in the PlotScript return a string (not tested)
        public string readtext(string path ="Assets/PlotScript/PlotScript.txt")
        {
            ///string path = Application.persistentDataPath + "/test.txt";
            string content;
            //Read the text from directly from the test.txt file
            StreamReader reader = new StreamReader(path);
            content = reader.ReadToEnd();
            reader.Close();
            return content;
        }
//seperate the text and initialize 
        public void sepearte_text(string plot)
        {
            string[] content;
            string[] sentence = plot.Split('\n');
            Chatboxpool = new GameObject[sentence.Length];
            ChatBoxUI temp = null;
            for(int i =0; i < sentence.Length;i++)
            {
                content = sentence[i].Split(':');
                GameObject newObject = Instantiate(chatUiPrefab,Parent.transform) as GameObject;
                
                Chatboxpool[i] = newObject;
                if (String.Compare(content[0], "G", StringComparison.Ordinal)==0)
                {
                    Chatboxpool[i].GetComponent<ChatBoxUI>().initialized(People.github,ChatSprite[0],content[1]);
                }
                else if (String.Compare(content[0], "P", StringComparison.Ordinal) == 0)
                {
                    Chatboxpool[i].GetComponent<ChatBoxUI>().initialized(People.programmer,ChatSprite[1],content[1]);
                }
                else if (String.Compare(content[0], "H", StringComparison.Ordinal)==0)
                {
                    Chatboxpool[i].GetComponent<ChatBoxUI>().initialized(People.Boss,ChatSprite[2],content[1]);
                }
                else if (String.Compare(content[0], "B", StringComparison.Ordinal) == 0)
                {
                    Chatboxpool[i].GetComponent<ChatBoxUI>().initialized(People.Hacker, ChatSprite[3], content[1]);
                }
                else
                {
                    Debug.Log("wrong input of people representation");
                }
                temp = null;
            }
        }
//return the spawn position
        public void UpdateSpawnPosition(GameObject scrollbar)
        {
            for (int i = 0; i < Chatboxpool.Length; i++)
            {
                // var tran0 = scrollbar.transform.GetChild(0).Find("G" + i.ToString()).GetComponent<RectTransform>();
                //var position = GetCenterPosition(tran);
                //Debug.Log("x y z"+position.x+" "+position.y+" "+position.z);
                //Chatboxpool[i].transform.position = new Vector3(position.x, position.y, position.z);
                
                var tran = scrollbar.transform.GetChild(i).GetComponent<RectTransform>();
                // Debug.Log("x y z"+position.x+" "+position.y+" "+position.z);
                Chatboxpool[i].GetComponent<RectTransform>().position = tran.position;
                // Chatboxpool[i].transform.position = UI_Worldspace.transform.position;
            }
        }
        //need to constrain the movement of player
        public void ScrollTheBar(GameObject scrollbar, float distance)
        {
            // Debug.Log(scrollbar);
            // Debug.Log(scrollbar.transform.GetChild(0).position.y);
            if ( checkboxshowingrange>=1 && Input.GetKeyDown(KeyCode.S))
            {
                scrollbar.transform.Translate(Vector3.down*distance);
                checkboxshowingrange--;
            }
            else if (checkboxshowingrange+3< Chatboxpool.Length && Input.GetKeyDown(KeyCode.W))
            {
                scrollbar.transform.Translate(Vector3.up*distance);
                checkboxshowingrange++;
            }
            else
            {
                
            }

            for (int i = 0; i < Chatboxpool.Length; i++)
            {
                if (i < 3 + checkboxshowingrange && i >= checkboxshowingrange)
                {
                    Chatboxpool[i].gameObject.SetActive(true);
                }
                else
                {
                    Chatboxpool[i].gameObject.SetActive(false);
                }
            }

        }
        //dead
        private Vector3 GetverticalDistance(RectTransform rt)
        {
            Vector3[] v = new Vector3[4];
            rt.GetWorldCorners(v);
            return v[1]-v[0];
        }
        
        public void InitializeChatManager()
        {
            
            sepearte_text(readtext());
            UpdateSpawnPosition(ChatBar);
            chatboxdistance =  GetverticalDistance(Chatboxpool[0].GetComponent<RectTransform>()).magnitude;
            ChatBarOriginalPosition = ChatBar.transform.position;
        }

        private void Start()
        {
            sepearte_text(readtext());
            UpdateSpawnPosition(ChatBar);
            chatboxdistance =  GetverticalDistance(Chatboxpool[0].GetComponent<RectTransform>()).magnitude;
            ChatBarOriginalPosition = ChatBar.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            ScrollTheBar(ChatBar,chatboxdistance);
            UpdateSpawnPosition(ChatBar);
        }


    }
}

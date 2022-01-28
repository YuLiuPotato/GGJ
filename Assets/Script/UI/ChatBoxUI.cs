using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{    
    public enum People
    {
        github = 0,
        programmer = 1,
        Boss=2,
        Hacker=3,
        
    }
    
    public class ChatBoxUI : MonoBehaviour
    {
        [SerializeField]
        private Sprite ChatSprite; // this is the sprite list
        [SerializeField] private TextMeshProUGUI ChatText; // the main chat UI

        [SerializeField] private Image LeftImage; // the image UI container for sender

        [SerializeField] private Image RightImage; // the image UI container for getter

        //public static ChatBoxUI Instance { get; private set; }
        
        private People Sender;

        public void initialized(People sender, Sprite chatSprite,string sentence)
        {
            
            //do additional initialization steps here
            LeftImage = transform.Find("Left_sprite").GetComponent<Image>();
            ChatText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            RightImage = transform.Find("Right_sprite").GetComponent<Image>();
            ChatSprite = chatSprite;
            this.Sender = sender;
            changeText(sentence);
            
        }
        public void changeText(string sentence)
        {
            ChatText.text = sentence;
            set_people_sprite(); // print the sender sprite
        }
// set the sprite for the Image in right place, people: the sprite image to show, name:"place to shou"
        private void set_people_sprite()
        {
            Image temp =LeftImage;
            if ( Sender != People.programmer)
            {
                temp = LeftImage;
                Color originalColor = RightImage.color;
                RightImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            }
            else 
            {
                temp = RightImage;
                Color originalColor = LeftImage.color;
                LeftImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
            }
            temp.sprite = ChatSprite;
        }
    }
    
}

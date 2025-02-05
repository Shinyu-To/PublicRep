using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;//単列
    public GameObject dialogueBox;//Dialogue Panel　の　表示か非表示
    public TextMeshProUGUI dialogueText, nameText;
    //public Image dialogueCharacterImage;

    [TextArea(1, 3)]
    public string[] dialogueLines;//会話の内容、会話の内容が一行を超えた場合のための、数グループ
    [SerializeField] private int currentLine;//会話の順

    private bool isScrolling;//会話文章のエフェクト
    [SerializeField] private float textSpeed;//会話文字の現すスピード

    //private PlayerController playerController;
    //private int run;

    //tips panel
    public GameObject tipsBox;
    public TextMeshProUGUI tipsText;


    private void Awake()//単列
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
               Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dialogueText.text = dialogueLines[currentLine];//会話の順に従って表示

        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //run = playerController.Run();
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)//会話パネルが表示されているかどうか
        {

            if (Input.GetButtonUp("Submit") || Input.GetKeyDown(KeyCode.JoystickButton0))//ボダン押せば次の会話へ行く 0 = A
            {

                //OffTips();

                if(isScrolling == false)//
                {
                    currentLine++;

                    if (currentLine < dialogueLines.Length)
                    {
                        CheckName();
                        //dialogueText.text = dialogueLines[currentLine];//会話の順に従って表示
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogueBox.SetActive(false);//会話パネルを隠す
                        //ShowTips();
                    }
                }
            }
        }


    }

    public void ShowDialogue(string[] _newLines, bool _hasName)//それぞれの対象やNPCの会話内容
    {


        dialogueLines = _newLines;//それぞれの会話内容を値をつける
        currentLine = 0;//0から（最初の会話から）

        CheckName();

        //dialogueText.text = dialogueLines[currentLine];//会話の順に従って表示
        StartCoroutine(ScrollingText());//会話の文字一つずつ現すエフェクとの開始

        dialogueBox.SetActive(true);//会話パネルを表示

        //nameText.gameObject.SetActive(true);//hasName　ON　名前がある
        //nameText.gameObject.SetActive(false);//hasName　OFF　名前がない
        nameText.gameObject.SetActive(_hasName);
        //if hasName == true, nameText display.
        //if hasName == false, nameText Hide.
    }

    //tips
    public void ShowTips()
    {
        tipsBox.SetActive(true);
    }

    public void OffTips()
    {
        tipsBox.SetActive(false);
    }


    public void OffDialogue()//会話パネルのOFF
    {
        dialogueBox.SetActive(false);
    }

    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            //n-を検索し、n-＃＃＃を名前テキスト（nameText）につける。Replace(A, B)　BでAを替える
            currentLine++;
        }
    }

 
    private IEnumerator ScrollingText()//会話の文字一つずつ現すエフェクト
    {
        isScrolling = true;
        dialogueText.text = "";//毎回会話の最初は空白から

        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;//文字一つずつ現す
            yield return new WaitForSeconds(textSpeed);
        }

        isScrolling  = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;//�P��
    public GameObject dialogueBox;//Dialogue Panel�@�́@�\������\��
    public TextMeshProUGUI dialogueText, nameText;
    //public Image dialogueCharacterImage;

    [TextArea(1, 3)]
    public string[] dialogueLines;//��b�̓��e�A��b�̓��e����s�𒴂����ꍇ�̂��߂́A���O���[�v
    [SerializeField] private int currentLine;//��b�̏�

    private bool isScrolling;//��b���͂̃G�t�F�N�g
    [SerializeField] private float textSpeed;//��b�����̌����X�s�[�h

    //private PlayerController playerController;
    //private int run;

    //tips panel
    public GameObject tipsBox;
    public TextMeshProUGUI tipsText;


    private void Awake()//�P��
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
        dialogueText.text = dialogueLines[currentLine];//��b�̏��ɏ]���ĕ\��

        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //run = playerController.Run();
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)//��b�p�l�����\������Ă��邩�ǂ���
        {

            if (Input.GetButtonUp("Submit") || Input.GetKeyDown(KeyCode.JoystickButton0))//�{�_�������Ύ��̉�b�֍s�� 0 = A
            {

                //OffTips();

                if(isScrolling == false)//
                {
                    currentLine++;

                    if (currentLine < dialogueLines.Length)
                    {
                        CheckName();
                        //dialogueText.text = dialogueLines[currentLine];//��b�̏��ɏ]���ĕ\��
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogueBox.SetActive(false);//��b�p�l�����B��
                        //ShowTips();
                    }
                }
            }
        }


    }

    public void ShowDialogue(string[] _newLines, bool _hasName)//���ꂼ��̑Ώۂ�NPC�̉�b���e
    {


        dialogueLines = _newLines;//���ꂼ��̉�b���e��l������
        currentLine = 0;//0����i�ŏ��̉�b����j

        CheckName();

        //dialogueText.text = dialogueLines[currentLine];//��b�̏��ɏ]���ĕ\��
        StartCoroutine(ScrollingText());//��b�̕�����������G�t�F�N�Ƃ̊J�n

        dialogueBox.SetActive(true);//��b�p�l����\��

        //nameText.gameObject.SetActive(true);//hasName�@ON�@���O������
        //nameText.gameObject.SetActive(false);//hasName�@OFF�@���O���Ȃ�
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


    public void OffDialogue()//��b�p�l����OFF
    {
        dialogueBox.SetActive(false);
    }

    private void CheckName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            //n-���������An-�������𖼑O�e�L�X�g�inameText�j�ɂ���BReplace(A, B)�@B��A��ւ���
            currentLine++;
        }
    }

 
    private IEnumerator ScrollingText()//��b�̕�����������G�t�F�N�g
    {
        isScrolling = true;
        dialogueText.text = "";//�����b�̍ŏ��͋󔒂���

        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter;//�����������
            yield return new WaitForSeconds(textSpeed);
        }

        isScrolling  = false;
    }

}

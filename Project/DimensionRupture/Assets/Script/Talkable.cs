using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Talkable : MonoBehaviour
{
    [SerializeField] private bool isEntered;
    [SerializeField] private bool hasName;//�m�[�l�[��
    [TextArea(1, 3)]
    public string[] lines;//�Ώۂ��ꂼ��̉�b���e

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//Player��Collider2D�͈͓��ɓ����
        {
            isEntered = true;//�͈͓��ɂ���Ɣ��f
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//Player��Collider2D�͈͓����痣����
        {
            isEntered = false;//�͈͓��ɂ��Ȃ��Ɣ��f
        }
    }

    private void Update()
    {
        if (isEntered && DialogueManager.instance.dialogueBox.activeInHierarchy == false�@&& Input.GetKeyDown(KeyCode.JoystickButton1) || isEntered && DialogueManager.instance.dialogueBox.activeInHierarchy == false && Input.GetKeyDown(KeyCode.T))//1 = B
        {//�v���C���[��Collider2D�͈͓��ɓ���ƁA�{�_����������
            DialogueManager.instance.ShowDialogue(lines, hasName);
            //�X�N���v�gDialogueManager����void ShowDialogue���Ăяo���Ďg�p����
        }

        if(isEntered && DialogueManager.instance.tipsBox.activeInHierarchy == false)
        {
            DialogueManager.instance.ShowTips();
        }
        
        if(isEntered == false && DialogueManager.instance.tipsBox.activeInHierarchy == true)
        {
            DialogueManager.instance.OffTips();
        }

        //if (isEntered == false && DialogueManager.instance.dialogueBox.activeInHierarchy == true)
        //{//�v���C���[��Collider2D�͈͓����痣����
            //DialogueManager.instance.OffDialogue();//��b�p�l�����I�t
        //}
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Talkable : MonoBehaviour
{
    [SerializeField] private bool isEntered;
    [SerializeField] private bool hasName;//ノーネーム
    [TextArea(1, 3)]
    public string[] lines;//対象それぞれの会話内容

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//PlayerがCollider2D範囲内に入ると
        {
            isEntered = true;//範囲内にいると判断
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//PlayerがCollider2D範囲内から離れると
        {
            isEntered = false;//範囲内にいないと判断
        }
    }

    private void Update()
    {
        if (isEntered && DialogueManager.instance.dialogueBox.activeInHierarchy == false　&& Input.GetKeyDown(KeyCode.JoystickButton1) || isEntered && DialogueManager.instance.dialogueBox.activeInHierarchy == false && Input.GetKeyDown(KeyCode.T))//1 = B
        {//プレイヤーがCollider2D範囲内に入ると、ボダンを押せば
            DialogueManager.instance.ShowDialogue(lines, hasName);
            //スクリプトDialogueManager中のvoid ShowDialogueを呼び出して使用する
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
        //{//プレイヤーがCollider2D範囲内から離れると
            //DialogueManager.instance.OffDialogue();//会話パネルがオフ
        //}
        
    }

}

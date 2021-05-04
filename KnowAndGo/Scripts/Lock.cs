using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] Key key;

    bool isCorrectAnswer;
    Animator animator;

    void Start()
    {
        switch (id)
        {
            case 1:
                QuestionManager.OnAnswer1Created += DefineIsCorrect;
                break;
            case 2:
                QuestionManager.OnAnswer2Created += DefineIsCorrect;
                break;
            case 3:
                QuestionManager.OnAnswer3Created += DefineIsCorrect;
                break;
        }

        animator = GetComponent<Animator>();
    }

    void DefineIsCorrect(string answer, bool isCorrect)
    {
        isCorrectAnswer = isCorrect;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        if (player.HasKey(key))
        {
            player.ActivateActionText(true);
        }
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        if (player.HasKey(key))
        {
            if (Input.GetButtonDown("Press"))
            {
                Timer.stop = true;
                StartCoroutine(PressButton(player));
            }
        }
    }

    IEnumerator PressButton(Player player)
    {
        animator.SetTrigger("Open");
        yield return new WaitForSeconds(1);
        player.EndAnimation(isCorrectAnswer);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player == null)
            return;

        if (player.HasKey(key))
        {
            player.ActivateActionText(false);
        }
    }


    void OnDestroy()
    {
        switch (id)
        {
            case 1:
                QuestionManager.OnAnswer1Created -= DefineIsCorrect;
                break;
            case 2:
                QuestionManager.OnAnswer2Created -= DefineIsCorrect;
                break;
            case 3:
                QuestionManager.OnAnswer3Created -= DefineIsCorrect;
                break;
        }
    }
}

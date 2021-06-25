using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowHideInstruction : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] GameObject button;
    [SerializeField] Vector3 posOfButtonWhenShowing = new Vector3(196.7f, 1639.2f,0);
    [SerializeField] Vector3 posOfButtonWhenHidden = new Vector3(196.7f, 2086, 0);
    bool isInstructionShowing = true;
    /// <summary>
    /// If instructions are hidden show them otherwise hide instruction if it is showing.
    /// </summary>
    public void ShowOrHideInstruction()
    {
        if (isInstructionShowing) HideInstruction();
        else ShowInstruction();
    }

    /// <summary>
    /// Show instructions
    /// </summary>
    public void ShowInstruction()
    {
        
        text.SetActive(true);
        RectTransform rt = button.GetComponent<RectTransform>();
        rt.anchoredPosition = posOfButtonWhenShowing;
        button.GetComponentInChildren<Text>().text = "Hide Instructions";
        isInstructionShowing = true;
    }
    /// <summary>
    /// Hide instructions
    /// </summary>
    public void HideInstruction()
    {
  
        text.SetActive(false);
        RectTransform rt = button.GetComponent<RectTransform>();
        rt.anchoredPosition = posOfButtonWhenHidden;
        button.GetComponentInChildren<Text>().text = "Show Instructions";
        isInstructionShowing = false;
    }
}

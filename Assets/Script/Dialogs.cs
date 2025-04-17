using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Dialogs : MonoBehaviour
{
    private Story _currentStory;
    private TextAsset _inkJson;

    private GameObject _dialogPanel;
    private TextMeshProUGUI _dialogText;
    private TextMeshProUGUI _nameCharText;

    private GameObject _choiceButtonPanel;
    private GameObject _choiceButton;
    private List<TextMeshProUGUI> _choicesText = new();
    
    public bool DialogPlay { get; private set; }

    [Inject]
    public void Contract(DialogInstaller dialogInstaller)
    {
        _inkJson = dialogInstaller.inkJson;
        _dialogPanel = dialogInstaller.DialogPanel;
        _dialogText = dialogInstaller.DialogText;
        _nameCharText = dialogInstaller.NameCharText;
        _choiceButtonPanel = dialogInstaller.ChoiceButtonPanel;
        _choiceButton = dialogInstaller.ChoiceButton;
        
    } 
    
    private void Awake()
    {
        _currentStory = new Story(_inkJson.text);
    }

    void Start()
    {
        StartDialog();
    }

    public void ChoiceButtonAction(int choiceIndex)
    {
        _currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueDialog();
    }
    
    public void ContinueDialog()
    {
        if (_currentStory.canContinue)
        {
            ShowDialog();
            ShowChoiceButton();
        }
        else
        {
            ExitDialog();
        }
    }
    
    private void StartDialog()
    {
        DialogPlay = true; 
        _dialogPanel.SetActive(true); 
        ContinueDialog();
    }
    
    private void ShowDialog()
    {
        _dialogText.text = _currentStory.Continue();
        _nameCharText.text = (string)_currentStory.variablesState["CharName"];
    }

    private void ShowChoiceButton()
    {
        List<Choice> currentChoices = _currentStory.currentChoices;
        _choiceButtonPanel.SetActive(currentChoices.Count != 0);

        if (currentChoices.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < currentChoices.Count; i++)
        {
            GameObject choice = Instantiate(_choiceButton);
            choice.GetComponent<ButtonAction>().Index = i;
            choice.transform.SetParent(_choiceButtonPanel.transform);

            TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = currentChoices[i].text;
            _choicesText.Add(choiceText); 
        }
    }

    private void ExitDialog()
    {
        Debug.Log("end");
        DialogPlay = false;
        _dialogPanel.SetActive(false);
    }

}

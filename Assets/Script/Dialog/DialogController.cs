using System.Collections.Generic;

using Ink.Runtime;

using TMPro;

using UnityEngine;

namespace VisualNovelGame
{
    public class DialogController : MonoBehaviour
    {
        private Story _currentStory;
        private TextAsset _inkJson;

        private GameObject _dialogPanel;
        private TextMeshProUGUI _dialogText;
        private TextMeshProUGUI _nameCharText;

        private GameObject _choiceButtonPanel;
        private GameObject _choiceButton;
        private List<TextMeshProUGUI> _choicesText = new List<TextMeshProUGUI>();

        public bool DialogPlay { get; private set; }

        public void Initialize(DialogParameters context)
        {
            _inkJson = context.inkJson;
            _dialogPanel = context.DialogPanel;
            _dialogText = context.DialogText;
            _nameCharText = context.NameCharText;
            _choiceButtonPanel = context.ChoiceButtonPanel;
            _choiceButton = context.ChoiceButton;

            _currentStory = new Story(_inkJson.text);
        }

        private void Start()
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
            _nameCharText.text = (string) _currentStory.variablesState["CharName"];
        }

        private void ShowChoiceButton()
        {
            var currentChoices = _currentStory.currentChoices;
            _choiceButtonPanel.SetActive(currentChoices.Count != 0);

            if (currentChoices.Count <= 0)
            {
                return;
            }

            for (var i = 0; i < currentChoices.Count; i++)
            {
                var choice = Instantiate(_choiceButton);
                choice.GetComponent<ButtonAction>().Index = i;
                choice.transform.SetParent(_choiceButtonPanel.transform);

                var choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
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
}
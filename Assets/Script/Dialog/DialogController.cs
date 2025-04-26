using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;

namespace VisualNovelGame
{
    public class DialogController : MonoBehaviour
    {
        public bool IsDialogPlaying { get; private set; }

        private Story _currentStory;
        private TextAsset _inkJson;

        private GameObject _dialogPanel;
        private TextMeshProUGUI _dialogText;
        private TextMeshProUGUI _nameCharText;

        private GameObject _choiceButtonPanel;
        private ButtonView _choiceButtonPrefab;

        private List<ButtonView> _createdViews = new List<ButtonView>();

        public void Initialize(DialogParameters context)
        {
            _inkJson = context.inkJson;
            _dialogPanel = context.DialogPanel;
            _dialogText = context.DialogText;
            _nameCharText = context.NameCharText;
            _choiceButtonPanel = context.ChoiceButtonPanel;
            _choiceButtonPrefab = context.ChoiceButtonPrefab;

            _currentStory = new Story(_inkJson.text);
        }

        public void Run()
        {
            StartDialog();
        }

        public void Dispose()
        {
            foreach (var view in _createdViews)
            {
                view.ButtonClicked -= ChoiceButtonAction;
            }
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

        private void ChoiceButtonAction(int choiceIndex)
        {
            _currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueDialog();
        }

        private void StartDialog()
        {
            IsDialogPlaying = true;
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
            var currentChoices = _currentStory.currentChoices;
            _choiceButtonPanel.SetActive(currentChoices.Count != 0);

            if (currentChoices.Count <= 0)
            {
                return;
            }

            for (var i = 0; i < currentChoices.Count; i++)
            {
                var choiceButtonView = Instantiate(
                    _choiceButtonPrefab,
                    _choiceButtonPanel.transform
                );

                choiceButtonView.ButtonClicked += ChoiceButtonAction;

                choiceButtonView.SetIndex(i);
                choiceButtonView.SetText(currentChoices[i].text);
                _createdViews.Add(choiceButtonView);
            }
        }

        private void ExitDialog()
        {
            Debug.Log("end");
            IsDialogPlaying = false;
            _dialogPanel.SetActive(false);
        }
    }
}

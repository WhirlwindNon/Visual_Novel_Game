using System;

using UnityEngine;
using UnityEngine.Assertions;

namespace Test
{
    public class TestDialogueSystem : MonoBehaviour
    {
        [SerializeField] private TestDialogueConfig _config;
        [SerializeField] private TextView _textView;

        [SerializeField] private ButtonView[] _dialogueButtonViews;

        private void Awake()
        {
            CheckReferences();
        }

        private void Start()
        {
            var length = _dialogueButtonViews.Length;
            Assert.IsTrue(length == _config.DialogueParamsCollection.Length);

            for (var i = 0; i < length; i++)
            {
                var config = _config.DialogueParamsCollection[i];
                var view = _dialogueButtonViews[i];

                view.SetText(config.Text);
                view.SetIndex(config.Index);

                view.ButtonClicked += AskForAnswer;
            }
        }

        private void AskForAnswer(int index)
        {
            Debug.Log($"Button with index = {index} - clicked");
            _textView.SetText($"Button with index = {index} - clicked");
        }

        private void CheckReferences()
        {
            if (!_config)
            {
                throw new Exception("Config is null at " + nameof(TestDialogueSystem));
            }

            if (_dialogueButtonViews == null)
            {
                throw new Exception("DialogueButtonViews is null at " + nameof(TestDialogueSystem));
            }

            foreach (var button in _dialogueButtonViews)
            {
                if (!button)
                {
                    throw new Exception("Button is null at " + nameof(TestDialogueSystem));
                }
            }
        }
    }
}
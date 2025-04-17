using UnityEngine;
using UnityEngine.InputSystem;

namespace VisualNovelGame
{
    public class InputReader : MonoBehaviour, Controls.IDialogueActions
    {
        private Controls _inputActions;
        private DialogController _dialogController;

        private void OnEnable()
        {
            _dialogController = FindObjectOfType<DialogController>();

            if (_inputActions != null)
            {
                return;
            }

            _inputActions = new Controls();
            _inputActions.Dialogue.SetCallbacks(this);
            _inputActions.Dialogue.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Dialogue.Disable();
        }

        public void OnNextPhrases(InputAction.CallbackContext context)
        {
            if (context.started && _dialogController.DialogPlay)
            {
                _dialogController.ContinueDialog();
            }
        }
    }
}
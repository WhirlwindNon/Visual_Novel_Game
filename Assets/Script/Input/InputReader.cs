using UnityEngine;
using UnityEngine.InputSystem;

namespace VisualNovelGame
{
    public class InputReader : MonoBehaviour, Controls.IDialogueActions
    {
        [SerializeField] private DialogController _dialogController;

        private Controls _inputActions;

        private void OnEnable()
        {
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
            if (context.started && _dialogController.IsDialogPlaying)
            {
                _dialogController.ContinueDialog();
            }
        }
    }
}
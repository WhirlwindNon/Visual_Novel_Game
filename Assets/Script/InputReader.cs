using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IDialogueActions
{
     Controls _inputActions;
     Dialogs _dialogs;

     private void OnEnable()
     {
         _dialogs = FindObjectOfType<Dialogs>();
         
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
        if (context.started && _dialogs.DialogPlay)
        {
            _dialogs.ContinueDialog();
            
        }
    }
}

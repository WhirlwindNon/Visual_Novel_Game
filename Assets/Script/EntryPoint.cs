using UnityEngine;

namespace VisualNovelGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private DialogParameters _dialogParameters;
        [SerializeField] private DialogController _dialogController;

        private void Awake()
        {
            _dialogController.Initialize(_dialogParameters);
        }
    }
}
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VisualNovelGame
{
    public class ButtonAction : MonoBehaviour
    {
        public int Index;
        private Button _button;
        private DialogController _dialogController;
        private UnityAction _clickAcktion;

        private void Start()
        {
            _button = GetComponent<Button>();
            _dialogController =
                FindObjectOfType<DialogController>(); // лучше не использовать метод "найти" когда много объектов в котором используется одинаковый скрипт

            _clickAcktion = new UnityAction(() => _dialogController.ChoiceButtonAction(Index));
            _button.onClick.AddListener(_clickAcktion);
        }

        private void Showlog()
        {
            Debug.Log("hi");
        }
    }
}
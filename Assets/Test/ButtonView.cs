using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class ButtonView : MonoBehaviour
    {
        public event Action<int> ButtonClicked;

        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _textContainer;

        private int _index;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        public void SetText(string text)
        {
            _textContainer.text = text;
        }

        private void OnClick()
        {
            ButtonClicked?.Invoke(_index);
        }
    }
}
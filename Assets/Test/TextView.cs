using TMPro;

using UnityEngine;

namespace Test
{
    public class TextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textContainer;

        public void SetText(string text)
        {
            _textContainer.text = text;
        }
    }
}
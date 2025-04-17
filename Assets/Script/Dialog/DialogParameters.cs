using System;

using TMPro;

using UnityEngine;

namespace VisualNovelGame
{
    [Serializable]
    public class DialogParameters
    {
        public TextAsset inkJson;

        public GameObject DialogPanel;
        public TextMeshProUGUI DialogText;
        public TextMeshProUGUI NameCharText;

        public GameObject ChoiceButtonPanel;
        public GameObject ChoiceButton;
    }
}
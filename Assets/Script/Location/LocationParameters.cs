using System;

using UnityEngine.UI;

namespace VisualNovelGame
{
    [Serializable]
    public class LocationParameters
    {
        public Button NextButton;
        public Button PreviousButton;

        public BackgroundConfig BackgroundConfig;
        public Image BackgroundImage;
    }
}
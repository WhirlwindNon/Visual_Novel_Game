using UnityEngine;

namespace VisualNovelGame
{
    public class LocationController
    {
        private readonly LocationParameters _locationParameters;

        private int _currentLocationIndex = 0;

        public LocationController(LocationParameters locationParameters)
        {
            _locationParameters = locationParameters;
        }

        public void Initialize()
        {
            _locationParameters.NextButton.onClick.AddListener(ChooseNext);
            _locationParameters.PreviousButton.onClick.AddListener(ChoosePrevious);

            SetBackground(_currentLocationIndex);
        }

        private void SetBackground(int index)
        {
            var background = _locationParameters.BackgroundConfig.Backgrounds[index];
            SetSprite(background);
        }

        public void Dispose()
        {
            _locationParameters.NextButton.onClick.RemoveListener(ChooseNext);
            _locationParameters.PreviousButton.onClick.RemoveListener(ChoosePrevious);
        }

        private void ChooseNext()
        {
            ++_currentLocationIndex;
            SetBackground(_currentLocationIndex);
        }

        private void ChoosePrevious()
        {
            --_currentLocationIndex;
            SetBackground(_currentLocationIndex);
        }

        private void SetSprite(Sprite sprite)
        {
            _locationParameters.BackgroundImage.sprite = sprite;
        }
    }
}
using UnityEngine;

namespace VisualNovelGame
{
    public class LocationController
    {
        private readonly LocationParameters _locationParameters;

        private int _currentLocationIndex = 0;
        private int _locationCount = 0;

        public LocationController(LocationParameters locationParameters)
        {
            _locationParameters = locationParameters;
            _locationCount = _locationParameters.BackgroundConfig.Backgrounds.Length;
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
            if (_currentLocationIndex + 1 > _locationCount - 1)
                return;
            ++_currentLocationIndex;
            SetBackground(_currentLocationIndex);
        }

        private void ChoosePrevious()
        {
            if (_currentLocationIndex - 1 < 0)
                return;
            --_currentLocationIndex;
            SetBackground(_currentLocationIndex);
        }

        private void SetSprite(Sprite sprite)
        {
            _locationParameters.BackgroundImage.sprite = sprite;
        }
    }
}

using System;
using UnityEngine;

namespace VisualNovelGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private DialogParameters _dialogParameters;

        [SerializeField]
        private DialogController _dialogController;

        [SerializeField]
        private GameObject[] _locations;

        private LocationController _locationController;

        private void Awake()
        {
            _locationController = new LocationController(_locations);

            _dialogController.Initialize(_dialogParameters);
            _locationController.Initialize();
        }

        private void Start()
        {
            _dialogController.Run();
        }

        private void OnDestroy()
        {
            _locationController.Dispose();
            _dialogController.Dispose();
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace VisualNovelGame
{
    public class LocationController
    {
        private int _locationCount = 0;

        private GameObject[] _locations;
        private Hashtable _locations_hashtable = new Hashtable();

        public LocationController(GameObject[] locations)
        {
            _locationCount = locations.Length;
            _locations = locations;
        }

        public void Initialize()
        {
            foreach (GameObject location in _locations)
            {
                Button[] _buttons = location.GetComponentsInChildren<Button>();
                foreach (Button button in _buttons)
                {
                    button.onClick.AddListener(() => ChooseLocation(button.gameObject.name));
                }
                _locations_hashtable[location.gameObject.name] = location;
            }
            DisableAllLocations();

            _locations[0].SetActive(true);
        }

        public void Dispose()
        {
            foreach (GameObject location in _locations)
            {
                Button[] _buttons = location.GetComponentsInChildren<Button>();
                foreach (Button button in _buttons)
                {
                    button.onClick.RemoveAllListeners();
                }
            }
        }

        private void ChooseLocation(string index)
        {
            DisableAllLocations();
            GetLocation(index).SetActive(true);
        }

        private void DisableAllLocations()
        {
            foreach (GameObject location in _locations)
            {
                location.SetActive(false);
            }
        }

        public GameObject GetLocation(string id)
        {
            return (GameObject)_locations_hashtable[id];
        }
    }
}

using System;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace VisualNovelGame {
public class ButtonView : MonoBehaviour {
  public int Index => _index;

  public event Action<int> ButtonClicked;

  [SerializeField]
  private TextMeshProUGUI _textMeshPro;
  [SerializeField]
  public Button _button;

  private int _index;

  private void Awake() { _button.onClick.AddListener(OnButtonClick); }

  private void OnDestroy() { _button.onClick.RemoveListener(OnButtonClick); }

  public void SetIndex(int index) { _index = index; }

  public void SetText(string text) { _textMeshPro.text = text; }

  private void OnButtonClick() {
    Debug.Log("aboba");
    ButtonClicked?.Invoke(_index);
  }
}
}

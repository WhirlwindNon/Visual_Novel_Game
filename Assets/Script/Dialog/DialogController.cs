using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace VisualNovelGame {
public class DialogController : MonoBehaviour {
  public bool IsDialogPlaying { get; private set; }
  public bool AIDialog { get; private set; }

  private Story _currentStory;
  private TextAsset _inkJson;

  private GameObject _dialogPanel;
  private GameObject _dialogPanelAI;
  private TextMeshProUGUI _dialogText;
  private InputField _dialogTextAI;

  private TextMeshProUGUI _nameCharText;
  private TextMeshProUGUI _Relationship;
  private TextMeshProUGUI _outputField;

  private GameObject _choiceButtonPanel;
  private ButtonView _choiceButtonPrefab;
  private OllamaJSON _context = JsonUtility.FromJson<OllamaJSON>(
      "{\"model\":\"llama3.2-vision:11b\",\"messages\":[{\"role\":\"system\"," +
      "\"content\":\"ты абоба. верни ответ в формате json где text это твой " +
      "ответ, а relationship это твоё отношение к пользователю от 0 до 100, " +
      "по умолчанию оно равно 50 " +
      "\"}],\"format\":{\"type\":\"object\",\"properties\":{\"text\":{" +
      "\"type\":\"string\"},\"relationship\":{\"type\":\"integer\"}}," +
      "\"required\":[\"text\",\"relationship\"]},\"stream\":false}");
  private List<ButtonView> _createdViews = new List<ButtonView>();

  public void Initialize(DialogParameters context, GameObject[] locations) {
    _inkJson = context.inkJson;
    _dialogPanel = context.DialogPanel;
    _dialogPanelAI = context.DialogPanelAI;
    _dialogText = context.DialogText;
    _dialogTextAI = context.DialogTextAI;
    _nameCharText = context.NameCharText;
    _choiceButtonPanel = context.ChoiceButtonPanel;
    _choiceButtonPrefab = context.ChoiceButtonPrefab;
    _Relationship = _dialogPanelAI.GetComponentInChildren<TextMeshProUGUI>();
    // _outputField = _dialogPanelAI.GetComponentInChildren<Image>()
    //                    .GetComponentInChildren<TextMeshProUGUI>();
    // _outputField = tmp.GetComponentInChildren<TextMeshProUGUI>();

    _currentStory = new Story(_inkJson.text);

    foreach (GameObject location in locations) {
      GameObject CharactersNode = location.transform.GetChild(2).gameObject;
      Button[] _buttons = CharactersNode.GetComponentsInChildren<Button>();
      foreach (Button button in _buttons) {
        button.onClick.AddListener(() => StartDialog());
      }
    }
  }

  public void Run() {
    // StartDialog();
  }

  public void Dispose() {
    foreach (var view in _createdViews) {
      Debug.Log("dispose");
      view.ButtonClicked -= ChoiceButtonAction;
    }
  }

  public void ContinueDialog() {
    Debug.Log(_currentStory.canContinue);
    if (_currentStory.canContinue) {
      ShowDialog();
      ShowChoiceButton();
    } else if (AIDialog) {
      StartAI();
    } else {
      AIDialog = true;
      ExitDialog();
    }
  }

  private void ChoiceButtonAction(int choiceIndex) {
    _currentStory.ChooseChoiceIndex(choiceIndex);
    _choiceButtonPanel.SetActive(false);
    ContinueDialog();
  }

  private void StartDialog() {
    IsDialogPlaying = true;
    _dialogPanel.SetActive(true);
    ContinueDialog();
  }

  private void ShowDialog() {
    _dialogText.text = _currentStory.Continue();
    // _nameCharText.text = (string)_currentStory.variablesState["CharName"];
  }

  private void ShowChoiceButton() {
    var currentChoices = _currentStory.currentChoices;
    // Debug.Log(_currentStory.currentChoices);
    if (currentChoices.Count <= 0)
      return;

    _choiceButtonPanel.SetActive(true);

    for (var i = 0; i < currentChoices.Count; i++) {
      var choiceButtonView =
          Instantiate(_choiceButtonPrefab, _choiceButtonPanel.transform);

      choiceButtonView.ButtonClicked += ChoiceButtonAction;
      choiceButtonView.SetIndex(i);
      choiceButtonView.SetText(currentChoices[i].text);
      _createdViews.Add(choiceButtonView);
    }
  }

  private void StartAI() {
    _dialogPanel.SetActive(false);
    _dialogPanelAI.SetActive(true);
  }

  public void ExitDialog() {
    Debug.Log("end");
    IsDialogPlaying = false;
    _dialogPanel.SetActive(false);
    _dialogPanelAI.SetActive(false);
  }

  public void Submit() {
    Message msg = JsonUtility.FromJson<Message>(
        "{\"role\": \"user\", \"content\":\"" + _dialogTextAI.text + "\" }");
    _context.messages.Add(msg);
    StartCoroutine(GetSimpleRequest());
    _dialogTextAI.text = "";
  }

  public IEnumerator GetSimpleRequest() {
    string json = JsonUtility.ToJson(_context);
    Debug.Log(json);
    using (var www =
               UnityWebRequest.Post("https://ollama.phimosis.space/api/chat",
                                    json, "application/json")) {
      Debug.Log("ready");
      yield return www.SendWebRequest();

      if (www.result == UnityWebRequest.Result.Success) {
        LlamaResponse llama =
            JsonUtility.FromJson<LlamaResponse>(www.downloadHandler.text);
        Content content = JsonUtility.FromJson<Content>(llama.message.content);
        Debug.Log(content.text);
        // _outputField.text = content.text;
        _Relationship.text = content.relationship;
        _context.messages.Add(llama.message);

      } else {
        Debug.Log(www.downloadHandler.text);
        Debug.Log(www.error);
      }
    }
  }
}
}

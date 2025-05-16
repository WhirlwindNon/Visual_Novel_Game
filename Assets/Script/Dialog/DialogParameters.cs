using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace VisualNovelGame {
[Serializable]
public class DialogParameters {
  public TextAsset inkJson;

  public GameObject DialogPanel;
  public GameObject DialogPanelAI;
  public TextMeshProUGUI DialogText;
  public InputField DialogTextAI;

  public TextMeshProUGUI NameCharText;

  public GameObject ChoiceButtonPanel;
  public ButtonView ChoiceButtonPrefab;
}

[Serializable]
public class OllamaJSON {
  public string model;
  public List<Message> messages;
  public bool stream;
  public Format format;
  // public Options options;
}

[Serializable]
public class Message {
  public string role;
  public string content;
}

[Serializable]
public class Format {
  public string type;
  public Properties properties;
  public string[] required;
}

[Serializable]
public class Properties {
  public Property text;
  public Property relationship;
}

[Serializable]
public class Property {
  public string type;
}

[Serializable]
public class LlamaResponse {
  public string model;
  public string created_at;
  public Message message;
  public string done_reason;
  public bool done;
  public long total_duration;
  public long load_duration;
  public int prompt_eval_count;
  public long prompt_eval_duration;
  public int eval_count;
  public long eval_duration;
}

[Serializable]
public class Content {
  public string text;
  public string relationship;
}

}

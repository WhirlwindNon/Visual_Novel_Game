using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    public int index;
    private Button _button;
    private Dialogs _dialogs;
    private UnityAction _clickAcktion;
    void Start()
    {
        _button = GetComponent<Button>();
        _dialogs = FindObjectOfType<Dialogs>(); // лучше не использовать метод "найти" когда много объектов в котором используется одинаковый скрипт
        _clickAcktion = new UnityAction((() => _dialogs.ChoiceButtonAction(index)));
        _button.onClick.AddListener(_clickAcktion);
    }

    private void Showlog()
    {
        Debug.Log("hi");
    }
    
}

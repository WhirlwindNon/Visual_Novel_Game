using System;

using UnityEngine;

namespace Test
{
    [CreateAssetMenu(menuName = "VisualNovelGame/" + nameof(TestDialogueConfig), fileName = nameof(TestDialogueConfig))]
    public class TestDialogueConfig : ScriptableObject
    {
        [field: SerializeField] public DialogueParams[] DialogueParamsCollection { get; private set; }
    }

    [Serializable]
    public class DialogueParams
    {
        public int Index;
        public string Text;
    }
}
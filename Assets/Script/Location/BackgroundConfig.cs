using UnityEngine;

namespace VisualNovelGame
{
    [CreateAssetMenu(menuName = nameof(VisualNovelGame) + "/" + nameof(BackgroundConfig),
        fileName = nameof(BackgroundConfig))]
    public class BackgroundConfig : ScriptableObject
    {
        public Sprite[] Backgrounds;
    }
}
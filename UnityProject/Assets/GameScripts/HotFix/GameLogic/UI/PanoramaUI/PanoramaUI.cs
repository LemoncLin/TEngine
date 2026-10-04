using UnityEngine;
using TEngine;

namespace GameLogic
{
    public class PanoramaUI : UIWidget
    {
        private Transform _sceneListRoot;

        protected override void ScriptGenerator()
        {
            _sceneListRoot = FindChildComponent<Transform>("m_tf_SceneList");
        }
    }
}

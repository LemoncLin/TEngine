using UnityEngine;
using TEngine;

namespace GameLogic
{
    public class SurroundingUI : UIWidget
    {
        private Transform _categoryTabRoot;
        private Transform _infoListRoot;

        protected override void ScriptGenerator()
        {
            _categoryTabRoot = FindChildComponent<Transform>("m_tf_CategoryTabs");
            _infoListRoot = FindChildComponent<Transform>("m_tf_InfoList");
        }
    }
}

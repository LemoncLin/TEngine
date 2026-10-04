using UnityEngine;
using TEngine;

namespace GameLogic
{
    public class ProjectNavigationUI : UIWidget
    {
        private Transform _modelDisplayRoot;

        protected override void ScriptGenerator()
        {
            _modelDisplayRoot = FindChildComponent<Transform>("m_tf_ModelRoot");
        }
    }
}

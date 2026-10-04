using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    public class UnitBrowsingUI : UIWidget
    {
        private Transform _unitListRoot;
        private Image _imgFloorPlan;

        protected override void ScriptGenerator()
        {
            _unitListRoot = FindChildComponent<Transform>("m_tf_UnitList");
            _imgFloorPlan = FindChildComponent<Image>("m_img_FloorPlan");
        }
    }
}

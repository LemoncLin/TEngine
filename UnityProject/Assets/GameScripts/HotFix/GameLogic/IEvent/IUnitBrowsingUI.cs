using TEngine;

namespace GameLogic
{
    [EventInterface(EEventGroup.GroupUI)]
    public interface IUnitBrowsingUI
    {
        void ShowUnitBrowsing();
        void CloseUnitBrowsing();
        void OnUnitSelected(int unitId);
    }
}

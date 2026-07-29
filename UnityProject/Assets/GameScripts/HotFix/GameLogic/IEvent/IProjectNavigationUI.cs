using TEngine;

namespace GameLogic
{
    [EventInterface(EEventGroup.GroupUI)]
    public interface IProjectNavigationUI
    {
        void ShowProjectNavigation();
        void CloseProjectNavigation();
        void OnFloorSelected(int floorIndex);
    }
}

using TEngine;

namespace GameLogic
{
    [EventInterface(EEventGroup.GroupUI)]
    public interface IMainMenuUI
    {
        void ShowMainMenu();
        void CloseMainMenu();
        void OnTabSelected(int tabIndex);
        void OnModuleLoading(int tabIndex);
        void OnModuleLoaded(int tabIndex);
    }
}

using TEngine;

namespace GameLogic
{
    [EventInterface(EEventGroup.GroupUI)]
    public interface IMainMenuUI
    {
        void ShowMainMenu();
        void CloseMainMenu();
    }
}

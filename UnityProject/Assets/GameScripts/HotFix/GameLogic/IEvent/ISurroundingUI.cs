using TEngine;

namespace GameLogic
{
    [EventInterface(EEventGroup.GroupUI)]
    public interface ISurroundingUI
    {
        void ShowSurrounding();
        void CloseSurrounding();
        void OnCategorySelected(int categoryId);
    }
}

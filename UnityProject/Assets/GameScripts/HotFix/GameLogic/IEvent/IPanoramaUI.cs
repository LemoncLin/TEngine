using TEngine;

namespace GameLogic
{
    [EventInterface(EEventGroup.GroupUI)]
    public interface IPanoramaUI
    {
        void ShowPanorama();
        void ClosePanorama();
        void OnSceneSelected(int sceneIndex);
    }
}

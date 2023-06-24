using System;

namespace UI.ResultScreen
{
    public interface IUIResultScreenView
    {
        event Action OnResultPanelClosed;
    }
}
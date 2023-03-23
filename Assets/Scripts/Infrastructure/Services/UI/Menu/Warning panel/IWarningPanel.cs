using System;

namespace UI.Warning_panel
{
    public interface IWarningPanel
    {
        void Show(string message, Action onConfirm = null);
    }
}
using System;

namespace UI.Warning_panel
{
    public interface IWarningPanel
    {
        void Show(string term, Action onConfirm = null);
    }
}
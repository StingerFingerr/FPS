using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Warning_panel
{
    public class WarningPanel: MonoBehaviour, IWarningPanel
    {
        public TextMeshProUGUI messageLabel;
        public Button okButton;
        
        private Action _onConfirm;
        
        public void Show(string message, Action onConfirm = null)
        {
            if(string.IsNullOrEmpty(message) is false)
                messageLabel.text = message;
            _onConfirm = onConfirm;
            gameObject.SetActive(true);
        }

        private void Start() => 
            okButton.onClick.AddListener(Confirm);

        private void Confirm()
        {
            _onConfirm?.Invoke();
            _onConfirm = null;
        }
    }
}
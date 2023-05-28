using System;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Warning_panel
{
    public class WarningPanel: MonoBehaviour, IWarningPanel
    {
        [SerializeField] private Localize messageLocalize;
        [SerializeField] private Button okButton;
        
        private Action _onConfirm;
        
        public void Show(string term, Action onConfirm = null)
        {
            messageLocalize.SetTerm(term);
            
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
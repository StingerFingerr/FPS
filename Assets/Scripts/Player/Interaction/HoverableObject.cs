using System;
using UnityEngine;
using Zenject;

namespace Player.Interaction
{
    [RequireComponent(typeof(Collider))]
    public abstract class HoverableObject: MonoBehaviour
    {
        public InteractableObject interactableObject;
        
        [SerializeField] private string onHoverTerm = string.Empty;
        [SerializeField] private string onHoverName = string.Empty;

        public static event Action<string> onHoverBegin;
        public static event Action onHoverEnd;

        private FirstPersonController _player;
        private const string Name = "{[NAME]}";
        private const float MaxDistanceToPickUp = 2f;
        
        private string _message;
        private bool _isHovered;


        [Inject]
        private void Construct(FirstPersonController player) => 
            _player = player;

        private void OnEnable() => 
            OnHoverEnd();

        private void OnMouseOver()
        {
            if(CheckDistance() && _isHovered)
                OnMouseExit();
            
            if(CheckDistance() is false && _isHovered is false)
                OnMouseEnter();
        }

        protected void OnMouseEnter()
        {
            if (CheckDistance())
                return;
            _isHovered = true;

            interactableObject.enabled = true;
            OnHoverBegin();
            onHoverBegin?.Invoke(GetHoverMessage());
        }

        protected void OnMouseExit()
        {
            if (_isHovered is false)
                return;
            _isHovered = false;

            interactableObject.enabled = false;
            OnHoverEnd();
            onHoverEnd?.Invoke();
        }

        protected abstract void OnHoverBegin();
        protected abstract void OnHoverEnd();

        private string GetHoverMessage()
        {
            if (string.IsNullOrEmpty(_message))
                _message = I2.Loc.LocalizationManager
                    .GetTranslation(onHoverTerm)
                    .Replace(Name, onHoverName);
            
            return _message;
        }

        private bool CheckDistance() => 
            Vector3.Distance(transform.position, _player.transform.position) > MaxDistanceToPickUp;
    }
}
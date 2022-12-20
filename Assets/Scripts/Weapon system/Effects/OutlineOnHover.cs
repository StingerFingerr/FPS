using Player.Interaction;
using UnityEngine;

namespace Effects
{
    public class OutlineOnHover: MonoBehaviour, IHoverable
    {
        public AnimationCurve pulsingPattern = AnimationCurve.EaseInOut(0,0,1,0);
        public float pulsationSpeed = 1f;
        public float pulsationSize = 1f;
        public float minPulsationSize = .5f;
        public float pulsationOffset = .3f;
        public Outline outline;
        
        public string onHoverTerm = string.Empty;
        public string onHoverName = string.Empty;
        private string NAME = "{[NAME]}";

        private Coroutine _outlineAnimation;

        private bool _isOnHover;
        private float _time;
        private string _message;

        private void Update()
        {
            SetOutlineWidth();
            UpdateTime();
        }

        public string OnHoverBegin()
        {
            if (_isOnHover)
                return _message;
            
            _time = -pulsationOffset;
            _isOnHover = true;
            outline.enabled = true;
            enabled = true;
            
            return GetHoverMessage();
        }

        public void OnHoverEnd()
        {
            if(_isOnHover is false)
                return;
            
            _isOnHover = false;
            outline.enabled = false;
            enabled = false;
        }

        private void SetOutlineWidth() => 
            outline.OutlineWidth = pulsingPattern.Evaluate(_time) * pulsationSize + minPulsationSize;

        private void UpdateTime()
        {
            _time += Time.deltaTime * pulsationSpeed;
            if (_time >= 1f)
                _time = 0;
        }

        private string GetHoverMessage()
        {
            if (string.IsNullOrEmpty(_message))
                _message = I2.Loc.LocalizationManager
                    .GetTranslation(onHoverTerm)
                    .Replace(NAME, onHoverName);
            
            return _message;
        }
    }
}
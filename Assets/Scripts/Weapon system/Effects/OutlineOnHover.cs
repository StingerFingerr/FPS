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
        public Outline outline;
        
        public string onHoverMessage = string.Empty;

        private bool _isOnHover;
        private float _time;

        private void SimulateOutline()
        {
            enabled = true;
            _isOnHover = true;
            outline.enabled = true;
            outline.OutlineWidth = pulsingPattern.Evaluate(_time) * pulsationSize + minPulsationSize;
            UpdateTime();
        }

        private void UpdateTime()
        {
            _time += Time.deltaTime * pulsationSpeed;
            if (_time >= 1f)
                _time = 0;
        }

        private void LateUpdate()
        {
            if(_isOnHover is false)
                DisableOutline();
            
            _isOnHover = false;
        }

        private void DisableOutline()
        {
            _time = 0;
            outline.enabled = false;
            enabled = false;
        }

        public string OnHover()
        {
            SimulateOutline();
            
            return onHoverMessage;
        }
    }
}
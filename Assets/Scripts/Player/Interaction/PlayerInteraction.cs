using System.Collections;
using UnityEngine;
using Weapon;
using Weapons;

namespace Player.Interaction
{
    public class PlayerInteraction: MonoBehaviour
    {
        public WeaponHolder weaponHolder;
        
        [Header("Pointer")] 
        public float pointerDistance = 2f;
        public float castPointerTimeout = .1f;
        public LayerMask interactableMask;
        public Transform pointerForward;
        
        
        private IInteractable _interactable;
        private IHoverable _hoverable;
        
        
        private void OnEnable() => 
            StartCoroutine(StartCastingPointers());

        private void Update() => 
            _hoverable?.OnHover();

        private IEnumerator StartCastingPointers()
        {
            while (true)
            {
                CastPointer();
                yield return new WaitForSeconds(castPointerTimeout);
            }
        }

        private void CastPointer()
        {
            Ray ray = new Ray(pointerForward.position, pointerForward.forward);


            if (Physics.Raycast(ray, out RaycastHit hit, pointerDistance, interactableMask))
            {
                _interactable = hit.transform.gameObject.GetComponent<IInteractable>();
                _hoverable = hit.transform.gameObject.GetComponent<IHoverable>();
            }
            else
            {
                _interactable = null;
                _hoverable = null;
            }
        }

        private void OnInteract()
        {
            _interactable?.Interact();
            
            if (_interactable is WeaponBase weapon)            
                weaponHolder.SetNewWeapon(weapon);
            
            _interactable = null;
        }
    }
}
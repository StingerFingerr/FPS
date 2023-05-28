using UnityEngine;
using Zenject;

public abstract class InteractableObject: MonoBehaviour
{
    private PlayerInputs _inputs;

    [Inject]
    private void Construct(PlayerInputs inputs) => 
        _inputs = inputs;

    private void OnEnable() => 
        _inputs.onInteract += Interact;

    private void OnDisable() => 
        _inputs.onInteract -= Interact;

    protected abstract void Interact();
}
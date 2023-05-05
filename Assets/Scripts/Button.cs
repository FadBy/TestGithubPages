using UnityEngine;
using UnityEngine.Events;

public abstract class Button : MonoBehaviour
{
    public UnityEvent onActive;
    public UnityEvent onDeactive;

    private bool wasActive = false;

    protected virtual void FixedUpdate()
    {
        if (IsActive() && !wasActive)
        {
            onActive.Invoke();
            wasActive = true;
        }
        else if (IsDeactive() && wasActive)
        {
            onDeactive.Invoke();
            wasActive = false;
        }
    }

    protected abstract bool IsActive();
    protected abstract bool IsDeactive();
}

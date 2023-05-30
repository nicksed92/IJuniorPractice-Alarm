using UnityEngine;
using UnityEngine.Events;

public class AlarmTrigger : MonoBehaviour
{
    [HideInInspector] public UnityEvent<bool> Triggered = new UnityEvent<bool>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            Triggered.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            Triggered.Invoke(false);
        }
    }
}

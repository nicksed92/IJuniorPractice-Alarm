using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private int _moveSpeed = Animator.StringToHash("MoveSpeed");

    private void Awake()
    {
        PlayerMovement.SpeedChanged.AddListener(OnSpeedChanged);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnSpeedChanged(float speed)
    {
        _animator.SetFloat(_moveSpeed, speed);
    }
}
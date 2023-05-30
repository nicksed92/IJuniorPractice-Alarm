using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _trigger;

    private AudioSource _audioSource;
    private Coroutine _currentVolumeRoutine;
    private WaitForSeconds _waitForSeconds;

    private void Awake()
    {
        _trigger.Triggered.AddListener(OnTriggered);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _waitForSeconds = new WaitForSeconds(0.05f);
    }

    private void OnTriggered(bool isEntered)
    {
        StopRoutine(_currentVolumeRoutine);
        _currentVolumeRoutine = StartCoroutine(ChangeVolume(isEntered));
    }

    private void StopRoutine(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private IEnumerator ChangeVolume(bool isEntered)
    {
        if (isEntered && _audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        float targetVolume = isEntered ? 1f : 0f;
        float delta;

        while (_audioSource.volume != targetVolume)
        {
            delta = isEntered ? Time.deltaTime : -Time.deltaTime;
            _audioSource.volume = Mathf.Clamp01(_audioSource.volume + delta);
            yield return _waitForSeconds;
        }

        if (isEntered == false)
        {
            _audioSource.Stop();
        }
    }
}
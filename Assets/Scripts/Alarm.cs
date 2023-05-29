using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    private AudioSource _audioSource;
    private Coroutine _currentVolumeRoutine;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _waitForSeconds = new WaitForSeconds(0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            StopRoutine(_currentVolumeRoutine);
            _currentVolumeRoutine = StartCoroutine(UpVolume());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            StopRoutine(_currentVolumeRoutine);
            _currentVolumeRoutine = StartCoroutine(DownVolume());
        }
    }

    private void StopRoutine(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private IEnumerator UpVolume()
    {
        if (_audioSource.isPlaying == false)
        {
            _audioSource.Play();
        }

        while (_audioSource.volume < 1f)
        {
            _audioSource.volume += Time.deltaTime;
            yield return _waitForSeconds;
        }
    }

    private IEnumerator DownVolume()
    {
        while (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime;
            yield return _waitForSeconds;
        }

        _audioSource.Stop();
    }
}

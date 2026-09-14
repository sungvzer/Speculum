using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class PhoneCall : MonoBehaviour
{
    [Header("INITIAL DELAY")]
    [Tooltip("Enable random start delay, overriding 'First Delay'")]
    public bool randomizeStartTime = false;

    [Tooltip("Fixed delay before the very first playback (if not randomized)")]
    [Min(0f)] public float firstDelay = 0.0f;

    [Tooltip("Minimum wait time before the first playback (in seconds)")]
    [Min(0f)] public float randomStartTimeMinSeconds = 0.0f;

    [Tooltip("Maximum wait time before the first playback (in seconds)")]
    [Min(0f)] public float randomStartTimeMaxSeconds = 0.0f;


    [Header("LOOP DELAY")]
    [Tooltip("Enable random pause between loops, overriding 'Pause Delay'")]
    public bool randomizePauseDelay = false;

    [Tooltip("Fixed pause between playbacks (if not randomized)")]
    [Min(0f)] public float pauseDelay = 5.0f;

    [Tooltip("Minimum pause duration between loops (in seconds)")]
    [Min(0f)] public float randomPauseDelayMinSeconds = 0.0f;

    [Tooltip("Maximum pause duration between loops (in seconds)")]
    [Min(0f)] public float randomPauseDelayMaxSeconds = 0.0f;


    [Header("RESPONSE AUDIO")]
    public AudioClip responseAudioClip;


    [Header("EVENTS")]
    public UnityEvent OnCallStarted;

    public UnityEvent OnCallEnded;


    private AudioSource audioSource;
    private AudioClip originalRingtoneClip;
    private Coroutine loopCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            originalRingtoneClip = audioSource.clip;
        }

        if (randomizeStartTime)
        {
            randomStartTimeMinSeconds = Mathf.Max(0.0f, randomStartTimeMinSeconds);
            randomStartTimeMaxSeconds = Mathf.Max(randomStartTimeMinSeconds, randomStartTimeMaxSeconds);
        }

        if (randomizePauseDelay)
        {
            randomPauseDelayMinSeconds = Mathf.Max(0.0f, randomPauseDelayMinSeconds);
            randomPauseDelayMaxSeconds = Mathf.Max(randomPauseDelayMinSeconds, randomPauseDelayMaxSeconds);
        }
    }

    private void Start()
    {
        audioSource.loop = false;
        loopCoroutine = StartCoroutine(PlayAudioLoop());
    }

    private IEnumerator PlayAudioLoop()
    {
        audioSource.clip = originalRingtoneClip;

        if (randomizeStartTime)
        {
            float randomDelay = Random.Range(randomStartTimeMinSeconds, randomStartTimeMaxSeconds);
            Debug.Log($"Random start delay: {randomDelay} seconds");
            yield return new WaitForSeconds(randomDelay);
        }
        else if (firstDelay > 0f)
        {
            yield return new WaitForSeconds(firstDelay);
        }

        while (true)
        {
            OnCallStarted?.Invoke();
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length);

            OnCallEnded?.Invoke();

            if (randomizePauseDelay)
            {
                float randomDelay = Random.Range(randomPauseDelayMinSeconds, randomPauseDelayMaxSeconds);
                yield return new WaitForSeconds(randomDelay);
            }
            else if (pauseDelay > 0f)
            {
                yield return new WaitForSeconds(pauseDelay);
            }
        }
    }

    public void AnswerCall()
    {
        if (loopCoroutine != null)
        {
            StopCoroutine(loopCoroutine);
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        OnCallEnded?.Invoke();

        if (responseAudioClip != null)
        {
            loopCoroutine = StartCoroutine(PlayResponseAndRestartLoop());
        }
        else
        {
            loopCoroutine = StartCoroutine(PlayAudioLoop());
        }
    }

    private IEnumerator PlayResponseAndRestartLoop()
    {
        audioSource.clip = responseAudioClip;
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        loopCoroutine = StartCoroutine(PlayAudioLoop());
    }
}

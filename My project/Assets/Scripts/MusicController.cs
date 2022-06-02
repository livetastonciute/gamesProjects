using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private AudioSource lead;

    [SerializeField]
    private List<AudioSource> pool;

    [SerializeField]
    private int phaseCount = 4;

    private readonly List<AudioSource> playing = new List<AudioSource>();
    private float phaseLength;

    private void Awake()
    {
        phaseLength = lead.clip.length / phaseCount;
    }
    // Start is called before the first frame update
    void Start()
    {
        lead.Play();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var audioSource in playing)
        {
            audioSource.time = lead.time;
        }
    }
    public void PlayRandom()
    {
        if (pool.Count == 0)
        {
            return;
        }

        var random = pool[Random.Range(0, pool.Count)];
        Play(random);
    }
    public void StopRandom()
    {
        if (pool.Count == 0)
        {
            return;
        }

        var random = playing[Random.Range(0, playing.Count)];
        Stop(random);
    }
    public void StopAll()
    {
        lead.Stop();
        foreach(var audioSource in playing)
        {
            Stop(audioSource);
        }
    }
    private void Play(AudioSource audioSource)
    {
        StartCoroutine(PlayOnPhase(audioSource));
    }
    private void Stop(AudioSource audioSource)
    {
        StartCoroutine(StopOnPhase(audioSource));
    }
    private bool IsNextPhase()
    {
        var currentPhaseTime = lead.time % phaseLength + Time.deltaTime;
        return currentPhaseTime >= phaseLength;
    }
    private IEnumerator PlayOnPhase(AudioSource audioSource)
    {
        pool.Remove(audioSource);

        if (!lead.isPlaying)
        {
            lead.Play();
        }
        yield return new WaitUntil(IsNextPhase);

        audioSource.Play();
        audioSource.time = lead.time;

        playing.Add(audioSource);
    }

    private IEnumerator StopOnPhase(AudioSource audioSource)
    {
        playing.Remove(audioSource);

        yield return new WaitUntil(IsNextPhase);

        audioSource.Stop();
        pool.Add(audioSource);
    }


}

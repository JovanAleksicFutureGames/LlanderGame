using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource _audioSource;
    [field:SerializeField]public AudioSource PlayerAudioSource { get; private set; }
    [Header("Player Audio Clips")]
    [SerializeField]private AudioClip[] _playerAudioClips;
    private void Awake()
    {
        instance = this;
        if (instance != null) Destroy(this);
        _audioSource = GetComponent<AudioSource>();
        if(PlayerManager.instance != null) 
        {
            PlayerAudioSource = PlayerManager.instance.GetPlayer(0).gameObject.GetComponent<AudioSource>();
        }
        DontDestroyOnLoad(this);
    }

    public void CleanInstances(GameObject targetObj) 
    {
        if (instance != null) Destroy(targetObj);
    }

    public void AssignPlayerAudioController() 
    {
        PlayerAudioSource = PlayerManager.instance.GetPlayer(0).gameObject.GetComponent<AudioSource>();
    }

    public void PlayerEnglineSound() 
    {
        if (PlayerAudioSource == null)
        {
            PlayerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
        PlayerAudioSource.Play();
        Debug.Log("Playing Engine Sound");

    }

    public void SetEngineClip() 
    {
        PlayerAudioSource.clip = _playerAudioClips[0];
    }

    public void PlayerPlayJumpSound() 
    {
        if (PlayerAudioSource == null)
        {
            PlayerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
        PlayerAudioSource.PlayOneShot(_playerAudioClips[1]);

    }

    public void PlayerPlayExplosionSound() 
    {
        if (PlayerAudioSource == null)
        {
            PlayerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
        PlayerAudioSource.PlayOneShot(_playerAudioClips[2]);

    }

    public void StopPlayerSound()
    {
        if (PlayerAudioSource.clip != null)
            PlayerAudioSource.Stop();
    }
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource _audioSource;
    [field:SerializeField]public AudioSource PlayerAudioSource { get; private set; }
    [Header("Player Audio Clips")]
    [SerializeField]private AudioClip[] _playerAudioClips;
    [SerializeField] private AudioClip _victoryClip;
    [SerializeField] private AudioClip _musicClip;
    [SerializeField] private AudioClip _buttonPress;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            CleanInstances(gameObject);
        }
        _audioSource = GetComponent<AudioSource>();
        if(PlayerManager.instance != null) 
        {
            PlayerAudioSource = PlayerManager.instance.GetPlayer(0).gameObject.GetComponent<AudioSource>();
        }
        _audioSource.enabled = true;
        _audioSource.clip = _musicClip;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _audioSource.Play();
    }

    private void AssignPlayerAudioSource()
    {
        PlayerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    public void CleanInstances(GameObject targetObj)
    {
        if (instance != null) Destroy(targetObj);
    }

    public void PlayerPlayJumpSound()
    {
        if (PlayerAudioSource == null)
        {
            AssignPlayerAudioSource();
        }
        PlayerAudioSource.PlayOneShot(_playerAudioClips[1]);
    }

    public void PlayerTakeDamage() 
    {
        if(PlayerAudioSource == null)
        {
            AssignPlayerAudioSource();
        }
        PlayerAudioSource.PlayOneShot(_playerAudioClips[3]);
    }

    public void PlayVictorySound() 
    {

        _audioSource.Stop();
        _audioSource.PlayOneShot(_victoryClip);
    }

    public void PressButton() 
    {        _audioSource.PlayOneShot(_buttonPress);
    }
}

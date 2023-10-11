using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private GameObject _victoryParticle;
    [SerializeField] private AudioClip _victorySound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _victoryParticle.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            _victoryParticle.SetActive(true);
            GameManager.instance.WinCondition();
        }
    }


}

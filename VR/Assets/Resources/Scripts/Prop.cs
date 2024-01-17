using UnityEngine;

public class Prop : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnCharacterTouched()
    {
        audioSource.Play();
    }
}

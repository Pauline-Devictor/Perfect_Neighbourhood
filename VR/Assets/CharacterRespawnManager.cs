using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRespawnManager : MonoBehaviour
{
    [SerializeField] private GameObject characterPrefab;
    private GameObject currentCharacter;

    void Start()
    {
        Character.OnHit += OnCharacterHitHandler;
        currentCharacter = Instantiate(characterPrefab);
    }

    public void OnCharacterHitHandler()
    {
        Invoke("RegenerateRagdoll", 10);
    }

    public void RegenerateRagdoll()
    {
        Destroy(currentCharacter);
        currentCharacter = Instantiate(characterPrefab);
    }
}

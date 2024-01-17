using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject root;
    private Collider propDetectionCollider;
    private List<Rigidbody> ragdollRigidbody;

    private void Start()
    {
        ragdollRigidbody = GetRigidbodyRecursive(root);
        propDetectionCollider = GetComponent<Collider>();
        DisableRagdoll();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Prop"))
            return;

        EnableRagdoll();
        propDetectionCollider.enabled = false;
        if (other.TryGetComponent(out Prop prop))
            prop.OnCharacterTouched();
    }

    private void DisableRagdoll()
    {
        ragdollRigidbody.ForEach(rigidbody => rigidbody.constraints = RigidbodyConstraints.FreezeAll);
    }

    private void EnableRagdoll()
    {
        ragdollRigidbody.ForEach(rigidbody => rigidbody.constraints = RigidbodyConstraints.None);
    }

    private List<Rigidbody> GetRigidbodyRecursive(GameObject obj)
    {
        List<Rigidbody> rigidbody = new List<Rigidbody>();

        if (obj.TryGetComponent(out Rigidbody collider))
            rigidbody.Add(collider);

        foreach (Transform child in obj.transform)
            rigidbody.AddRange(GetRigidbodyRecursive(child.gameObject));

        return rigidbody;
    }


}

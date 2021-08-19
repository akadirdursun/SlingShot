using UnityEngine;

public class RagdollControl : MonoBehaviour
{
    [SerializeField] private Animator characterAnim;
    [SerializeField] private Collider mainCollider;

    private Rigidbody[] ragdollRBs;
    private Collider[] ragdollColliders; 
    private void Awake()
    {
        characterAnim = GetComponent<Animator>();
        mainCollider = GetComponent<Collider>();

        ragdollRBs = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();        
    }


    private void RagdollActivity(bool isActive)
    {
        characterAnim.enabled = !isActive;

        for (int i = 1; i < ragdollRBs.Length; i++)
        {
            ragdollRBs[i].isKinematic = !isActive;
            ragdollColliders[i].enabled = isActive;
        }
        mainCollider.isTrigger = isActive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
            RagdollActivity(true);
    }

    private void OnDisable()
    {
        RagdollActivity(false);
    }
}
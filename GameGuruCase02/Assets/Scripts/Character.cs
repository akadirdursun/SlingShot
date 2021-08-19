using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlingShotProject
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Rigidbody characterRB;
        [SerializeField] private Animator characterAnim;
        [SerializeField] private Transform shoulderTarget;

        private bool isShooted;

        public Transform ShoulderTarget { get => shoulderTarget; }

        private void OnEnable()
        {
            SetAsDefault();
        }

        private void SetAsDefault()
        {
            characterAnim.SetBool("IsSeated", false);
            characterAnim.SetBool("IsFlying", false);
            characterRB.velocity = Vector3.zero;
        }

        public void PutOnSeat(Vector3 pos)
        {
            characterRB.isKinematic = true;
            transform.position = pos;
            characterAnim.SetBool("IsSeated", true);
        }

        public void LiftOffSeat(Vector3 pos)
        {
            transform.position = pos;
            characterAnim.SetBool("IsSeated", false);
        }

        public void Shoot(Vector3 force)
        {
            transform.parent = null;
            characterRB.isKinematic = false;
            characterRB.AddForce(force, ForceMode.Impulse);
            characterAnim.SetBool("IsFlying", true);
            isShooted = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isShooted && !other.CompareTag("Target"))
            {
                SetAsDefault();
                gameObject.SetActive(false);
                isShooted = false;
            }
        }
    }
}
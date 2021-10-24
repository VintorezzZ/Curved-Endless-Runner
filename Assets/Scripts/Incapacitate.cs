using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incapacitate : MonoBehaviour
{    
    private Rigidbody[] _ragdollRigidbodies;    
    private Collider[] _ragdollColliders;
    public Animator animator;
    public CharacterController charBoxCollider;
    public Collider charCupsuleCollider;
    private void Start()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
        SetRagdollCollidersEnabled(false);
        SetRagdollRigibbodiesKinematic(true);
   
        EventHub.GameOvered += ActivateRagdoll;
    }

    private void SetRagdollCollidersEnabled(bool enabled)
    {
        foreach (Collider i in _ragdollColliders)
        {
            i.enabled = enabled;
        }
    }

    private void SetRagdollRigibbodiesKinematic(bool kinematic)
    {
        foreach (Rigidbody i in _ragdollRigidbodies)
        {
            i.isKinematic = kinematic;
        }
    }

    private void ActivateRagdoll()
    {
        animator.enabled = false;
        charBoxCollider.enabled = false;
        charCupsuleCollider.enabled = false;
        SetRagdollCollidersEnabled(true); 
        SetRagdollRigibbodiesKinematic(false);

        Explode();
    }

    private void Explode()
    {
        //ragdollColliders = Physics.OverlapSphere(transform.position, 50f);

        foreach (var closeObjs in _ragdollRigidbodies)
        {
            //Rigidbody rigidbody = closeObjs.GetComponent<Rigidbody>();
            closeObjs.AddExplosionForce(1000f, transform.position, 1f);
            //float speed = 0.1f;
            //Vector3 impulse = new Vector3(0, 0, 1);
            //closeObjs.AddForce(impulse * speed, ForceMode.Impulse); //AddForceAtPosition(-Vector3.forward, transform.position, ForceMode.Force); //AddForce(-Vector3.forward, ForceMode.Impulse);//AddExplosionForce(1000f, transform.position, 1f);
        }
    }

    private void OnDisable()
    {
        EventHub.GameOvered -= ActivateRagdoll;
    }
}

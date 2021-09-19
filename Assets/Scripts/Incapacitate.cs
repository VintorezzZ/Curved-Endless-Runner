using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incapacitate : MonoBehaviour
{    
    private Rigidbody[] ragdollRigidbodies;    
    private Collider[] ragdollColliders;
    public Animator animator;
    public CharacterController charBoxCollider;
    public Collider charCupsuleCollider;
    private void Awake()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();   
        SetRagdollCollidersEnabled(false);
        SetRagdollRigibbodiesKinematic(true);
    }

    private void Update()
    {
        ActivateRagdoll();
    }
    public void SetRagdollCollidersEnabled(bool enabled)
    {
        foreach (Collider i in ragdollColliders)
        {
            i.enabled = enabled;
        }
    }
    public void SetRagdollRigibbodiesKinematic(bool kinematic)
    {
        foreach (Rigidbody i in ragdollRigidbodies)
        {
            i.isKinematic = kinematic;
        }
    }

    public void ActivateRagdoll()
    {
        if(PlayerController.gameOver == true)
        {
            animator.enabled = false;
            charBoxCollider.enabled = false;
            charCupsuleCollider.enabled = false;

            SetRagdollCollidersEnabled(true);
            SetRagdollRigibbodiesKinematic(false);

            Explode();
        }
    }

    private void Explode()
    {
        //ragdollColliders = Physics.OverlapSphere(transform.position, 50f);

        foreach (var closeObjs in ragdollRigidbodies)
        {
            //Rigidbody rigidbody = closeObjs.GetComponent<Rigidbody>();
            closeObjs.AddExplosionForce(1000f, transform.position, 1f);
            //float speed = 0.1f;
            //Vector3 impulse = new Vector3(0, 0, 1);
            //closeObjs.AddForce(impulse * speed, ForceMode.Impulse); //AddForceAtPosition(-Vector3.forward, transform.position, ForceMode.Force); //AddForce(-Vector3.forward, ForceMode.Impulse);//AddExplosionForce(1000f, transform.position, 1f);
        }
    }

}

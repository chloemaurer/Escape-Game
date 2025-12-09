using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationCharacterControler : MonoBehaviour
{
    // --- Variables de Configuration ---

    [Header("Mouvement & Rotation")]
    public Rigidbody rb;
    public float Speed = 150f;
    public float RotationSpeed = 180f;
    public bool UsePhysics = true;

    [Header("Saut & Sol")]
    [Tooltip("Force verticale appliquée lors du saut")]
    public float JumpForce = 500f; // Force de saut à ajuster

    [Tooltip("LayerMask des objets considérés comme 'sol'")]
    public LayerMask GroundLayer;

    // --- Variables d'Input ---

    float forwardInput = 0f;
    float turnInput = 0f;
    bool sprintInput = false;
    bool jumpInput = false; // Input du saut

    // --- Méthode Start ---

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            // IMPORTANT : La gravité DOIT être activée pour un saut réaliste
            rb.useGravity = true;
        }
    }

    // --- Méthode Update (Gestion des Inputs) ---

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        // NOUVEAU : Enregistrement de l'input de saut
        // Utiliser GetKeyDown pour un saut par impulsion unique
        if (Input.GetKeyDown(KeyCode.Space))
            jumpInput = true;
    }

    // --- Méthode FixedUpdate (Gestion de la Physique) ---

    void FixedUpdate()
    {
        if (rb == null || !UsePhysics || rb.isKinematic) return; // Si isKinematic est vrai, cette méthode de mouvement n'est pas recommandée ici.

        float speedFactor = sprintInput ? 1.5f : 1f;
        float currentSpeed = Speed * speedFactor;

        // GESTION DU MOUVEMENT HORIZONTAL (Assumant que vous utilisez la vélocité pour la collision)
        float turnAngle = turnInput * RotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRot = Quaternion.Euler(0f, turnAngle, 0f);
        rb.MoveRotation(rb.rotation * deltaRot);

        Vector3 desiredHorizontalVelocity = transform.forward * forwardInput * currentSpeed * Time.fixedDeltaTime;
        rb.linearVelocity = new Vector3(desiredHorizontalVelocity.x, rb.linearVelocity.y, desiredHorizontalVelocity.z);

        // ----------------------------------------
        // 3. GESTION DU SAUT (La partie essentielle)
        // ----------------------------------------
        if (jumpInput && IsGrounded())
        {
            // Réinitialiser la vélocité verticale (utile pour éviter les forces résiduelles)
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // Appliquer une force instantanée vers le haut
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        // Réinitialiser l'input après la tentative de saut
        jumpInput = false;
    }

    // --- Méthode IsGrounded (Vérification au Sol) ---

    bool IsGrounded()
    {
        Collider characterCollider = GetComponent<Collider>();
        if (characterCollider == null) return true;

        // Position de départ du SphereCast (légèrement au centre de la base)
        Vector3 sphereOrigin = transform.position + Vector3.up * 0.1f;

        // Rayon de la sphère de vérification (légèrement plus petit que le collider)
        float radius = characterCollider.bounds.extents.x * 0.9f;

        // Distance maximale pour détecter le sol
        float maxDistance = characterCollider.bounds.extents.y + 0.1f;

        // Utilisation d'un SphereCast, qui est plus fiable que Raycast pour vérifier le sol d'un personnage
        // On vérifie sous le personnage
        return Physics.SphereCast(sphereOrigin, radius, Vector3.down, out RaycastHit hit, maxDistance, GroundLayer, QueryTriggerInteraction.Ignore);
    }
}
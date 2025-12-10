using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationCharacterControler : MonoBehaviour
{
    // --- Variables de Configuration ---

    [Header("Mouvement & Rotation")]
    public Rigidbody rb;
    [Tooltip("Vitesse en mètres par seconde (Réduire de 150 à ~5-10 après correction!)")]
    // 🚨 ATTENTION : Changez cette valeur dans l'Inspecteur (ex: 7f)
    public float Speed = 7f;
    public float RotationSpeed = 180f;
    public bool UsePhysics = true;

    [Header("Saut & Sol")]
    [Tooltip("Force verticale appliquée lors du saut")]
    public float JumpForce = 500f;

    [Tooltip("LayerMask des objets considérés comme 'sol'")]
    public LayerMask GroundLayer;

    // --- Variables d'Input ---

    float forwardInput = 0f;
    float turnInput = 0f;
    bool sprintInput = false;
    bool jumpInput = false;

    // --- Méthode Start ---

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rb.useGravity = true;
        }
    }

    // --- Méthode Update (Gestion des Inputs) ---

    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space))
            jumpInput = true;
    }

    // --- Méthode FixedUpdate (Gestion de la Physique) ---

    void FixedUpdate()
    {
        if (rb == null || !UsePhysics || rb.isKinematic) return;

        float speedFactor = sprintInput ? 1.5f : 1f;
        float currentSpeed = Speed * speedFactor;

        // 1. ROTATION (Méthode correcte pour un Rigidbody)
        float turnAngle = turnInput * RotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRot = Quaternion.Euler(0f, turnAngle, 0f);
        rb.MoveRotation(rb.rotation * deltaRot);

        // 2. MOUVEMENT HORIZONTAL (CORRIGÉ : Pas de multiplication par Time.fixedDeltaTime sur la vélocité)
        // On définit la vélocité désirée en mètres/seconde
        Vector3 desiredHorizontalVelocity = transform.forward * forwardInput * currentSpeed;

        // On applique cette vélocité, en conservant la composante verticale actuelle
        rb.linearVelocity = new Vector3(desiredHorizontalVelocity.x, rb.linearVelocity.y, desiredHorizontalVelocity.z);

        // 3. GESTION DU SAUT
        if (jumpInput && IsGrounded())
        {
            // Réinitialiser la vélocité verticale avant d'ajouter l'impulsion
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // Appliquer la force
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        jumpInput = false;
    }

    // --- Méthode IsGrounded (Vérification au Sol) ---

    bool IsGrounded()
    {
        Collider characterCollider = GetComponent<Collider>();
        if (characterCollider == null) return true;

        Vector3 sphereOrigin = transform.position + Vector3.up * 0.1f;
        float radius = characterCollider.bounds.extents.x * 0.9f;
        float maxDistance = characterCollider.bounds.extents.y + 0.1f;

        return Physics.SphereCast(sphereOrigin, radius, Vector3.down, out RaycastHit hit, maxDistance, GroundLayer, QueryTriggerInteraction.Ignore);
    }
}
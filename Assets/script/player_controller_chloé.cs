using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationCharacterControler : MonoBehaviour
{
    // --- Variables de configuration ---
    [SerializeField] private Rigidbody rb;                 // Rigidbody du personnage
    [SerializeField] private float Speed = 7f;             // vitesse de déplacement en m/s
    [SerializeField] private float RotationSpeed = 180f;   // vitesse de rotation en degrés/s
    [SerializeField] private bool UsePhysics = true;       // utiliser la physique pour le mouvement

    [SerializeField] private float JumpForce = 500f;       // force appliquée lors du saut
    [SerializeField] private LayerMask GroundLayer;        // couche considérée comme sol

    // --- Variables d'input ---
    private float forwardInput = 0f;
    private float turnInput = 0f;
    private bool sprintInput = false;
    private bool jumpInput = false;

    // --- Start ---
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

    // --- Update : lecture des inputs ---
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        sprintInput = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space))
            jumpInput = true;
    }

    // --- FixedUpdate : gestion du mouvement physique ---
    void FixedUpdate()
    {
        if (rb == null || !UsePhysics || rb.isKinematic) return;

        float speedFactor = sprintInput ? 1.5f : 1f;
        float currentSpeed = Speed * speedFactor;

        // 1. Rotation
        float turnAngle = turnInput * RotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRot = Quaternion.Euler(0f, turnAngle, 0f);
        rb.MoveRotation(rb.rotation * deltaRot);

        // 2. Mouvement horizontal
        Vector3 desiredHorizontalVelocity = transform.forward * forwardInput * currentSpeed;
        rb.linearVelocity = new Vector3(desiredHorizontalVelocity.x, rb.linearVelocity.y, desiredHorizontalVelocity.z);

        // 3. Gestion du saut
        if (jumpInput && IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }

        jumpInput = false;
    }

    // --- Vérification si le personnage est au sol ---
    private bool IsGrounded()
    {
        Collider characterCollider = GetComponent<Collider>();
        if (characterCollider == null) return true;

        Vector3 sphereOrigin = transform.position + Vector3.up * 0.1f;
        float radius = characterCollider.bounds.extents.x * 0.9f;
        float maxDistance = characterCollider.bounds.extents.y + 0.1f;

        return Physics.SphereCast(sphereOrigin, radius, Vector3.down, out RaycastHit hit, maxDistance, GroundLayer, QueryTriggerInteraction.Ignore);
    }
}

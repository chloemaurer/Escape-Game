    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;


    /// <summary>
    /// Classe pour la navigation avec le CharacterController en 2D (déplacement uniquement)
    /// </summary>
    public class NavigationCharacterControler : MonoBehaviour
    {
        [Tooltip("Fraction of the screen"), Range(0, 1)]
        public float ScreenInteractionFraction = 0.1f;

        public Rigidbody rb;
        public float Speed = 5f;
        public float RotationSpeed = 100f;

        void Start()
        {
            if (rb == null)
                rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Vector3 movementInput = Vector3.zero;

            // Récupérer les inputs et les convertir en espace local
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movementInput += Vector3.down;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movementInput += Vector3.up;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movementInput += Vector3.left;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movementInput += Vector3.right;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                movementInput = Vector3.forward * Speed;
            }

            // Déplacement
            if (movementInput != Vector3.zero)
            {
                Vector3 movementDirection = transform.TransformDirection(movementInput.normalized);
                rb.MovePosition(transform.position + movementDirection * Speed * Time.deltaTime);
            }

            // Rotation avec la souris
            if (Input.mousePosition.x < Screen.width * ScreenInteractionFraction)
            {
                transform.Rotate(Vector3.forward, -RotationSpeed * Time.deltaTime);
            }
            else if (Input.mousePosition.x > Screen.width * (1 - ScreenInteractionFraction))
            {
                transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
            }
        }
    }

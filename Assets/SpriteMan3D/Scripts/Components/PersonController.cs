using UnityEngine;

namespace SpriteMan3D
{
    /// <summary>
    /// The character controller used to update an Animator.
    /// </summary>
    public class PersonController : MonoBehaviour
    {
        /// <summary>
        /// The sprite manager this controller updates.
        /// </summary>
        public Animator modelAnimator;

        /// <summary>
        /// Gets or sets whether an attack started in the current frame.
        /// </summary>
        private bool attackStarted;
        public bool AttackStarted
        {
            get
            {
                return attackStarted;
            }
            set
            {
                attackStarted = value;
                modelAnimator.SetBool("AttackStarted", value);
            }
        }
        /// <summary>
        /// Gets or sets whether this character is grounded in the current frame.
        /// </summary>
        private bool isGrounded;
        public bool IsGrounded
        {
            get
            {
                return isGrounded;
            }
            set
            {
                isGrounded = value;
                modelAnimator.SetBool("IsGrounded", value);
            }
        }
        /// <summary>
        /// Gets or sets whether a character is moving in the current frame.
        /// </summary>
        private bool isMoving;
        public bool IsMoving
        {
            get
            {
                return isMoving;
            }
            set
            {
                isMoving = value;
                modelAnimator.SetBool("IsMoving", value);
            }
        }
        /// <summary>
        /// Gets or sets whether a character is running in the current frame.
        /// </summary>
        private bool isRunning;
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                modelAnimator.SetBool("IsRunning", value);
            }
        }
        /// <summary>
        /// Gets or sets whether a jump started in the current frame.
        /// </summary>
        private bool jumpStarted;
        public bool JumpStarted
        {
            get
            {
                return jumpStarted;
            }
            set
            {
                jumpStarted = value;
                modelAnimator.SetBool("JumpStarted", value);
            }
        }
    }
}

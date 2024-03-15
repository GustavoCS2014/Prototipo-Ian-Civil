﻿using System;
using Attributes;
using Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Utilities;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseEntityController<T> : MonoBehaviour where T : BaseEntityController<T>
    {
        [SerializeField, ReadOnly] protected string currentState;
        [SerializeField] protected BaseEntitySettings settings;
        [SerializeField] protected HurtBox hurtBox;
        [SerializeField] protected LayerMask stairsMask;
        [SerializeField] protected bool showDebug;

        const float GRAVITY = -9.807f;
        private float _direction;
        private float _lastGrounded;

        protected StateMachine<T> StateMachine { get; private set; }

        public Rigidbody2D Rigidbody { get; private set;  }

        protected virtual void Awake()
        {
            StateMachine = new StateMachine<T>();
            StateMachine.StateChanged += OnStateChanged;

            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public float Direction
        {
            get => _direction;
            set
            {
                Vector3 localScale = transform.localScale;

                value = value.Clamp(-1f, 1f);

                if (value != 0f)
                {
                    transform.localScale = new Vector3(
                        Mathf.Sign(value) * Mathf.Abs(localScale.x),
                        localScale.y,
                        localScale.z
                    );
                }

                _direction = value;
            }
        }

        public Vector2 FacingDirection => transform.localScale.x * Vector2.right;

        public bool Grounded => Physics2D.OverlapCircle(
            Rigidbody.position,
            settings.GroundCheckRadius,
            settings.GroundLayer
        );

        public bool IsOnCoyoteTime => _lastGrounded < settings.CoyoteTime;

        private void OnStateChanged(State<T> state) => currentState = state.ToString();

        protected virtual void Update() {
            StateMachine?.CurrentState?.Update();
            
            _lastGrounded = !Grounded ? _lastGrounded + Time.deltaTime : 0;
            VerifyDirectionVector();
        }

        private void FixedUpdate() => StateMachine?.CurrentState?.FixedUpdate();

        protected virtual void OnDestroy()
        {
            if (StateMachine is null) return;
            StateMachine.Kill();
            StateMachine.StateChanged -= OnStateChanged;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            if (settings)
                Gizmos.DrawWireSphere(transform.position, settings.GroundCheckRadius);
        }

        #region Rigidbody

        public Vector2 Velocity
        {
            get => Rigidbody.velocity;
            set => Rigidbody.velocity = value;
        }

        public void MovePosition(Vector2 position) => Rigidbody.MovePosition(position);

        public void SetPosition(Vector2 position) => transform.position = position;

        public void AddForce(Vector2 force, ForceMode2D mode) => Rigidbody.AddForce(force, mode);
    
        /// <summary>
        /// Checks whether the player is standing on flat ground or a slope.
        /// </summary>
        /// <returns>Vector2, multiply this vector two with the input direction and speed to move.</returns>
        public Vector2 VerifyDirectionVector(){
            Vector2 playerTransform = new Vector2(transform.position.x, transform.position.y + .2f);
            float offset = .25f;
            float downStairsTransitionSlowdown = .5f;

            //? Getting the moving direction of 3 points.
            Vector2 vectorFront = GetDirectionVectorFromPosition(playerTransform + Vector2.right * offset);
            Vector2 vectorFeet = GetDirectionVectorFromPosition(playerTransform);
            Vector2 vectorBack = GetDirectionVectorFromPosition(playerTransform + Vector2.left * offset);

            bool allVectorsAreEqual = vectorFront.normalized == vectorFeet.normalized && 
                                        vectorFeet.normalized == vectorBack.normalized;

            //? checks if the player is at the start or end of some slope and makes it
            //? so the player don't get stuck on the edges.
            if(!allVectorsAreEqual && vectorFeet != Vector2.right){
                Rigidbody.sharedMaterial = settings.SlipperyMaterial;
                //? if the player is going up.
                if(Rigidbody.velocity.y > 0){
                    return vectorFront;
                }
                return vectorFront * downStairsTransitionSlowdown;
            }

            //? checks if the player is in the stairs, if so, corrects movement vector 
            //? and increases grip so the player doesn't slide.
            if(allVectorsAreEqual && vectorFeet != Vector2.right){
                Rigidbody.sharedMaterial = settings.GripMaterial;
                return vectorFront;
            }
            //? if the player is standing on flat ground return default settings.
            Rigidbody.sharedMaterial = settings.SlipperyMaterial;
            return Vector2.right;
        }

        /// <summary>
        /// Casts a ray down and calculates the direction vector from the normal of the surface.
        /// </summary>
        /// <param name="startPosition">Point from which the ray is cast.</param>
        /// <returns>The direction vector.</returns>
        public Vector2 GetDirectionVectorFromPosition(Vector2 startPosition){
            float rayLenght = .75f;
            RaycastHit2D hit2D = Physics2D.Raycast(startPosition, Vector2.down, rayLenght, settings.GroundLayer);
            if(showDebug) Debug.DrawRay(startPosition, Vector2.down * rayLenght, Color.red);
            if(hit2D){
                Vector2 upStairsDirection = GetPerpendicularVector(hit2D.normal, true);
                if(showDebug) Debug.DrawRay(startPosition, upStairsDirection, Color.blue);
                return upStairsDirection.normalized;
            }
            return Vector2.right;
        }        
        /// <summary>
        /// Get's a vector perpendicular to the inputVector and returns it.
        /// useful for getting the direction of a surface from the normal.
        /// </summary>
        /// <param name="inputVector">The vector from wich you want to know the perpendicular one.</param>
        /// <param name="inverted">inverts the direction the vector is facing.</param>
        /// <returns></returns>
        public Vector2 GetPerpendicularVector(Vector2 inputVector, bool inverted){
            return inverted ?
                new Vector2(inputVector.y, -inputVector.x) :
                new Vector2(-inputVector.y, inputVector.x);
        }
        #endregion
    }
}

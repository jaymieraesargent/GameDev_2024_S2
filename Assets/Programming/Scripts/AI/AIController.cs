using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class AIController : MonoBehaviour
    {
        public enum AIState
        {
            Idle,
            Patrol,
            Wander, 
            Stun,
            Attack,
            Chase
        }
        #region Variables 
        //the current state - going to need states???
        [SerializeField]private AIState _state = AIState.Idle;
        //Nav Mesh Agent
        [SerializeField]private NavMeshAgent _agent;
        //Animator
        [SerializeField] private Animator _animator;
        //walk speed and a run/chase speed
        [SerializeField] private float _walkSpeed = 2f, _runSpeed = 5;
        //patrolPoints/wayPoints []array of locations
        [SerializeField] private Transform[] _wayPoints;
        //iteration of array
        [SerializeField] private int _currentWayPointIndex = 0;
        // move randomly???
        [SerializeField] private Vector3 _randomPosition;
        //where are you player??
        [SerializeField] private float _detectionRadius = 10f;
        //who are you player??
        [SerializeField] private LayerMask _playerLayer;
        //keep chasing distance
        [SerializeField] private float _chaseDistance = 20f;
        //attack distance
        [SerializeField] private float _attackDistance = 2f;
        #endregion
        #region Unity Event Functions
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _state = AIState.Idle;
            TransitionToState(_state);

        }
        private void Update()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectionRadius, _playerLayer);
            if(hitColliders.Length > 0 )
            {
                TransitionToState(AIState.Chase);
            }
            //TransitionToState(_state);
            switch (_state)
            {
                case AIState.Idle:
                    StartCoroutine(Idle());
                    break;
                case AIState.Patrol:
                    Patrol();
                    break;
                case AIState.Wander:
                    Wander();
                    break;
                case AIState.Stun:
                    Stun();
                    break;
                case AIState.Attack:
                    Attack();
                    break;
                case AIState.Chase:
                    Chase();
                    break;
              
            }
        }
        #endregion

        #region States
        void TransitionToState(AIState newState)
        {
            _state = newState;
            switch (_state)
            {
                case AIState.Idle:
                   StartCoroutine(Idle());
                    break;
                case AIState.Patrol:
                    Patrol();
                    break;
                case AIState.Wander:
                    Wander();
                    break;
                case AIState.Stun:
                    Stun();
                    break;
                case AIState.Attack:
                    Attack();
                    break;
                case AIState.Chase:
                    Chase();
                    break;
               
            }
        }
        IEnumerator Idle()
        {
            PlayAnim("Idle");
            yield return new WaitForSeconds(Random.Range(3, 10f));
            if (_state == AIState.Idle)
            {
                int choice = Random.Range(0, 2);
                if (choice == 0)
                {
                    _randomPosition = GetRandomPosition();
                    TransitionToState(AIState.Wander);
                }
                else
                {
                    TransitionToState(AIState.Patrol);
                }
            }           
        }
        void Patrol()
        {
            _agent.speed = _walkSpeed;
            PlayAnim("Walk");
            if (_wayPoints.Length == 0)
            {
                Debug.Log("No patrol waypoints assigned!");
                TransitionToState(AIState.Idle);
            }
            if(!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
            {
                int choice = Random.Range(0, 2);
                if (choice == 0)
                {
                    TransitionToState(AIState.Idle);
                }
                else
                {
                    TransitionToNextWayPoint();
                }
            }
        }
        void TransitionToNextWayPoint()
        {
            _currentWayPointIndex = (_currentWayPointIndex +1) % _wayPoints.Length;
            _agent.SetDestination(_wayPoints[_currentWayPointIndex].position);
            _agent.speed = _walkSpeed;
            PlayAnim("Walk");
        }
        void Wander()
        {
            _agent.SetDestination(_randomPosition);
            _agent.speed = _walkSpeed;
            PlayAnim("Walk");
            if (_agent.remainingDistance <= 0.1f)
            {
                int choice = Random.Range(0, 2);
                if (choice == 0)
                {
                    _randomPosition = GetRandomPosition();
                    TransitionToState(AIState.Wander);
                }
                else
                {
                    TransitionToState(AIState.Idle);
                }
            }
        }
        Vector3 GetRandomPosition()
        {
            Vector3 finalPosition = Vector3.zero;
            Vector3 randomDirection = Random.insideUnitSphere * 10;
            randomDirection += transform.position;
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomDirection, out hit, 10,1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }
        void Chase()
        {
            if (!_agent.pathPending && _agent.remainingDistance <= _chaseDistance)
            {
                PlayAnim("Run");
                _agent.speed = _runSpeed;
                _agent.SetDestination(GetPlayerPosition());
            }
            else
            {
                _randomPosition = GetRandomPosition();
                TransitionToState(AIState.Wander);
            }
        }
        void Stun()
        {

        }
        void Attack()
        {

        }
        #endregion
        void PlayAnim(string trigger)
        {
            _animator.SetTrigger(trigger);
        }
        private Vector3 GetPlayerPosition()
        {
            return GameObject.FindWithTag("Player").transform.position;
        }
    }
}


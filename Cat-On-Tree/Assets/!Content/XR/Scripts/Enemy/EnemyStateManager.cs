using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [SerializeField] public Animator animator;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] Transform player;
    //[SerializeField] Transform target;

    public AState currentState;
    public IdleState idleState = new IdleState();
    public WalkState walkState = new WalkState();
    public AttackState attackState = new AttackState();
    //public DeadState deadState = new DeadState();

    public float walkSpeed;
    public float agroDistance;
    public float attackDistance;

    public void SwitchState(AState state)
    {
        if (currentState != null)
            currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SwitchState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            player = FindAnyObjectByType<XROrigin>().transform;

        agent.SetDestination(player.position);
        agent.destination = player.position;
        if (currentState != null)
            currentState.UpdateState(this);
    }


    public void SetSpeed(float newSpeed)
    {
        agent.speed = newSpeed;
    }
    //public void SetDistination(Transform newDestination)
    //{
    //    target = newDestination;
    //}

    public Vector3 CheckOnTargetRotation()
    {
        return (player.position - transform.position);
    }

    public void RotateTowards(Vector3 direction) 
    {
        var q = Quaternion.LookRotation(CheckOnTargetRotation());
        transform.rotation = Quaternion.Lerp(transform.rotation, q, 3f * Time.deltaTime);
    }

    public float CheckOnTarget()
    {
        return (transform.position - player.position).magnitude;
    }
   
    
    }




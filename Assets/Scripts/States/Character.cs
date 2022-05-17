using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    // Properties
    public bool IsSheperdClose => m_isSheperdClose;
    public bool IsInFence => m_isInFenceZone;
    public bool IsStarving => m_isStarving;
    public bool IsSleeping
    { 
        get { return m_isSleeping; }
        set { m_isSleeping = value; } 
    }
    public float StomachPoints 
    { 
        get { return m_stomachPoints; } 
        set { m_stomachPoints = value; } 
    }
    public float TiredPoints 
    { 
        set { m_tiredPoints = value; }
    }


    // Variables
    private bool m_isSheperdClose = false; // FollowState
    private bool m_isInFenceZone = false; // FenceZoneState
    private bool m_isSleeping = false; // SleepState
    private bool m_isStarving = false; // HungryState

    [SerializeField]
    public GameObject m_Shepherd;

    [SerializeField]
    private float m_hungerPerSecond = 0.2f;
    [SerializeField]
    private float m_stomachPoints;
    [SerializeField]
    private float m_tiredPerSecond = 1f;
    [SerializeField]
    private float m_tiredPoints;

    // Der Path-selbst der vom Agent benutzt werden soll
    public Path m_Path;
    public NavMeshAgent m_NavMeshAgent;
    public FiniteStateMachine m_Fsm;
    public MeshRenderer m_MeshRenderer;

    private void Awake()
    {
        m_stomachPoints = Random.Range(60f, 100f);
        m_tiredPoints = Random.Range(60f, 100f);
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_MeshRenderer = GetComponentInChildren<MeshRenderer>();
        m_Fsm = new FiniteStateMachine(this, E_States.WALKSTATE);

        m_Shepherd = GameObject.Find("ShepherdAgent");
    }

    private void Start()
    {
        // per Singleton den Path zuweisen der angesteuret werden soll
        m_Path = PathDatabase.m_instance.GetPathByID(0);
    }

    void Update()
    {
        m_Fsm.Update();
        GetHungry();
        GetTired();
    }

    public void OnTriggerEnter(Collider _other)
    {
        // Shepherd
        if (_other.CompareTag("Shepherd"))
        {
            Debug.Log("OnTriggerEnter: Shepherd");
            m_isSheperdClose = true;
        }

        // FenceZone
        if (_other.CompareTag("FenceZone"))
        {
            Debug.Log("OntriggerEnter: FenceZone");
            m_isInFenceZone = true;
        }
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.CompareTag("Shepherd"))
        {
            Debug.Log("OnTriggerStay: Shepherd");
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        // Shepherd
        if (_other.CompareTag("Shepherd"))
        {
            Debug.Log("OnTriggerExit: Shepherd");
            m_isSheperdClose = false;
        }

        // FenceZone
        if (_other.CompareTag("FenceZone"))
        {
            Debug.Log("OntriggerExit: FenceZone");
            m_isInFenceZone = false;
        }
    }

    public void GetHungry()
    {
        m_stomachPoints -= m_hungerPerSecond * Time.deltaTime;

        if (m_stomachPoints <= 0)
        {
            Destroy(this.gameObject);
        }

        if (m_stomachPoints >= 50)
        {
            m_isStarving = false;
        }
        else if (m_stomachPoints < 50)
        {
            m_isStarving = true;
        }
    }

    public void GetTired()
    {
        m_tiredPoints -= m_tiredPerSecond * Time.deltaTime;

        if (m_tiredPoints <= 0)
        {
            m_isSleeping = true;
        }
    }
}

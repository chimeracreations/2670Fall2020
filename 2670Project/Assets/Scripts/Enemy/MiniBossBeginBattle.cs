using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.UI;


public class MiniBossBeginBattle : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera c_VirtualCamera;
    public GameObject lookHere;
    public PlayerData player;
    private Animator animator;
    private WaitForSeconds wfs;
    [SerializeField] private UnityEvent callEvent1, callEvent2, callEvent3, callEvent4, callEvent5, callEvent6, callEvent7;
    private NavMeshAgent agent;
    private GameObject character;
    private float huntSpeed;
    private bool startMovement = false;
    public float bossHealth = 3f;
    public GameObject theEnd;
    private bool stopEnd;
    private bool startOnce;
    public GameObject lights;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        character = GameObject.FindWithTag("Player");
        huntSpeed = 4f;
        theEnd.SetActive(false);
        stopEnd = false;
        startOnce = false;
        lights.SetActive(false);
    }

    private void Update() 
    {
        if (startMovement == true)
        {
            agent.speed = huntSpeed;
            agent.destination = character.transform.position;
        }
    }

    public void StartScene(float time)
    {
        StartCoroutine(MiniBossScene(time));
    }

    private IEnumerator MiniBossScene(float time)
    {
        if (!startOnce)
        {
            lights.SetActive(true);
            startOnce = true;
            c_VirtualCamera.m_LookAt = lookHere.transform;
            player.canControl = false;
            animator.SetTrigger("startBattle");
            yield return wfs = new WaitForSeconds(time);
            callEvent1.Invoke();
            yield return wfs = new WaitForSeconds(time);
            callEvent2.Invoke();
            yield return wfs = new WaitForSeconds(time);
            yield return wfs = new WaitForSeconds(time);
            callEvent6.Invoke();
            c_VirtualCamera.m_LookAt = character.transform;
            c_VirtualCamera.m_Follow = character.transform;
            player.canControl = true;
            startMovement = true;
            lights.SetActive(false);
        }
    }

    public void takeDamage()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        startMovement = false;
        StartCoroutine(Damaged(4f));
    }

    private IEnumerator Damaged(float time)
    {
        startMovement = false;
        animator.SetTrigger("Damaged");
        bossHealth -= 1f;
        yield return wfs = new WaitForSeconds(time);
        if (bossHealth <= 0f && stopEnd == false)
        {
            StartCoroutine(MiniBossEndScene(3f));
        }
        else
        {
            callEvent3.Invoke();
            Upgrade();
            startMovement = true;
            agent.isStopped = false;
        }
    }

    public void Upgrade()
    {
        huntSpeed = huntSpeed + 1.1f;
        gameObject.transform.localScale += new Vector3(1f,1f,1f);
    }

    public IEnumerator MiniBossEndScene(float time)
    {
        lights.SetActive(true);
        c_VirtualCamera.m_LookAt = lookHere.transform;
        callEvent7?.Invoke();
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        stopEnd = true;
        player.canControl = false;
        animator.SetTrigger("endBattle");
        yield return wfs = new WaitForSeconds(time);
        callEvent4.Invoke();
        yield return wfs = new WaitForSeconds(time);
        callEvent5.Invoke();
        yield return wfs = new WaitForSeconds(time);
        yield return wfs = new WaitForSeconds(time);
        theEnd.SetActive(true);
        Time.timeScale = 0;
    }

}

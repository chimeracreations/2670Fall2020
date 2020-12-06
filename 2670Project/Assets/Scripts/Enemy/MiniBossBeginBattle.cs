using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class MiniBossBeginBattle : MonoBehaviour
{
    public PlayerData player;
    private Animator animator;
    private WaitForSeconds wfs;
    [SerializeField] private UnityEvent callEvent1, callEvent2, callEvent3;
    private NavMeshAgent agent;
    private GameObject character;
    private float huntSpeed;
    private bool startMovement = false;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        character = GameObject.FindWithTag("Player");
        huntSpeed = 6f;
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
        player.canControl = false;
        animator.SetTrigger("startBattle");
        yield return wfs = new WaitForSeconds(time);
        callEvent1.Invoke();
        yield return wfs = new WaitForSeconds(time);
        callEvent2.Invoke();
        yield return wfs = new WaitForSeconds(time);
        yield return wfs = new WaitForSeconds(time);
        player.canControl = true;
        startMovement = true;
    }

    public void takeDamage()
    {
        startMovement = false;
        StartCoroutine(Damaged(4f));
    }

    private IEnumerator Damaged(float time)
    {
        animator.SetTrigger("Damaged");
        yield return wfs = new WaitForSeconds(time);
        callEvent3.Invoke();
        Upgrade();
        startMovement = true;
    }

    public void Upgrade()
    {
        huntSpeed = huntSpeed + 1f;
        gameObject.transform.localScale += new Vector3(0.3f,0.3f,0.3f);
    }

}

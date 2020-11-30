using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MiniBossStartScene : MonoBehaviour
{
    public PlayerData player;
    private Animator animator;
    private WaitForSeconds wfs;
    public IntData torchStart, torchUnder;
    [SerializeField] private UnityEvent callDialogue1, callDialogue2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        torchStart.value = 0;
        torchUnder.value = 0;
    }


    public void StartScene(float time)
    {
        StartCoroutine(MiniBossScene(time));
    }

    private IEnumerator MiniBossScene(float time)
    {
        player.canControl = false;
        animator.SetTrigger("startScene");
        yield return wfs = new WaitForSeconds(time);
        callDialogue1.Invoke();
        yield return wfs = new WaitForSeconds(time);
        callDialogue2.Invoke();
        yield return wfs = new WaitForSeconds(time);
        yield return wfs = new WaitForSeconds(time);
        player.canControl = true;
        Destroy(gameObject);
    }
}

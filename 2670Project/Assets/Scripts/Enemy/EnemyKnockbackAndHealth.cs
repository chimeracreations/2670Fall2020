using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackAndHealth : MonoBehaviour
{
    public GameObject enemy;
    private GameObject player;
    private UnityEngine.AI.NavMeshAgent agent;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public float knockbackDuration = 0.25f;
    private Vector3 pushDirection;
    public float knockbackSpeed = 15f; 
    public float maxHealth;
    private float health;
    private Color color;
    private Color alphaColor;
    private Color betaColor;
    private Renderer[] colorAll;
    public bool revealed;
    public EnemyData data;
    public GameObject heart;
    public GameObject bean;
    public Vector3 offset;


    private void Start() 
    {
        agent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        colorAll = GetComponentsInChildren<Renderer>();
        color = colorAll[1].material.color;
        alphaColor = gameObject.GetComponent<MeshRenderer>().material.color;
        betaColor = gameObject.GetComponent<MeshRenderer>().material.color;
        betaColor.a = 0.75f;
    }

    private void OnEnable()
    {
        revealed = false;
        health = data.enemyMaxHeath;
    }

    private void Update() 
    {
        if (!revealed)
        {
            for (int x = 0; x <= transform.childCount; x++)
            {
                colorAll[x].material.color = Color.Lerp(enemy.GetComponent<MeshRenderer>().material.color, alphaColor, 1 * Time.deltaTime);
            }
        } 
    }

    private IEnumerator OnTriggerEnter(Collider other) 
    {
        if (other.tag == "TailStink" || other.tag == "Bomb")
        {
            agent = transform.parent.GetComponent<UnityEngine.AI.NavMeshAgent>();
            pushDirection = new Vector3(0,0,-1);
            pushDirection = player.transform.position - transform.parent.position;
            pushDirection =- pushDirection.normalized;
            pushDirection.y = 0;
            float i = 0; 


            while (i <= knockbackDuration)
            {
                yield return wffu;
                i += (1f * Time.deltaTime);
                agent.Move(pushDirection * Time.deltaTime * knockbackSpeed);
                if ((i > .01f && i < .2f))
                {
                    color.a = .4f;
                }
                else color.a = 0.75f;
                for (int x = 0; x <= transform.childCount; x++)
                    {
                        colorAll[x].material.color = color;
                    }
            }

             health -= 1f;

            if (health <= 0f)
            {
                enemy.SetActive(false);
                var dropRandom = Random.value;
                if (dropRandom >= 0.9f)
                {
                    Instantiate(heart, transform.position + offset, transform.rotation);
                }
                if (dropRandom <= 0.1f)
                {
                    Instantiate(bean, transform.position + offset, transform.rotation);
                }
            } 
        }
    }

    public void Reveal()
    {
        revealed = true;
        {
            StartCoroutine(MakeAppear());
        }
        if (revealed == false) 
        {
            StopCoroutine(MakeAppear());    
        }

    }

    public IEnumerator MakeAppear()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.Lerp(GetComponent<MeshRenderer>().material.color, betaColor, 1 * Time.deltaTime);
        for (int x = 0; x <= transform.childCount; x++)
        {
            colorAll[x].material.color = Color.Lerp(enemy.GetComponent<MeshRenderer>().material.color, betaColor, 1 * Time.deltaTime);
        }
        yield return wffu;
    }

    public void Hide()
    {
        revealed = false;
    }
}
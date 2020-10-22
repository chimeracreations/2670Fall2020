using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private WaitForSeconds wfs = new WaitForSeconds(1f);
    private Renderer bombColor;
    private Color color;
    private Color colorSave;
    private Collider bombCollider;
    public float i = 0;
    // Start is called before the first frame update
    private void OnEnable() 
    {
        bombColor = GetComponent<Renderer>();
        color = bombColor.material.color;
        colorSave = bombColor.material.color;
        bombCollider = GetComponent<Collider>();
        bombCollider.enabled = false;

        StartCoroutine(OnEnableCoroutine());
    }

    IEnumerator OnEnableCoroutine() 
    {
        while (i <= 4)
        {
            yield return wfs;
            i++;
            //transform.Translate(0,-1f,0);
            if (i == 1 || i == 3)
            {
                color = Color.red;
                color.a = .2f;
            }
            else if (i == 4)
            {
                color = Color.red;
                color.a = .001f;
                gameObject.transform.localScale = gameObject.transform.localScale * 5f;
            }
            else 
            {   
                color = colorSave;
                color.a = 1f;
            }
            
            bombColor.material.color = color;
            if (i == 4) bombCollider.enabled = true;
            if (i == 5) Destroy(gameObject);
        }
    }
}

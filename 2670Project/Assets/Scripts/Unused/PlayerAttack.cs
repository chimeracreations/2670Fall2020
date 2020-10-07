// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerAttack : MonoBehaviour
// {
//     private Animator animator;
//     private float angle;

//     // Start is called before the first frame update
//     void Start()
//     {
//         animator = GetComponent<Animator>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         angle = gameObject.GetComponentInParent<CharacterMover>().deltaAngle;
//         if (Input.GetButtonDown("Fire1") && angle < 0)
//         {
//             animator.SetTrigger("playerAttack");
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalDataBinding : MonoBehaviour
{
    public Animator animator;
    public float Speed
    {
        set
        {
            animator.SetFloat(Anim_K_Speed, value);
        }
    }
    public bool Attack
    {
        set
        {
            if(value)
            {
                animator.SetTrigger(Anim_K_Attack);
            }
        }
    }
    public bool Dead
    {
        set
        {
            animator.Play("Dead", 0,0);
        }
    }
    public bool StartStates
    {
        set
        {
            animator.Play("Start", 0, 0);
        }
    }
    private int Anim_K_Speed;
    private int Anim_K_Attack;
    // Start is called before the first frame update
    void Start()
    {
        Anim_K_Attack = Animator.StringToHash("Attack");
        Anim_K_Speed = Animator.StringToHash("Speed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

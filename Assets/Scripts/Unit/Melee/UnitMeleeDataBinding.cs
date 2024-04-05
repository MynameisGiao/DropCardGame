using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMeleeDataBinding : MonoBehaviour
{
    public Animator animator;
    public float Speed
    {
        set
        {
            animator.SetFloat(Anim_K_Speed, value);
        }
    }
    public bool Dead
    {
        set
        {
            animator.Play("Dead", 0, 0);
        }
    }
    public bool Attack
    {
        set
        {
            int index = UnityEngine.Random.Range(1, 3); // lấy từ 1 -> 2
            animator.Play("Attack "+index.ToString(), 0, 0);
        }
    }
    private int Anim_K_Speed;
    void Start()
    {
        Anim_K_Speed = Animator.StringToHash("Speed");       
    }

}

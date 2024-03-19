using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcControl : EnemyControl
{
    public OrcDataBinding dataBinding;
    public Orc_AttackBaseState attackBaseState;
    public Orc_AttackUnitState attackUnitState;
    public Orc_StartState startState;
    public Orc_MoveState moveState;
    public Orc_DeadState deadState;
    public override void Setup(EnemyInitData enemyInitData)
    {
        base.Setup(enemyInitData);

        attackBaseState.parent = this;
        attackUnitState.parent = this;
        startState.parent = this;
        moveState.parent = this;
        deadState.parent = this;
        GotoState(startState);
        StartCoroutine("LoopDetectUnit");
    }
    IEnumerator LoopDetectUnit()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true)
        {
            yield return wait;
            if (cur_State == moveState)
            {
                Collider[] cols = Physics.OverlapSphere(trans_detect.position, range_detect, mask_unit);
                int index = -1;
                if (cols.Length == 1)
                {
                    index = 0;
                }

                // xác định unit có khoảng cách gần nhất
                float distance = 100f;
                for (int i = 0; i < cols.Length; i++)
                {
                    float dis = Vector3.Distance(trans.position, cols[i].transform.position);
                    if (dis < distance)
                    {
                        distance = dis;
                        index = i;
                    }
                }

                if (index != -1)
                {
                    GotoState(attackUnitState, cols[index].transform);
                }
            }

        }
    }
}
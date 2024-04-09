using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardDialog : BaseDialog
{
   public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }
}

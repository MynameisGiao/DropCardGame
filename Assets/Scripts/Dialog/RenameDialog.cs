using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RenameDialog : BaseDialog
{
    public TMP_InputField nicknameInput;

    public override void Setup(DialogParam param)
    {
        base.Setup(param);
    }
    void Start()
    {

        nicknameInput.text = DataController.instance.GetName();
    }

    public void OnSelect()
    {
        UpdateName();
        DialogManager.instance.HideDialog(dialogIndex);
    }
    public void UpdateName()
    {
        DataController.instance.UpdateName(nicknameInput.text);
    }

    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }
}

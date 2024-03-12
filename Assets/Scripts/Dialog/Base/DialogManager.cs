using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : BYSingletonMono<DialogManager>
{
    public RectTransform anchor_dl;
    private Dictionary<DialogIndex, BaseDialog> dic = new Dictionary<DialogIndex, BaseDialog>();
    private List<BaseDialog> ls_dialog = new List<BaseDialog>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (DialogIndex index in DialogConfig.dialogIndices)
        {
            string name_dialog = index.ToString();
            GameObject dl_obj = Instantiate(Resources.Load("Dialog/" + name_dialog, typeof(GameObject))) as GameObject;
            dl_obj.transform.SetParent(anchor_dl, false);

            BaseDialog dl = dl_obj.GetComponent<BaseDialog>();
            dic.Add(index, dl);
            dl_obj.SetActive(false);
        }
    }
    public void ShowDialog(DialogIndex index, DialogParam param = null, Action callback = null)
    {
        BaseDialog dl = dic[index];

        Action cb = () =>
        {
            callback?.Invoke();
        };
        dl.gameObject.SetActive(true);
        dl.GetComponent<RectTransform>().SetAsLastSibling();

        dl.Setup(param);
        dl.SendMessage("ShowDialog", (object)cb, SendMessageOptions.RequireReceiver);
        if (!ls_dialog.Contains(dl))
        {
            ls_dialog.Add(dl);
        }
    }


    public void HideDialog(DialogIndex index, Action callback = null)
    {
        BaseDialog dl = dic[index];

        Action cb = () =>
        {
            callback?.Invoke();
            dl.gameObject.SetActive(false);

        };
        dl.SendMessage("HideDialog", (object)cb, SendMessageOptions.RequireReceiver);
        if (ls_dialog.Contains(dl))
        {
            ls_dialog.Remove(dl);
        }
    }
    public void HideAllDialog()
    {
        foreach (BaseDialog dl in ls_dialog)
        {
            Action cb = () =>
            {
                dl.gameObject.SetActive(false);

            };
            dl.SendMessage("HideDialog", (object)cb, SendMessageOptions.RequireReceiver);
        }
        ls_dialog.Clear();
    }

    
}
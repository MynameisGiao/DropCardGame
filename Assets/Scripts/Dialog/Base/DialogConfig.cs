using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogIndex
{
    PauseDialog=1,
    WinDialog=2,
    FailDialog=3,
    SettingDialog=4
}
public class DialogParam
{

}
public class WinDialogParam:DialogParam
{
    public ConfigMissionRecord cf_mission;
}
public class SettingDialogParam : DialogParam
{
    public bool isShowPause;
}
public class DialogConfig
{
    public static DialogIndex[] dialogIndices =
    {
      DialogIndex.PauseDialog,
      DialogIndex.WinDialog,
      DialogIndex.FailDialog,
      DialogIndex.SettingDialog,
    };
}

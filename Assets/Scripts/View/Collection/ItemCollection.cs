using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public GameObject[] rare_objects_bg;
    public GameObject[] rare_objects_frame;
    public Image icon;
    public TMP_Text name_lb;
    public TMP_Text level_lb;
    public GameObject btn_info;
    public GameObject lock_object;
    private ConfigUnitRecord config_unit;

    public UnitData data;
    public void Setup(ConfigUnitRecord cf)
    {

        data = DataController.instance.GetUnitData(cf.ID);

        config_unit = cf;
        name_lb.text = config_unit.Name;
      
        for (int i = 0; i < rare_objects_bg.Length; i++)
        {
            rare_objects_bg[i].SetActive(i + 1 == (int)config_unit.Rare);
        }
        for (int i = 0; i < rare_objects_frame.Length; i++)
        {
            rare_objects_frame[i].SetActive(i + 1 == (int)config_unit.Rare);
        }
        icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_unit.Prefab);
        
        if (data != null)
        {
            ConfigUnitLevelRecord cf_level = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(cf.ID);
            if (data.level < cf_level.Maxlv)
                level_lb.text = "Level " + data.level.ToString();
            else
                level_lb.text = "Max level";

        }
        else
        {
            level_lb.text = " ";
        }
        lock_object.SetActive(data == null);
        

    }
    public void OnShowInfo()
    {
       
        InfoUnitDialogParam param = new InfoUnitDialogParam { cf_unit = config_unit };
        DialogManager.instance.ShowDialog(DialogIndex.InfoUnitDialog, param);

    }
}

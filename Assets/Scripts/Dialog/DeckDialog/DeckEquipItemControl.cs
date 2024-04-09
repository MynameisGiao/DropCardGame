using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckEquipItemControl : MonoBehaviour
{
    public GameObject[] rare_objects;
    public Image icon;
    public TMP_Text name_lb;
    public TMP_Text level_lb;
    public GameObject btn_select;

    private ConfigUnitRecord config_unit;
    private UnitData cur_UnitData;
    private int index;
    public void Setup(UnitData data, UnitData cur_UnitData, int index)
    {
        this.cur_UnitData = cur_UnitData;
        this.index = index;
        config_unit = ConfigManager.instance.configUnit.GetRecordByKeySearch(data.id);
        name_lb.text = config_unit.Name;
        data = DataController.instance.GetUnitData(data.id);
        ConfigUnitLevelRecord cf_level = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(data.id);
        if (data.level < cf_level.Maxlv)
            level_lb.text = "Lv " + data.level.ToString();
        else
            level_lb.text = "MAX LV ";
        for (int i = 0; i < rare_objects.Length; i++)
        {
            rare_objects[i].SetActive(i + 1 == (int)config_unit.Rare);
        }
        icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_unit.Prefab);

    }
    public void OnSelect()
    {
    
        DialogManager.instance.HideDialog(DialogIndex.DeckEquipDialog);
        DataController.instance.ChangeDeck(cur_UnitData, index);
    }
}

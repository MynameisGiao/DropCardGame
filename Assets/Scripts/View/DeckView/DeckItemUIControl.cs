using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckItemUIControl : MonoBehaviour
{
    public GameObject[] rare_objects;
    public GameObject[] rare_objects_frame;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text name_lb;
    [SerializeField] private TMP_Text level_lb;
    [SerializeField] private TMP_Text hp_lb;
    [SerializeField] private TMP_Text damage_lb;

    private ConfigUnitRecord config_unit;
    private ConfigUnitLevelRecord config_unit_level;
    private UnitData data;
    public void Setup(UnitData data_)
    {
        config_unit = ConfigManager.instance.configUnit.GetRecordByKeySearch(data_.id);
        config_unit_level = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(config_unit.ID);
        data = DataController.instance.GetUnitData(data_.id);
        name_lb.text = config_unit.Name;
        if (data.level < config_unit_level.Maxlv)
            level_lb.text = "Level: " + data.level.ToString();
        else
            level_lb.text = "Max Level";
        for (int i = 0; i < rare_objects.Length; i++)
        {
            rare_objects[i].SetActive(i + 1 == (int)config_unit.Rare);
        }
        for (int i = 0; i < rare_objects_frame.Length; i++)
        {
            rare_objects_frame[i].SetActive(i + 1 == (int)config_unit.Rare);
        }
        icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_unit.Prefab);
        hp_lb.text =  config_unit_level.GetHP(data.level).ToString();
        damage_lb.text = config_unit_level.GetDamage(data.level).ToString();
       
    }
   
    public void OnChangeCard()
    {
       DeckEquipDialogParam param = new DeckEquipDialogParam();
        param.unitData = data;
        DialogManager.instance.ShowDialog(DialogIndex.DeckEquipDialog, param);
    }
    

}

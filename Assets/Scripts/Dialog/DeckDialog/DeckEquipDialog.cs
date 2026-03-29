using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckEquipDialog : BaseDialog
{
    public GameObject[] rare_objects;
    public Image icon;
    public TMP_Text level_lb;
    public TMP_Text damage_lb;
    public TMP_Text hp_lb;
    public Transform parent_item;
    public DeckEquipItemControl prefab;
    public UnitData cur_UnitData;
    
    public override void Setup(DialogParam param)
    {
        DeckEquipDialogParam dl_param = (DeckEquipDialogParam)param;
        cur_UnitData = dl_param.unitData;
        
        // Get all unlocked units
        List<UnitData> units = DataController.instance.GetUnlockedUnitsExcludeDeck();
        // List<UnitData> allUnits = DataController.instance.GetUnlockedUnitsByRare((1));;
        SetupItems(units);
        
        // Setup unit info display
        ConfigUnitRecord config_unit = ConfigManager.instance.configUnit.GetRecordByKeySearch(cur_UnitData.id);

        ConfigUnitLevelRecord cf_unit_level = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(config_unit.ID);
        if (cur_UnitData.level < cf_unit_level.Maxlv)
            level_lb.text = "Level " + cur_UnitData.level.ToString();
        else
            level_lb.text = "MAX LEVEL ";

        hp_lb.text =  cf_unit_level.GetHP(cur_UnitData.level).ToString();
        damage_lb.text = cf_unit_level.GetDamage(cur_UnitData.level).ToString();
        
        for (int i = 0; i < rare_objects.Length; i++)
        {
            rare_objects[i].SetActive(i + 1 == (int)config_unit.Rare);
        }
        icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_unit.Prefab);
    }

    private void SetupItems(List<UnitData> units)
    {
        // Xóa các items cũ
        foreach (Transform child in parent_item)
        {
            Destroy(child.gameObject);
        }

        // Tạo items từ danh sách units
        for (int i = 0; i < units.Count; i++)
        {
            DeckEquipItemControl item = Instantiate(prefab, parent_item);
            item.Setup(units[i], cur_UnitData, 0);
        }
    }
    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }
}

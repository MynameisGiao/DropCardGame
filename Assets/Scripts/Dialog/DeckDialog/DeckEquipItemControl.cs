using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckEquipItemControl : MonoBehaviour
{
    public GameObject[] rare_objects;
    public GameObject[] rare_objects_frame;
    public Image icon;
    public TMP_Text name_lb;
    public TMP_Text level_lb;
    public TMP_Text hp_lb;
    public TMP_Text damage_lb;
    public GameObject btn_select;

    private ConfigUnitRecord config_unit;
    private UnitData cur_UnitData;
    private UnitData selectedUnitData;

    public void Setup(UnitData data, UnitData cur_UnitData, int index)
    {
        this.cur_UnitData = cur_UnitData;
        config_unit = ConfigManager.instance.configUnit.GetRecordByKeySearch(data.id);
        name_lb.text = config_unit.Name;
        this.selectedUnitData = DataController.instance.GetUnitData(data.id);
        ConfigUnitLevelRecord cf_level = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(selectedUnitData.id);
        if (selectedUnitData.level < cf_level.Maxlv)
            level_lb.text = "Lv " + selectedUnitData.level.ToString();
        else
            level_lb.text = "MAX LV ";
        for (int i = 0; i < rare_objects.Length; i++)
        {
            rare_objects[i].SetActive(i + 1 == (int)config_unit.Rare);
        }
        for (int i = 0; i < rare_objects_frame.Length; i++)
        {
            rare_objects_frame[i].SetActive(i + 1 == (int)config_unit.Rare);
        }

        hp_lb.text =  cf_level.GetHP(selectedUnitData.level).ToString();
        damage_lb.text = cf_level.GetDamage(selectedUnitData.level).ToString();

        icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_unit.Prefab);

    }
    public void OnSelect()
    {
        // Tìm vị trí của cur_UnitData trong deck
        List<UnitData> deck = DataController.instance.GetDeck();
        int deckIndex = -1;
        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i].id == cur_UnitData.id)
            {
                deckIndex = i;
                break;
            }
        }

        if (deckIndex >= 0)
        {
            DataController.instance.ChangeDeck(selectedUnitData, deckIndex);
            selectedUnitData.TriggerEventData(DataSchema.DECK);
            selectedUnitData.TriggerEventData(DataSchema.DIC_UNIT_EX_DECK);
            DialogManager.instance.HideDialog(DialogIndex.DeckEquipDialog);
        }
        else
        {
            Debug.LogError("Không tìm thấy unit trong deck!");
        }

    }
}

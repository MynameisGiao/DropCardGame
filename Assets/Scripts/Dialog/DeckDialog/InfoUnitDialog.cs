using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InfoUnitDialog : BaseDialog
{
    public GameObject[] rare_objects;
    public Image icon;
    public TMP_Text name_lb;
    public TMP_Text level_lb;
    public TMP_Text skill_lb;
    public TMP_Text rare_lb;
    public TMP_Text damage_lb;
    public TMP_Text hp_lb;
    public Button btn_unlock;
    public Button btn_upgrade;
    public TMP_Text cost_lb;
    public TMP_Text stamina_lb;
    public TMP_Text cooldown_lb;

    private ConfigUnitRecord config_unit;
    private ConfigUnitLevelRecord cf_unit_lv;
    private InfoUnitDialogParam dl_param;
    private UnitData data;

    int gold;
    public override void Setup(DialogParam param)
    {
       
        dl_param = (InfoUnitDialogParam)param;
        UpdateData(null);

    }
    public override void OnShowDialog()
    {
        DataTrigger.RegisterValueChange(DataSchema.DIC_UNIT + "/" + config_unit.ID.Tokey(), UpdateData);
    }
    public override void OnHideDialog()
    {
        DataTrigger.UnRegisterValueChange(DataSchema.DIC_UNIT + "/" + config_unit.ID.Tokey(), UpdateData);
    }
    private void UpdateData(object dataChange)
    {
        gold = DataController.instance.GetGold();
        cf_unit_lv = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(dl_param.cf_unit.ID);
        config_unit = dl_param.cf_unit;
        name_lb.text = config_unit.Name.ToUpper();
        skill_lb.text = "Skill: " + config_unit.Skill;
        rare_lb.text = "Rare: " + config_unit.Rare.ToString();
        stamina_lb.text= config_unit.Stamina.ToString();
        cooldown_lb.text= config_unit.Cool_down.ToString();
       
        for (int i = 0; i < rare_objects.Length; i++)
        {
            rare_objects[i].SetActive(i + 1 == (int)config_unit.Rare);
        }
        icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_unit.Prefab);
        cost_lb.gameObject.SetActive(true);
        data = DataController.instance.GetUnitData(config_unit.ID);
        if (data != null)
        {

            int damage = Utilities.CalculatorStat(cf_unit_lv.Min_damage, cf_unit_lv.Max_damage, cf_unit_lv.Maxlv, data.level, cf_unit_lv.Factor_damage);
            int hp = Utilities.CalculatorStat(cf_unit_lv.Min_hp, cf_unit_lv.Max_hp, cf_unit_lv.Maxlv, data.level, cf_unit_lv.Factor_hp);
            level_lb.text = "Level: " + data.level.ToString();
            damage_lb.text = "Damage: " + damage.ToString();
            hp_lb.text = "Hp: " + hp.ToString();
            btn_upgrade.gameObject.SetActive(true);
            btn_unlock.gameObject.SetActive(false);
            
            int costLvNext = Utilities.CalculatorStat(cf_unit_lv.Min_cost, cf_unit_lv.Max_cost, cf_unit_lv.Maxlv, data.level + 1, cf_unit_lv.Factor_cost);
            cost_lb.text=costLvNext.ToString();
            btn_upgrade.interactable = gold >= costLvNext;
            if(data.level >= cf_unit_lv.Maxlv)
            {
                level_lb.text = "MAX LEVEL";
                btn_upgrade.gameObject.SetActive(false);
                cost_lb.gameObject.SetActive(false);
            }
        }
        else // data == null
        {
            level_lb.text = "Level: 1";
            damage_lb.text = "Damage: " +cf_unit_lv.Min_damage.ToString();
            hp_lb.text = "Hp: " + cf_unit_lv.Min_hp.ToString();
            cost_lb.text = cf_unit_lv.Min_cost.ToString();
            btn_upgrade.gameObject.SetActive(false);
            btn_unlock.gameObject.SetActive(true);
            btn_unlock.interactable = gold >= cf_unit_lv.Min_cost;
        }

    }
    public void OnClose()
    {
        DialogManager.instance.HideDialog(dialogIndex);
    }
    public void OnUpgrade()
    {
        DataController.instance.UpgradeUnit(cf_unit_lv, () =>
        {

        });
    }
    public void OnUnlock()
    {
        DataController.instance.UnlockUnit(cf_unit_lv, ()=> 
        { 
            
        });
    }
}

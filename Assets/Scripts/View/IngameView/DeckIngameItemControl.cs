using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DeckIngameItemControl : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    public GameObject[] rare_objects;
    public Image icon;
    public TMP_Text name_lb;
    public TMP_Text stamina_lb;
    public TMP_Text cooldown_lb;

    private UnitData cur_UnitData;
    private ConfigUnitRecord config_unit;
    private IngameView parent;
    private RectTransform rect_item_drag;

    public GameObject item_drag;
    public Image icon_drag;
    public GameObject cdObj;
    public Image clock;
    public GameObject lockObj;
    public GameObject lock_cd;
    public GameObject valid_obj;
    public GameObject invalid_obj;
    public LayerMask layerMask;

    private bool isValid = false;
    private Camera cam;
    private Vector3 pos_drag;
    private Vector3 pos_CreateUnit;
    private bool isDraging;
    private ConfigUnitLevelRecord cf_level;

    public TMP_Text level_lb;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (lockObj.activeSelf)
            return;
        isDraging = true;
        isValid = false;
        parent.lock_ui.SetActive(true);
        item_drag.SetActive(true);
        item_drag.transform.SetParent(parent.m_DraggingPlane, false);
        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        parent.lock_ui.SetActive(false);
        item_drag.SetActive(false);
        // drop card
        if(isValid && !lockObj.activeSelf)
        {
            parent.OnDropUnit(cur_UnitData, config_unit);
            cdObj.SetActive(true);
            clock.fillAmount = 0;
            clock.DOFillAmount(1, config_unit.Cool_down).OnComplete(() =>
            {
                cdObj.SetActive(false);
            });
            MissionManager.instance.OnCreateUnit(cur_UnitData, config_unit,pos_CreateUnit);
        }
        valid_obj.SetActive(false);
        invalid_obj.SetActive(false);
        isValid = false;
        isDraging = false;
        ConfigScene.instance.SetMarkUnitRange(Vector3.zero,1, false);


    }
    private void SetDraggedPosition(PointerEventData data)
    {
        pos_drag = data.position;

        Vector2 pos_local;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent.m_DraggingPlane, data.position, data.pressEventCamera, out pos_local);
        rect_item_drag.anchoredPosition = pos_local;
    }
    private void FixedUpdate()
    {
        if (isDraging)
        {
            Ray r = cam.ScreenPointToRay(pos_drag);
            Debug.DrawLine(r.origin, r.origin + r.direction * 5000, Color.cyan, 0.02f);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 100, layerMask))
            {
                valid_obj.SetActive(true);
                invalid_obj.SetActive(false);
                isValid = true;
                pos_CreateUnit = hit.point;
                ConfigScene.instance.SetMarkUnitRange(pos_CreateUnit, cf_level.GetRange(cur_UnitData.level), true);
            }
            else
            {
                valid_obj.SetActive(false);
                invalid_obj.SetActive(true);
                isValid = false;
                ConfigScene.instance.SetMarkUnitRange(pos_CreateUnit, cf_level.GetRange(cur_UnitData.level), false);
            }
        }

    }
    public void Setup(UnitData unitData, IngameView parent)
    {
        cam = Camera.main;
        this.parent = parent;
        cur_UnitData = unitData;
        List<UnitData> decks = DataController.instance.GetDeck();
        parent.OnStanimaChange.AddListener(OnStaminaChange);

        config_unit = ConfigManager.instance.configUnit.GetRecordByKeySearch(cur_UnitData.id);
        name_lb.text = config_unit.Name;
        stamina_lb.text= config_unit.Stamina.ToString();
        cooldown_lb.text=config_unit.Cool_down.ToString();
        cf_level = ConfigManager.instance.configUnitLevel.GetRecordByKeySearch(config_unit.ID);
        if (cur_UnitData.level < cf_level.Maxlv)
            level_lb.text = "Lv " + cur_UnitData.level.ToString();
        else
            level_lb.text = "MAX LV ";
        
        for (int i = 0; i < rare_objects.Length; i++)
        {
            rare_objects[i].SetActive(i + 1 == (int)config_unit.Rare);
        }
        icon.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_unit.Prefab);
        icon_drag.overrideSprite = SpriteLibControl.instance.GetSpriteByName(config_unit.Prefab);
        rect_item_drag = item_drag.GetComponent<RectTransform>();
        lockObj.SetActive(true);
        lock_cd.SetActive(false);
       
    }
    private void OnStaminaChange(int stamina)
    {
        if(stamina >= config_unit.Stamina)
        {
            lockObj.SetActive(false);
        }
        else
        {
            lockObj.SetActive(true);
        }
    }
}

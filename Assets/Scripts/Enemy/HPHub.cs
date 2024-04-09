using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPHub : MonoBehaviour
{
    private Transform anchor_world;
    private RectTransform parent_rect;
    [SerializeField]
    private RectTransform rect_trans;
    private Camera cam;
    [SerializeField]
    private GameObject group_hp;
    private float time_hide;
    private float cur_value;
    private float hp_value;
    public Image hp_fg;

    private void Awake()
    {
        cam = Camera.main;
    }
    public void SetUp(Transform anchor_world, RectTransform parent_rect, Color color)
    {
        this.anchor_world = anchor_world;
        this.parent_rect = parent_rect;
        hp_fg.color = color;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void UpdateHP(int cur_hp, int max_hp)
    {
        time_hide = 1;
        cur_value = (float)cur_hp / (float)max_hp;
    }
    // Update is called once per frame
    void Update()
    {
        time_hide -= Time.deltaTime;
        group_hp.SetActive(time_hide > 0);

        Vector2 screenPoint = cam.WorldToScreenPoint(anchor_world.position);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parent_rect, screenPoint, null, out localPoint);
        rect_trans.anchoredPosition = localPoint;

        hp_value = Mathf.Lerp(hp_value, cur_value, Time.deltaTime * 2);
        hp_fg.fillAmount = hp_value;
    }
    public void OnDetachHub()
    {
        BYPoolManager.instance.GetPool("HPHub").Despawn(transform);
    }
    public void OnSpawn()
    {
        cur_value = 1;
        hp_value = 1;
        hp_fg.fillAmount = 1;
        group_hp.SetActive(false);
    }
    public void OnDespawn()
    {

        group_hp.SetActive(false);
    }
}


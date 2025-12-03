using UnityEngine;
using UnityEngine.UI;
using System;



public class WeaponHotbar : MonoBehaviour
{
    [Header("UI icon cho 5 ô")]
    public Image[] slots = new Image[5];

    [Header("Vũ khí trong 5 slot")]
    public WeaponSlotData[] weapons = new WeaponSlotData[5];

    [Header("Shop Weapon Data")]
    public Sprite shopGun01Icon;
    public GameObject shopGun01Prefab;

    public Sprite shopGun02Icon;
    public GameObject shopGun02Prefab;

    public Sprite shopGun03Icon;
    public GameObject shopGun03Prefab;

    [Header("Hotbar State")]
    public int activeIndex = 0;
    public event Action<WeaponSlotData> OnSelectionChanged;

    // PlayerPrefs keys
    private string shopGun01Key = "Item_01_Bought";
    private string shopGun02Key = "Item_02_Bought";
    private string shopGun03Key = "Item_03_Bought";

    void Start()
    {
        Debug.Log("=== WEAPON HOTBAR START ===");

        InitializeWeapons();
        RefreshUI();

        Debug.Log("==========================");
    }

    // Load từ PlayerPrefs và add weapon đã mua
    void InitializeWeapons()
    {
        // Item 01
        if (PlayerPrefs.GetInt(shopGun01Key, 0) == 1 && shopGun01Prefab != null && shopGun01Icon != null)
        {
            weapons[0] = new WeaponSlotData { icon = shopGun01Icon, prefab = shopGun01Prefab };
            Debug.Log("[Hotbar] Slot 0 cập nhật Item_01");
        }

        // Item 02
        if (PlayerPrefs.GetInt(shopGun02Key, 0) == 1 && shopGun02Prefab != null && shopGun02Icon != null)
        {
            AddWeapon(shopGun02Icon, shopGun02Prefab);
            Debug.Log("[Hotbar] Thêm Item_02 vào hotbar");
        }

        // Item 03
        if (PlayerPrefs.GetInt(shopGun03Key, 0) == 1 && shopGun03Prefab != null && shopGun03Icon != null)
        {
            AddWeapon(shopGun03Icon, shopGun03Prefab);
            Debug.Log("[Hotbar] Thêm Item_03 vào hotbar");
        }

        // Debug: Hiển thị tất cả slot
        for (int i = 0; i < weapons.Length; i++)
        {
            var w = weapons[i];
            if (w != null && w.prefab != null)
                Debug.Log($"[Hotbar] Slot[{i}]: {w.prefab.name}");
            else
                Debug.Log($"[Hotbar] Slot[{i}]: NULL");
        }
    }

    void Update()
    {
        // Chọn slot bằng phím 1-5
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                SelectSlot(i);
        }

        // Chọn slot bằng scroll wheel
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            int dir = (int)Mathf.Sign(scroll);
            SelectSlot((activeIndex - dir + slots.Length) % slots.Length);
        }
    }

    public void SelectSlot(int idx)
    {
        idx = Mathf.Clamp(idx, 0, weapons.Length - 1);
        if (idx == activeIndex) return;

        activeIndex = idx;
        RefreshUI();

        var slot = GetCurrentSlot();
        Debug.Log($"[Hotbar] Selected slot {idx}: {(slot?.prefab != null ? slot.prefab.name : "NULL")}");
        OnSelectionChanged?.Invoke(slot);
    }

    void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i]) continue;

            var data = (i < weapons.Length) ? weapons[i] : null;

            if (data != null && data.prefab != null)
            {
                slots[i].sprite = data.icon;
                slots[i].enabled = true;
            }
            else
            {
                slots[i].enabled = false;
            }

            slots[i].color = (i == activeIndex) ? Color.white : new Color(1, 1, 1, 0.5f);
        }
    }

    // Thêm weapon mới vào hotbar (khi vừa mua)
    public bool AddWeapon(Sprite icon, GameObject prefab)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null || weapons[i].prefab == null)
            {
                weapons[i] = new WeaponSlotData { icon = icon, prefab = prefab };
                RefreshUI();
                Debug.Log($"[Hotbar] Thêm weapon mới vào slot {i}: {prefab.name}");
                return true;
            }
        }
        Debug.LogWarning("[Hotbar] Không thể thêm weapon, hotbar đầy!");
        return false;
    }

    public WeaponSlotData GetCurrentSlot()
    {
        return (activeIndex < weapons.Length) ? weapons[activeIndex] : null;
    }
}

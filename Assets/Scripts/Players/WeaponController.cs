using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform weaponHolder;

    [Header("Default Weapons")]
    public GameObject gunSlot1;
    public GameObject gunSlot2;

    [Header("Shop Weapons")]
    public GameObject shopGun_01; // Item 1
    public GameObject shopGun_02; // Item 2
    public GameObject shopGun_03; // Item 3
    public GameObject shopGun_04; // Item 4
    public GameObject shopGun_05; // Item 5
    public GameObject shopGun_06; // Item 6

    [Header("Hotbar Reference")]  // Assign Hotbar GameObject ở đây!
    public WeaponHotbar hotbar;

    private GameObject currentWeapon;
    private SpriteRenderer playerSprite;
    private Gun currentGun;

    // PlayerPrefs keys (giữ nguyên nếu cần)
    private string shopGun01Key = "Item_01_Bought";
    private string shopGun02Key = "Item_02_Bought";
    private string shopGun03Key = "Item_03_Bought";
    private string shopGun04Key = "Item_04_Bought";
    private string shopGun05Key = "Item_05_Bought";
    private string shopGun06Key = "Item_06_Bought";

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();

        // Subscribe event từ Hotbar để equip khi select
        if (hotbar != null)
        {
            hotbar.OnSelectionChanged += OnHotbarWeaponSelected;
            Debug.Log("[WeaponController] Subscribed to Hotbar event");
        }
        else
        {
            Debug.LogError("[WeaponController] Hotbar reference NULL! Assign in Inspector.");
        }

        // Bỏ initial equip ở đây, để Hotbar handle (nó sẽ trigger SelectSlot ở Start())
    }

    private void OnHotbarWeaponSelected(WeaponSlotData data)
    {
        GameObject prefabToEquip = data.prefab;

        // Fallback default nếu chưa mua (dựa trên slot)
        if (prefabToEquip == null)
        {
            switch (hotbar.activeIndex)
            {
                case 1:  // Slot 1 dùng default2
                    prefabToEquip = gunSlot2;
                    break;
                default:  // Các slot khác dùng default1
                    prefabToEquip = gunSlot1;
                    break;
            }
        }

        if (prefabToEquip != null)
        {
            EquipWeapon(prefabToEquip);
            Debug.Log($"[WeaponController] Equipped: {prefabToEquip.name} for slot {hotbar.activeIndex}");
        }
    }

    void Update()
    {
        // KHÔNG xử lý input phím nữa! Hotbar handle hết.

        if (currentWeapon == null) return;

        var weaponSprite = currentWeapon.GetComponent<SpriteRenderer>();
        if (weaponSprite != null)
            weaponSprite.flipX = playerSprite.flipX;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - currentWeapon.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        currentWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(weaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        currentGun = currentWeapon.GetComponent<Gun>();
    }

    // Cleanup event
    private void OnDestroy()
    {
        if (hotbar != null)
            hotbar.OnSelectionChanged -= OnHotbarWeaponSelected;
    }
}
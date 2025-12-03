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

    private GameObject currentWeapon;
    private SpriteRenderer playerSprite;
    private Gun currentGun;

    // PlayerPrefs keys
    private string shopGun01Key = "Item_01_Bought";
    private string shopGun02Key = "Item_02_Bought";
    private string shopGun03Key = "Item_03_Bought";

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();

        // Kiểm tra PlayerPrefs
        bool hasShopGun01 = PlayerPrefs.GetInt(shopGun01Key, 0) == 1;
        bool hasShopGun02 = PlayerPrefs.GetInt(shopGun02Key, 0) == 1;
        bool hasShopGun03 = PlayerPrefs.GetInt(shopGun03Key, 0) == 1;

        // Trang bị ưu tiên: Item 3 > Item 2 > Item 1 > gunSlot1
        if (hasShopGun03 && shopGun_03 != null)
        {
            EquipWeapon(shopGun_03);
        }
        else if (hasShopGun02 && shopGun_02 != null)
        {
            EquipWeapon(shopGun_02);
        }
        else if (hasShopGun01 && shopGun_01 != null)
        {
            EquipWeapon(shopGun_01);
        }
        else
        {
            EquipWeapon(gunSlot1);
        }
    }

    void Update()
    {
        // Input trang bị
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bool hasGun1 = PlayerPrefs.GetInt(shopGun01Key, 0) == 1;
            EquipWeapon(hasGun1 && shopGun_01 != null ? shopGun_01 : gunSlot1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bool hasGun2 = PlayerPrefs.GetInt(shopGun02Key, 0) == 1;
            EquipWeapon(hasGun2 && shopGun_02 != null ? shopGun_02 : gunSlot2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bool hasGun3 = PlayerPrefs.GetInt(shopGun03Key, 0) == 1;
            EquipWeapon(hasGun3 && shopGun_03 != null ? shopGun_03 : gunSlot1);
        }

        // Điều khiển hướng súng
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
        if (weaponPrefab == null)
        {
            Debug.LogError("[EquipWeapon] weaponPrefab NULL!");
            return;
        }

        if (weaponHolder == null)
        {
            Debug.LogError("[EquipWeapon] weaponHolder NULL!");
            return;
        }

        if (currentWeapon != null)
            Destroy(currentWeapon);

        currentWeapon = Instantiate(weaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        currentGun = currentWeapon.GetComponent<Gun>();
    }
}

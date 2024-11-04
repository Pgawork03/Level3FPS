using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform barrel;
    [Header("Ammo")]
    [SerializeField] private int currentAmmo;
    [SerializeField] private int maxAmmo;
    [SerializeField] private bool infiniteAmmo;

    [Header("Performance")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float shootRate;
    [SerializeField] private int damage;

    private ObjectPool objectPool;
    private float lastShootTime;

    private bool isPlayer;

    private void Awake()
    {
        //Check if I am a Player
        if (GetComponent<PlayerMovement>()) isPlayer = true;
         
        //get objectPool component
        objectPool = GetComponent<ObjectPool>();
    }

    //public bool CanShoot()
    //{
    //    if (Time.time - lastShootTime > shootRate)
    //    {
    //        if(currentAmmo > 0) || infiniteAmmo)
    //            {
    //                return true;
    //            }
    //    }
    //}
    public void Shoot()
    {
        lastShootTime = Time.time;
        if (!infiniteAmmo) currentAmmo--;
        GameObject bullet = objectPool.GetGameObject();
        bullet.transform.position = barrel.position;
        bullet.transform.rotation = barrel.rotation;
    }
}

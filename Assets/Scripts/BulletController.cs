using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Info")]
    [SerializeField] private float activeTime;


    private int damage;

    public int Damage { get => damage; set => damage = value; }

    //When the gameobject SetActive = true
    private void OnEnable()
    {
        StartCoroutine(DeactiveAfterTime());
    }

    private IEnumerator DeactiveAfterTime()
    {
        yield return new WaitForSeconds(activeTime);
        gameObject.SetActive(false);
    }

    //when the bullet collide something 
    private void OnTriggerEnter(Collider other)
    {
        //Desactive the bullet
        gameObject.SetActive(false);

        //TODO Collision with enemy or player or floot or wall or object

    }

}

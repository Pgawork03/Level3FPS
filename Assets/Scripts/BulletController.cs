using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Header("Bullet Info")]
    [SerializeField] private float activeTime;

    [Header("Particle")]
    [SerializeField] private GameObject damageParticle;
    [SerializeField] private GameObject failParticle;
    


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
        if (other.CompareTag("Enemy"))
        {
            //Instantiate damageParticle "Blood"
            GameObject particles = Instantiate(damageParticle,transform.position,Quaternion.identity);
            //Create Damage on Enemy
            other.GetComponent<EnemyController>().DamageEnemy(damage);
        }
        else if (other.CompareTag("Player"))
        {
            GameObject particles = Instantiate(damageParticle, transform.position, Quaternion.identity);
            //TODO reduce life to Player
        }
        else
        {
            GameObject particles = Instantiate(failParticle, transform.position, Quaternion.identity);
        }

    }

}

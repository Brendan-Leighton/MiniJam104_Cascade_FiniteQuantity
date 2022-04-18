using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public Material thisMaterial;
    [SerializeField] private float _speed = 10f;
    private Vector3 direction;
    private float damage;

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    public void SetDirection(int projectileDirection)
    {
        direction = new Vector3(projectileDirection, 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("direction: " + direction);
        Vector3 projectileSpeed = direction * Time.deltaTime * _speed;
        Debug.Log("projectileSpeed:: " + projectileSpeed);
        gameObject.transform.Translate(projectileSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigga entered");
        if (other.CompareTag("Character"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<Character>().TakeDamage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Must be setted")]
    [SerializeField] private int damage = 1;
    [SerializeField] private int pierce = 0;
    [SerializeField] private int speed = 10;
    private float timer = 0;
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime; if (timer >= 10) { Destroy(this.gameObject); }
        this.transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        EventObject eventObject = collision.gameObject.GetComponent<EventObject>();
        if (eventObject.integrityPoints == 9999) { return; }
        eventObject.TakeDamage(damage, transform.position);

        if (eventObject.GetUnpierceableBool() == true) { pierce = -1; }
        else if (eventObject.GetUnpierceableBool() == false) { pierce--; }

        if (pierce < 0)
        { Destroy(this.gameObject); }
    }
}
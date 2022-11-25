using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Must be setted")]
    public int damage = 1;
    public int speed = 10;
    private float timer = 0;
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime; if (timer >= 10) { Destroy(this.gameObject); }
        this.transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        EventObject eventObject = collision.gameObject.GetComponent<EventObject>();
        if (eventObject.integrityPoints == 999) { return; }
        eventObject.TakeDamage(damage);
        Destroy(this.gameObject);
    }
}

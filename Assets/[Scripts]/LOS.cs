using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
[System.Serializable]
public class LOS : MonoBehaviour
{
    [Header("Detections Properties")]
    public Collider2D collidesWith;
    public ContactFilter2D contactFilter;
    public List<Collider2D> colisionList;

    private PolygonCollider2D LOScollider;
    // Start is called before the first frame update
    void Start()
    {
        LOScollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics2D.GetContacts(LOScollider, contactFilter, colisionList);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collidesWith = collision;
    }
}

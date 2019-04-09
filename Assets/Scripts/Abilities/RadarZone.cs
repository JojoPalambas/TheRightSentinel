using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarZone : MonoBehaviour
{
    public GameObject signaler;
    public Sentinel owner;

    private List<Player> alreadySeen;

    private void Start()
    {
        alreadySeen = new List<Player>();
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (owner != null && collider.gameObject != owner.gameObject && collider.GetComponent<Player>() != null && !alreadySeen.Contains(collider.GetComponent<Player>()))
        {
            Instantiate(signaler, collider.transform);
            alreadySeen.Add(collider.GetComponent<Player>());
        }
    }
}

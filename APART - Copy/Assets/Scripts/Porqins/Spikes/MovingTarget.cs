using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : MonoBehaviour
{
    Transform player;
    PlayerController pc;

    public float boundsExtender;
    public float timeBetweenMoves;

    bool didMove = false;

    private void Start()
    {
        transform.localPosition = Vector3.zero;
        player = transform.root;
        pc = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!didMove)
        {
            StartCoroutine(MoveLocation());
        }
            
    }
    IEnumerator MoveLocation()
    {
        didMove = true;

        Vector3 newPos = new Vector3(
            Random.Range(pc.boxCollider.bounds.extents.x - (boundsExtender * 1.5f), pc.boxCollider.bounds.extents.x),
            Random.Range(pc.boxCollider.bounds.extents.y - (boundsExtender * 1.5f), pc.boxCollider.bounds.extents.y + (boundsExtender * 1.5f)), 0f);
        transform.localPosition = newPos;
        
        yield return new WaitForSeconds(timeBetweenMoves);

        didMove = false;
    }
}

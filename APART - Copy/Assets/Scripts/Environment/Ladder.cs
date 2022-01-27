using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void Awake()
    {
        float width = GetComponent<SpriteRenderer>().size.x;
        float height = GetComponent<SpriteRenderer>().size.y;

        Transform topOfLadder = transform.GetChild(0).transform;
        Transform bottomOfLadder = transform.GetChild(1).transform;

        topOfLadder.position = new Vector3(transform.position.x, transform.position.y + (height / 2), 0f);
        bottomOfLadder.position = new Vector3(transform.position.x, transform.position.y - (height / 2), 0f);

        GetComponent<BoxCollider2D>().offset = Vector2.zero;
        GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }
}

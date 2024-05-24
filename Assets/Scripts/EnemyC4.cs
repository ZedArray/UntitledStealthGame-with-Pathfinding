using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC4 : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Explosion explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = (PlayerController)FindObjectOfType(typeof(PlayerController));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(explosionPrefab, transform.position, new Quaternion());
            player.killPlayer();
            Destroy(gameObject);
        }
    }
}

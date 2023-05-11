using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] spawns;
    public Transform player;
    public GameObject enemyPrefab;
    Vector3 playerPosition, whereToGo;
    float speed;
    float repeater;
    // Start is called before the first frame update
    void Start()
    {
        speed = 8f;
        repeater = 3f;
        InvokeRepeating("LaunchProjectile", 1.0f, repeater);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchProjectile() {
        speed += 1f;
        repeater -= 0.1f;
        var i = Random.Range(0, spawns.Length);
        var enemy = Instantiate(enemyPrefab, spawns[i].position, Quaternion.identity);
        playerPosition = player.position;
        whereToGo = (playerPosition - spawns[i].position).normalized;
        enemy.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector2(whereToGo.x * speed, whereToGo.y * speed);

        Destroy(enemy, 5f);
    }
}

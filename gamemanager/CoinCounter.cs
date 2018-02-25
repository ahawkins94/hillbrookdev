using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{

    public Player player;

    private void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
            player = GetComponent<Player>();
            Debug.Log("coincounter");
            player.coins = player.coins + 1;
            Destroy(this.gameObject);
         
        }
    }

    
}

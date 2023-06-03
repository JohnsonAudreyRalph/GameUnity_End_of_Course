using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int Coins_Number = 0;
    [SerializeField] private Text Coins_Text;
    [SerializeField] private AudioSource jumpClolector;

    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if (collsion.gameObject.CompareTag("coins"))
        {
            Destroy(collsion.gameObject);
            Coins_Number++;
            jumpClolector.Play();
            Debug.Log("Số coin: " + Coins_Number);
            Coins_Text.text = "COINS: " + Coins_Number;
        }
    }
}

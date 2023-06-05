using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInteractionButton : MonoBehaviour
{
    public GameObject buttumInteraction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            buttumInteraction.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        buttumInteraction.SetActive(false);
    }
}

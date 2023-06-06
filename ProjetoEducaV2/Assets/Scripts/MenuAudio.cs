using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameManager.Instance.PlayMusic("Menu");
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

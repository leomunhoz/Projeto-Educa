using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QiManager : MonoBehaviour
{
    public int qi = 0;
    public TMP_Text qiText;
    void Start()
    {
        qiText.text = qi.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

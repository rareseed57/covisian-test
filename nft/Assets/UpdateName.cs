using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(string username = "prova")
    {
        var tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = "Hi, " + username + "!";
    }
}

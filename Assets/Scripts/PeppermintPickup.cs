using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PeppermintPickup : MonoBehaviour
{
    public int pepCount;
    public TextMeshProUGUI pepText;
    public GameObject Door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pepText.text = pepCount.ToString();

        if(pepCount == 3)
        {
            Object.Destroy(Door);
            Debug.Log("Door Open");
        }
    }
}

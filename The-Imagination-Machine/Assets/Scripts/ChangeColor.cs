using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public GameObject obj;
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = obj.GetComponent<Renderer>();
    }

    public void ChangeToRandomColor() {
        rend.material.SetColor("_Color", Random.ColorHSV());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

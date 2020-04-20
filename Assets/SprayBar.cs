using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprayBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Mother mother;

    private Slider slider;
    void Start() {
        slider = (Slider) GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = mother.GetDisinfectantCharge();
    }
}

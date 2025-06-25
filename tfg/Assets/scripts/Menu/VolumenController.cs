using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumenController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private string controller;
    void Start()
    {
        float volumen;
        audioMixer.GetFloat(controller, out volumen);
        slider.value = Mathf.Pow(10, volumen / 20);
    }
    public void ControlDeVolumen(float v)
    {
        audioMixer.SetFloat(controller, Mathf.Log10(v) * 20);
    }

    
}

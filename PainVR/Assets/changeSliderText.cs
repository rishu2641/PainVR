 using UnityEngine;
 using UnityEngine.UI;
 
 public class changeSliderText : MonoBehaviour {
 
     public Text sliderValue;
	 public Slider slider;
	 
	 void Update(){
	 
	 sliderValue.text = slider.value.ToString("0.0");
	 
	 }
 }
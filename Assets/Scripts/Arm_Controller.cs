using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arm_Controller : MonoBehaviour
{
    public Slider baseSlider;
    public Slider armSlider;
    public Slider armBarSlider;
    public Slider elbowArmSlider;
    public Slider wristSlider;

    //valor del slider para la base va de -1 a 1.
    public float baseSliderValue = 0.0f;
    //Valor para el brazo
    public float upperArmSliderValue = 0.0f;
    //Valor del slider para el hombro
    public float armBarSliderValue = 0.0f;
    //Valor Slider para el codo
    public float elbowArmSliderValue = 0.0f;
    //Valor Slider para la muñeca
    public float wristSliderValue = 0.0f;

    //Estos espacios sirven para unir apropiadamente las partes del robot al inspector
    public Transform robotBase;
    public Transform robotArm;
    public Transform robotBar;
    public Transform robotElbow;
    public Transform robotWrist;

    //Permite tener numeros para ajustar los valores de velocidad de cada parte a rotar
    public float baseTurnRate = 1.0f;
    public float upperArmTurnRate = 1.0f;
    public float barArmTurnRate = 1.0f;
    public float elbowArmTurnRate = 1.0f;
    public float wristTurnRate = 1.0f;

    //Se ajustan los maximos y minimos de rotacion para cada articulación
    private float baseYRot = 0.0f;
    public float baseYRotMin = -180.0f;
    public float baseYRotMax = 180.0f;

    private float upperArmXRot = 0.0f;
    public float upperArmXRotMin = -60.0f;
    public float upperArmXRotMax = 60.0f;

    private float barArmXRot = 0.0f;
    public float barArmXRotMin = -90.0f;
    public float barArmXRotMax = 90.0f;

    private float elbowArmXRot = 0.0f;
    public float elbowArmXRotMin = -90.0f;
    public float elbowArmXRotMax = 90.0f;

    private float wristYRot = 0.0f;
    public float wristYRotMin = -90.0f;
    public float wristYRotMax = 90.0f;

    void Start()
    {
        //definir valores iniciales
        baseSlider.minValue = -1;
        armSlider.minValue = -1;
        armBarSlider.minValue = -1;
        elbowArmSlider.minValue = -1;
        wristSlider.minValue = -1;

        baseSlider.maxValue = 1;
        armSlider.maxValue = 1;
        armBarSlider.maxValue = 1;
        elbowArmSlider.maxValue = 1; 
        wristSlider.maxValue = 1;
    }

    void CheckInput()
    {
        baseSliderValue = baseSlider.value;
        upperArmSliderValue = armSlider.value;
        armBarSliderValue = armBarSlider.value;
        elbowArmSliderValue = elbowArmSlider.value;
        wristSliderValue = wristSlider.value;
    }
    
    void ProcessMovement()
    {
        //proporciona la rotacion para el robot alrededor del eje Y para la Base
        baseYRot += baseSliderValue * baseTurnRate;
        baseYRot = Mathf.Clamp(baseYRot, baseYRotMin, baseYRotMax);
        robotBase.localEulerAngles = new Vector3(robotBase.localEulerAngles.x, baseYRot, robotBase.localEulerAngles.z);
        //proporciona la rotacion para el robot alrededor del eje X para la Brazo
        upperArmXRot += upperArmSliderValue * upperArmTurnRate;
        upperArmXRot = Mathf.Clamp(upperArmXRot, upperArmXRotMin, upperArmXRotMax);
        robotArm.localEulerAngles = new Vector3(upperArmXRot, robotArm.localEulerAngles.y, robotArm.localEulerAngles.z);
        //Replicar las funciones anteriores para las otras articulaciones dependiendo si la rotacion es en X  o en Y
        barArmXRot += armBarSliderValue * barArmTurnRate;
        barArmXRot = Mathf.Clamp(barArmXRot, barArmXRotMin, barArmXRotMax);
        robotBar.localEulerAngles = new Vector3(barArmXRot, robotBar.localEulerAngles.y, robotBar.localEulerAngles.z);

        //4 en X, 5 en Y, 6 en X
        //Art 4
        elbowArmXRot += elbowArmSliderValue * elbowArmTurnRate;
        elbowArmXRot = Mathf.Clamp(elbowArmXRot, elbowArmXRotMin, elbowArmXRotMax);
        robotElbow.localEulerAngles = new Vector3(elbowArmXRot, robotElbow.localEulerAngles.y, robotElbow.localEulerAngles.z);
        //Art 5
        wristYRot += wristSliderValue * wristTurnRate;
        wristYRot = Mathf.Clamp(wristYRot, wristYRotMin, wristYRotMax);
        robotWrist.localEulerAngles = new Vector3(robotWrist.localEulerAngles.x, wristYRot, robotWrist.localEulerAngles.z);
    }

    public void ResetSliders()
    {
        //Devuelve los valores a 0 cuando el raton no esta haciendo click en el slider
        baseSliderValue = 0.0f;
        baseSlider.value = 0.0f;
        upperArmSliderValue = 0.0f;
        armSlider.value = 0.0f;
        armBarSliderValue = 0.0f;
        armBarSlider.value = 0.0f;
        elbowArmSliderValue = 0.0f;
        elbowArmSlider.value = 0.0f;
        wristSliderValue = 0.0f;
        wristSlider.value = 0.0f;
    }
    
    void Update()
    {
        CheckInput();
        ProcessMovement();
    }

    
}

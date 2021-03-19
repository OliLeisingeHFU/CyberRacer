using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horInput;
    private float vertInput;
    private float steerAngle;
    private float currentbrakeForce;
    private bool breaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider RearRightCol;
    [SerializeField] private WheelCollider RearLeftCol;
    [SerializeField] private WheelCollider FrontLeftCol;
    [SerializeField] private WheelCollider FrontRightCol;

    [SerializeField] private Transform RearRightMesh;
    [SerializeField] private Transform RearLeftMesh;
    [SerializeField] private Transform FrontLeftMesh;
    [SerializeField] private Transform FrontRightMesh;

    void FixedUpdate()
    {
        getInput();
        handleMotor();
        handleSteering();
        updateWheels();
    }

    private void getInput()
    {
        horInput = Input.GetAxis(HORIZONTAL);
        vertInput = Input.GetAxis(VERTICAL);
        breaking = Input.GetKey(KeyCode.Space);
    }

    private void handleMotor()
    {
        FrontLeftCol.motorTorque = vertInput * motorForce;
        FrontRightCol.motorTorque = vertInput * motorForce;
        currentbrakeForce = breaking ? brakeForce : 0f;
        brake();
    }

    private void handleSteering()
    {
        steerAngle = maxSteerAngle * horInput;
        FrontLeftCol.steerAngle = steerAngle;
        FrontRightCol.steerAngle = steerAngle;
    }

    private void brake()
    {
        RearRightCol.brakeTorque = currentbrakeForce;
        RearLeftCol.brakeTorque = currentbrakeForce;
        FrontLeftCol.brakeTorque = currentbrakeForce;
        FrontRightCol.brakeTorque = currentbrakeForce;
    }

    private void updateWheels()
    {
        updateSingleWheel(FrontLeftCol, FrontLeftMesh);
        updateSingleWheel(FrontRightCol, FrontRightMesh);
        updateSingleWheel(RearRightCol, RearRightMesh);
        updateSingleWheel(RearLeftCol, RearLeftMesh);
    }

    private void updateSingleWheel(WheelCollider collie, Transform wheel)
    {
        Vector3 pos;
        Quaternion rot;
        collie.GetWorldPose(out pos, out rot);
        wheel.rotation = rot;
        wheel.position = pos;
    }

}

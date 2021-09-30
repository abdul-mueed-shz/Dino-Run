using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cineCamScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator camAnim;
    public bool _normalCam = false;

    [SerializeField] private CinemachineVirtualCamera normal;
    [SerializeField] private CinemachineVirtualCamera rage;

    [SerializeField] private CinemachineVirtualCamera startCam;

    public bool StartCam =true;
    private void Awake() {
        camAnim = GetComponent<Animator>();
    }
    void Update()
    {
        if(!StartCam)
        {
            startCam.Priority = 0;
            switchCam();
            switchPriority();
        }
    }
    void StartCamera()
    {
        camAnim.Play("start");
    }
    void switchCam()
    {
        if(_normalCam)
        {
            camAnim.Play("rage");
        }
        else
        {
            camAnim.Play("normal");
        }
        _normalCam = !_normalCam;
    }
    void switchPriority()
    {
        if(_normalCam)
        {
            normal.Priority = 1;
            rage.Priority = 2;
        }
        else
        {
            normal.Priority = 2;
            rage.Priority = 1;
        }
        _normalCam = !_normalCam;
    }

    // Update is called once per frame
    
}

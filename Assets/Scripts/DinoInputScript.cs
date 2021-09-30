using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoInputScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float _recentFrameFingerPosX;
    private float _movementFactorX;

    public float movementX => _movementFactorX;

    private Animator _anim;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _recentFrameFingerPosX = Input.mousePosition.x;
        }
        else if(Input.GetMouseButton(0))
        {
            
            _movementFactorX = Input.mousePosition.x - _recentFrameFingerPosX;
            _recentFrameFingerPosX = Input.mousePosition.x;
            
            // if(_movementFactorX < 0)
            // {
            //     //_anim.SetFloat("Movement",0.5f);
            //     _anim.SetTrigger("TurnLeft");
            //     //Debug.LogError("LEFT");
            // }
            // else if(_movementFactorX >0){
            //     //_anim.SetFloat("Movement",1f);
            //     _anim.SetTrigger("TurnRight");
            // }

        }
        else if(Input.GetMouseButtonUp(0))
        {
            //_anim.SetFloat("Movement",0f);
            _movementFactorX = 0f;
            
        }
        
        
    }
}

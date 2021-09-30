using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    Animator _raptrAnim;
    void Update()
    {
        _raptrAnim = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
        {
            GetComponent<Collider>().isTrigger = true;
            _raptrAnim.SetTrigger("Attack");
            Destroy(gameObject, 5f);
        }
    }
}

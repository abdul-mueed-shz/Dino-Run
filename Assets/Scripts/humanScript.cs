using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanScript : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject blood;
    [SerializeField] private float minDistance = 20f;
    
    [SerializeField] private bool _isTerrified = false;
    private Animator _humanAnim;
    
    void Start()
    {
        _humanAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        if(distFromPlayer <= minDistance && !_isTerrified)
        {
            _isTerrified = true;
            _humanAnim.SetTrigger("terrified");
        }
        // if(distFromPlayer <= (minDistance/2))
        // {
        //     _humanAnim.SetTrigger("runBack");
        // }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject,0.5f);
            Invoke("bloodSplat",0.4f);
        }
    }
    private void bloodSplat()
    {
        Vector3 humanPos = transform.position;
        humanPos.y += 1.2f;
        GameObject clone = (GameObject)Instantiate (blood, humanPos, Quaternion.identity);
        humanPos.y += 0.5f;
        humanPos.x -= 1f;
        GameObject clone1 = (GameObject)Instantiate (blood, humanPos, Quaternion.identity);
        humanPos.y += 0.5f;
        humanPos.x += 2f;
        GameObject clone2 = (GameObject)Instantiate (blood, humanPos, Quaternion.identity);
        humanPos.y += 0.3f;
        humanPos.x -= 1f;
        GameObject clone3 = (GameObject)Instantiate (blood, humanPos, Quaternion.identity);
        Destroy(clone,1f);
        Destroy(clone1,2f);
        Destroy(clone2,3f);
        Destroy(clone3,3f);
    }
}

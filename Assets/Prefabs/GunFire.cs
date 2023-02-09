using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    [SerializeField] RaycastHit hitTarget;
    [SerializeField] Transform fireTransform;
    [SerializeField] GameObject Target;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 레이 디버그
        //Debug.DrawRay(fireTransform.position,fireTransform.forward* 10f,Color.green);

        
    }
     

    public void ShotRay()
    {
        
        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hitTarget))
        {
            
            Debug.Log(hitTarget.collider.gameObject.name);

            if (hitTarget.collider.gameObject != null)
            {
                Destroy(hitTarget.collider.gameObject);
            }
            
            
        }

    }


}

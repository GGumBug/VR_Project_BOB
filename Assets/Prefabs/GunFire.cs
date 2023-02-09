using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    [SerializeField] RaycastHit hitTarget;
    [SerializeField] Transform fireTransform;
    [SerializeField] GameObject Target;
    [SerializeField] GameObject gunAim;
    [SerializeField] Target target;

    void Start()
    {
        
    }

    void Update()
    {        
        Debug.DrawRay(fireTransform.position,fireTransform.forward* 10f,Color.green);

        
    }

    //public void AimRay()
    //{


    //    if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hitTarget))
    //    {
    //        Debug.Log(hitTarget.collider.gameObject.name);

    //        GameObject aimPos;
    //       // aimPos = amiPos.hitTarget.collider.gameObject.Transform.Position;

    //    }
    //}

     public void ShotRay()
     {      

        if (Physics.Raycast(fireTransform.position, fireTransform.forward, out hitTarget))
        {

            //if (hitTarget.collider.gameObject == target.CompareTag("_Note"))
            //{
            //    Destroy(hitTarget.collider.gameObject);
            //}

            if (hitTarget.collider.gameObject != null)
            {
                Destroy(hitTarget.collider.gameObject);
            }
            
            
        }

     }


}

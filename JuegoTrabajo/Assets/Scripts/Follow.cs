using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;

    private float target_poseX;
    private float target_poseY;

    private float posX;
    private float posY;

    public float derechaMax;
    public float izquierdaMax;

    public float alturaMax;
    public float alturaMin;

    public bool encendida = true;

    public float velocidad;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        posX = target_poseX + derechaMax;
        posY = target_poseY + alturaMin;
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), 1);
    }

    void Move_cam()
    {
        if (encendida)
        {
            if (target)
            {
                target_poseX = target.transform.position.x;
                target_poseY = target.transform.position.y;

                if(target_poseX > derechaMax && target_poseX < izquierdaMax)
                {
                    posX = target_poseX;
                }

                if(target_poseY < alturaMax && target_poseY > alturaMin)
                {
                    posY = target_poseY;
                }
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), velocidad*Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move_cam();
    }
}

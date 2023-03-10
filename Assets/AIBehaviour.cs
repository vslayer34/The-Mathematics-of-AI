using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject targetFuel;

    bool toggle = false;

    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            toggle = !toggle;
        }

        if (toggle && CalculateDistance(transform.position, targetFuel.transform.position) > 1.0f)
        {
            Debug.Log("AI activated.");
            CalculateAngle();
            //MoveTank();
        }
        else
        {
            Debug.Log("AI deactivated");
        }

    }

    Vector3 Cross(Vector3 v, Vector3 w)
    {
        float multiX = v.y * w.z - v.z * w.y;
        float multiY = v.x * w.z - v.z * w.x;
        float multiZ = v.x * w.y - v.y * w.z;

        return new Vector3(multiX, multiY, multiZ);
    }

    void CalculateAngle()
    {
        Vector3 tankForward = transform.up;
        Vector3 direction = targetFuel.transform.position - transform.position;

        float dot = tankForward.x * direction.x + tankForward.y * direction.y;
        float angle = Mathf.Acos(dot / (tankForward.magnitude * direction.magnitude));

        int clockWise = (Cross(tankForward, direction).z > 0.0f) ? 1 : -1;

        Debug.Log(angle * Mathf.Deg2Rad * clockWise);
        transform.Rotate(0.0f, 0.0f, angle * Mathf.Deg2Rad * clockWise);
    }

    void MoveTank()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    float CalculateDistance(Vector3 location, Vector3 targetLocation)
    {
        location.y = 0.0f;
        targetLocation.y = 0.0f;

        return Vector3.Distance(location, targetLocation);
    }
}

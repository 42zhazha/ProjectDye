using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    new Rigidbody rigidbody;
    [SerializeField] Transform TakePoint;
    DyeObject takeDye;

    float speed = 8;
    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (takeDye == null)
                Take();
            else
                Pun();
        }
        if (Input.GetKey(KeyCode.C) && takeDye == null)
        {
            Work();
        }
    }

    void Work()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.3f, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("DyeCube") && hit.transform.name == "ToolTable")
            {
                ToolTable toolTable = hit.transform.GetComponent<ToolTable>();
                if (toolTable.CanWork)
                    toolTable.Work();
            }
        }
    }

    void MoveUpdate()
    {
        Vector3 targerPosition = transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rigidbody.MovePosition(Vector3.MoveTowards(transform.position, targerPosition, speed * Time.deltaTime));
        transform.LookAt(targerPosition, Vector3.up);
    }

    void Take()
    {

        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.3f, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("DyeCube"))
            {
                takeDye = hit.transform.GetComponent<DyeCube>().Take();
                if (takeDye != null)
                    takeDye.Mounting(TakePoint);
            }
            else if (hit.transform.CompareTag("DyeObject"))
            {
                takeDye = hit.transform.GetComponent<DyeObject>();
                takeDye.Mounting(TakePoint);
            }
        }

    }

    void Pun()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.3f, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("DyeCube"))
            {
                DyeCube cube = hit.transform.GetComponent<DyeCube>();
                if (cube.Put(takeDye))
                    takeDye = null;
            }
            if (hit.transform.CompareTag("DyeObject"))
            {
                if (hit.transform.name == "Pot" && hit.transform.GetComponent<DyePot>().AddCuisine(takeDye))
                    takeDye = null;
            }
        }
        else
        {
            takeDye.Mounting(null);
            takeDye = null;
        }
    }
}

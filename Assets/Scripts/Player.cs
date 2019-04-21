using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerId;
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
        if (Input.GetKeyDown(PlayerId == 1 ? KeyCode.B : KeyCode.Keypad1))
        {
            if (takeDye == null)
                Take();
            else
                Pun();
        }
        if (Input.GetKey(PlayerId == 1 ? KeyCode.N : KeyCode.Keypad2) && takeDye == null)
        {

            Work();
        }
    }

    void Work()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.3f, out hit, 1))
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
        float x = Mathf.Round(Input.GetAxis("Player" + PlayerId.ToString() + "Horizontal"));
        float y = Mathf.Round(Input.GetAxis("Player" + PlayerId.ToString() + "Vertical"));

        Vector3 targerPosition = transform.position + new Vector3(x, 0, y);
        rigidbody.MovePosition(Vector3.MoveTowards(transform.position, targerPosition, speed * Time.deltaTime));
        transform.LookAt(targerPosition, Vector3.up);
    }

    void Take()
    {

        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.3f, out hit, 1))
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
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.3f, transform.TransformDirection(Vector3.forward), 1);


        int index = -1;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform == takeDye.transform || transform == hits[i].transform)
                continue;

            index = i;
            break;
        }

        if (index != -1)
        {
            RaycastHit hit = hits[index];
            if (hit.transform.CompareTag("DyeCube"))
            {
                DyeCube cube = hit.transform.GetComponent<DyeCube>();
                if (cube.Put(takeDye))
                    takeDye = null;
            }
            if (hit.transform.CompareTag("DyeObject"))
            {
                DyeObject dyeHit = hit.transform.GetComponent<DyeObject>();
                if (dyeHit.type == DyeType.Pot && (dyeHit as DyePot).Fusion(takeDye))
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

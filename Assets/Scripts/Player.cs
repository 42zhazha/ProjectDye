using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerId;
    new Rigidbody rigidbody;
    [SerializeField] Transform TakePoint;
    [SerializeField] Animator animator;
    DyeObject _takeDye;
    DyeObject TakeDye
    {
        get { return _takeDye; }
        set
        {
            _takeDye = value;
            animator.SetBool("IsHolding", _takeDye != null);
        }
    }

    [SerializeField] Transform toolPoint;


    float speed = 5;
    // Use this for initialization
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    ToolTable toolTable;
    // Update is called once per frame
    void Update()
    {
        MoveUpdate();
        if (Input.GetButtonDown("Player" + PlayerId.ToString() + "Button4"))
        {
            if (TakeDye == null)
                Take();
            else
                Pun();
        }

        if (Input.GetButton("Player" + PlayerId.ToString() + "Button3"))
        {
            Work();
        }
        else
        {
            if (this.toolTable != null)
            {
                this.toolTable.PutBackTool(toolPoint.GetChild(1).gameObject);
                this.toolTable = null;
            }
            animator.SetFloat("WorkAction", 0f);
        }
    }

    void Work()
    {
        Ray ray = new Ray(transform.position - new Vector3(0, 0.25f, 0), transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.3f, out hit, 1))
        {
            if (hit.transform.CompareTag("DyeCube"))
            {
                ToolTable toolTable = hit.transform.GetComponent<ToolTable>();
                if (toolTable != null && toolTable.CanWork)
                {
                    if (toolTable != this.toolTable)
                    {
                        if (this.toolTable != null)
                        {
                            this.toolTable.PutBackTool(toolPoint.GetChild(1).gameObject);
                        }
                        this.toolTable = toolTable;
                        GameObject tool = toolTable.PickUpTool();
                        tool.transform.SetParent(toolPoint);
                        tool.transform.localPosition = Vector3.zero;
                        tool.transform.localEulerAngles = Vector3.zero;
                    }

                    string action = toolTable.Work();
                    transform.LookAt(new Vector3(toolTable.transform.position.x, transform.position.y, toolTable.transform.position.z));
                    switch (action)
                    {
                        case "Pestle":
                            animator.SetFloat("WorkAction", 0.5f);
                            break;
                        case "Chop":
                            animator.SetFloat("WorkAction", 1f);
                            break;
                    }
                }
            }
        }
    }

    void MoveUpdate()
    {
        float x = Mathf.Round(Input.GetAxis("Player" + PlayerId.ToString() + "Horizontal"));
        float y = Mathf.Round(Input.GetAxis("Player" + PlayerId.ToString() + "Vertical"));
        if (x == 0 && y == 0)
        {
            animator.SetBool("IsMove", false);
        }
        else
        {
            animator.SetBool("IsMove", true);
            Vector3 targerPosition = transform.position + new Vector3(x, 0, y);
            rigidbody.MovePosition(Vector3.MoveTowards(transform.position, targerPosition, speed * Time.deltaTime));
            transform.LookAt(targerPosition, Vector3.up);
        }
    }

    void Take()
    {
        Ray ray = new Ray(transform.position - new Vector3(0, 0.25f, 0), transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.3f, out hit, 1))
        {
            if (hit.transform.CompareTag("DyeCube"))
            {
                TakeDye = hit.transform.GetComponent<DyeCube>().Take();
                if (TakeDye != null)
                {
                    TakeDye.Mounting(TakePoint);
                    TakeDye.transform.localEulerAngles = Vector3.zero;
                }
            }
            else if (hit.transform.CompareTag("DyeObject"))
            {
                TakeDye = hit.transform.GetComponent<DyeObject>();
                TakeDye.Mounting(TakePoint);
                TakeDye.transform.localEulerAngles = Vector3.zero;
            }
        }
    }

    void Pun()
    {
        Ray ray = new Ray(transform.position - new Vector3(0, 0.25f, 0), transform.TransformDirection(Vector3.forward));
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 0.3f, transform.TransformDirection(Vector3.forward), 1);


        int index = -1;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform == TakeDye.transform || transform == hits[i].transform)
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
                if (cube.Put(TakeDye))
                    TakeDye = null;
            }
            if (hit.transform.CompareTag("DyeObject"))
            {
                DyeObject dyeHit = hit.transform.GetComponent<DyeObject>();
                if (dyeHit.type == DyeType.Pot && (dyeHit as DyePot).Fusion(TakeDye))
                    TakeDye = null;
            }
        }
        else
        {
            TakeDye.Mounting(null);
            TakeDye = null;
        }
    }
}

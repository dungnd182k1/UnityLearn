using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariaController : MonoBehaviour
{

    public int num = 0;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeNum();
        }
    }

    void ChangeNum()
    {
        num += 1;
        if (num >= 3) num = 0;
        animator.SetInteger("num", num);
    }
}

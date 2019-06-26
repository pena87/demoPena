﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float fuerzaSalto = 100f;
    private bool enSuelo = true;
    public Transform comprobadorSuelo;
    float comprobadorRadio = 0.07f;
    public LayerMask mascaraSuelo;
    private bool dobleSalto = false;
    private bool corriendo = false;

    private Animator animator;
    public float velocidad = 1f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}

    void FixedUpdate()
    {
        if (corriendo)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(velocidad, GetComponent<Rigidbody2D>().velocity.y);
            
        }
        animator.SetFloat("VelX", GetComponent<Rigidbody2D>().velocity.x);
        enSuelo = Physics2D.OverlapCircle(comprobadorSuelo.position,comprobadorRadio,mascaraSuelo);
        animator.SetBool("TocandoSuelo", enSuelo);
        if (enSuelo)
        {
            dobleSalto = false;
        }
            
    }

    // Update is called once per frame
    void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            if (corriendo) {
                if (enSuelo || !dobleSalto)
                {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, fuerzaSalto);
                    if (!dobleSalto && !enSuelo)
                    {
                        dobleSalto = true;
                    }
                }
            }else
            {
                corriendo = true;
                NotificationCenter.DefaultCenter().PostNotification(this, "go");
            }
        }
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidesTrigger : MonoBehaviour
{
    public SlideController slide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var slideController = other.gameObject.GetComponent<SlideController>();
        slideController.Show();
    }
}

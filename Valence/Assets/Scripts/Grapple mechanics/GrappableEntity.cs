﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappableEntity : MonoBehaviour
{
    public bool isGrappled = false;
    // Start is called before the first frame update
    public static readonly HashSet<GrappableEntity> Entities = new HashSet<GrappableEntity>();
    void Start() {
        Entities.Add(this);
    }

    public void SetGrappled(bool set) {
        isGrappled = set;
    }
    public bool GetGrappled() {
        return isGrappled;
    }
    private void OnDestroy() {
        Entities.Remove(this);
    }
}

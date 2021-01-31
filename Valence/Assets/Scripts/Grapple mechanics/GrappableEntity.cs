using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappableEntity : MonoBehaviour
{
    // Start is called before the first frame update
    public static readonly HashSet<GrappableEntity> Entities = new HashSet<GrappableEntity>();
    void Start()
    {
        Entities.Add(this);
    }

    private void OnDestroy()
    {
        Entities.Remove(this);
    }
}

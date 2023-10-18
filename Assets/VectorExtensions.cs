using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 To2D(this Vector3 v) => new(v.x, v.y);
}
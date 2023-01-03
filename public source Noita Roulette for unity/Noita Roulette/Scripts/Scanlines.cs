using System;
using UnityEngine;

namespace PulseMotion.HighTechUI
{
    public class Scanlines : MonoBehaviour
    {
        public Material[] _materials = new Material[0];
        
        void Update()
        {
            if (_materials != null)
            {
                foreach (var m in _materials)
                {
                    if (m != null)
                    {
                        m.SetFloat("_UnscaledTime", Time.unscaledTime);
                    }
                }
            }
        }
    }
}

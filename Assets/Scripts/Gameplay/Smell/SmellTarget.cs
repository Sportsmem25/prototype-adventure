using System.Collections.Generic;
using UnityEngine;

public class SmellTarget : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;
    [SerializeField] private Material glowMaterial;

    private void Awake()
    {
        SetVisible(false);
    }

    public void SetVisible(bool isActive)
    {
        foreach (var r in renderers)
        {
            if (r == null) continue;

            List<Material> currentMaterials = new List<Material>();
            r.GetSharedMaterials(currentMaterials);
            if (isActive)
            {
                if (!currentMaterials.Contains(glowMaterial))
                    currentMaterials.Add(glowMaterial);
            }
            else
                currentMaterials.Remove(glowMaterial);
            r.materials = currentMaterials.ToArray();
        }
    }

    //[SerializeField] private Renderer[] renderers;

    //private void Awake()
    //{
    //    SetVisible(false);
    //}

    //public void SetVisible(bool value)
    //{
    //    foreach (var r in renderers)
    //    {
    //        r.enabled = value;
    //    }
    //}
}
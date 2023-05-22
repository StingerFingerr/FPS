using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRandomSkin: MonoBehaviour
{
    public List<SkinnedMeshRenderer> meshes;
    public List<Material> materials;

    private void Start()
    {
        var mesh = meshes[Random.Range(0, meshes.Count)];
        mesh.gameObject.SetActive(true);
        mesh.material = materials[Random.Range(0, materials.Count)];
    }
}
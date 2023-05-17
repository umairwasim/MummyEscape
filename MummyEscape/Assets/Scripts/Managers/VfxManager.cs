using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public static VfxManager Instance;

    public GameObject damageVfx;
    public GameObject levelWonVfx;
    public GameObject levelFailedVfx;

    private void Awake()
    {
        Instance = this;
    }

    //Display Vfx
    public void DisplayVfx(GameObject vfxToSpawn, Vector3 position, float delay = 3f)
    {
        GameObject vfx = Instantiate(vfxToSpawn, position, Quaternion.identity);
        Destroy(vfx, delay);
    }
}

using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public void DisableSelf()
    {
        Destroy(gameObject, 1f);
    }
}

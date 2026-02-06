using UnityEngine;

public class PreventCameraRotation : MonoBehaviour
{
    private Quaternion my_rotation;
    void Start()
    {
        my_rotation = this.transform.rotation;
    }
    void Update()
    {
        this.transform.rotation = my_rotation;

    }
}

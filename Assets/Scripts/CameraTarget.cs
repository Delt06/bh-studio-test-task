using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraTarget : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        var cam = Camera.main;
        Assert.IsNotNull(cam);

        var thirdPersonCamera = cam.GetComponent<ThirdPersonCamera>();
        Assert.IsNotNull(thirdPersonCamera);

        Cursor.lockState = CursorLockMode.Locked;
        thirdPersonCamera.BindTo(transform);
    }
}
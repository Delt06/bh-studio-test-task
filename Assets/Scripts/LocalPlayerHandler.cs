using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

public class LocalPlayerHandler : NetworkBehaviour
{
    [SerializeField] private InputProvider _inputProvider;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        var cam = Camera.main;
        Assert.IsNotNull(cam);

        EnableCameraFollow(cam);
        EnableMovementControls(cam);
    }

    private void EnableCameraFollow(Camera cam)
    {
        var thirdPersonCamera = cam.GetComponent<ThirdPersonCamera>();
        Assert.IsNotNull(thirdPersonCamera);

        Cursor.lockState = CursorLockMode.Locked;
        thirdPersonCamera.BindTo(transform);
    }

    private void EnableMovementControls(Camera cam)
    {
        _inputProvider.Init(cam);
    }
}
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

public class LocalPlayerHandler : NetworkBehaviour
{
    [SerializeField] private InputProvider _inputProvider;
    [SerializeField] private Movement _movement;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        var cam = Camera.main;
        Assert.IsNotNull(cam);

        EnableCameraFollow(cam);
        EnableMovement(cam);
    }

    private void EnableCameraFollow(Camera cam)
    {
        var thirdPersonCamera = cam.GetComponent<ThirdPersonCamera>();
        Assert.IsNotNull(thirdPersonCamera);

        Cursor.lockState = CursorLockMode.Locked;
        thirdPersonCamera.BindTo(transform);
    }

    private void EnableMovement(Camera cam)
    {
        _inputProvider.Init(cam);
        _movement.enabled = true;
    }
}
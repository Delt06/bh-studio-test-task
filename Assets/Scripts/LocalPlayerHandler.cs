using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

public class LocalPlayerHandler : NetworkBehaviour
{
    [SerializeField] private InputProvider _inputProvider;
    [SerializeField] private Movement _movement;
    [SerializeField] private Dash _dash;
    [SerializeField] private DashInput _dashInput;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        var cam = Camera.main;
        Assert.IsNotNull(cam);

        EnableCameraFollow(cam);
        EnableMovement(cam);
        EnableDash();
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

    private void EnableDash()
    {
        _dash.enabled = true;
        _dashInput.enabled = true;
    }
}
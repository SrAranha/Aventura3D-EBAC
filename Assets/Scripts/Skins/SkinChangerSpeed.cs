using UnityEngine;

public class SkinChangerSpeed : SkinChangerBase
{
    [Tooltip("To multiply the current speed of the player")]
    public float newSpeedFactor;
    protected override void ChangeSkin()
    {
        base.ChangeSkin();
        _playerController.ChangeSpeed(newSpeedFactor, skinDuration);
    }
}

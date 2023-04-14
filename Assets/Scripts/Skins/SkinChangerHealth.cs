using UnityEngine;

public class SkinChangerHealth : SkinChangerBase
{
    public int healthToAdd;

    protected override void ChangeSkin()
    {
        base.ChangeSkin();
        _playerController.AddHealth(healthToAdd, skinDuration);
    }
}

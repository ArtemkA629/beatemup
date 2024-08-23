using UnityEngine;

public static class PlayerInput
{
    public static Vector3 GetMovement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        return new Vector3(-yInput, 0f, xInput);
    }

    public static bool IsAttacking()
    {
        return Input.GetButtonDown("Fire1");
    }
}

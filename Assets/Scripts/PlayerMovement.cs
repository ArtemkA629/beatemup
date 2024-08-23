using UnityEngine;

public class PlayerMovement : EntityMovement
{
    public PlayerMovement(Transform transform, float speed, EntityAnimator animator, 
        float minXBound, float maxXBound, float minZBound, float maxZBound) : 
        base(transform, speed, animator, minXBound, maxXBound, minZBound, maxZBound) { }

    public override Vector3 GetMovement()
    {
        return PlayerInput.GetMovement();
    }
}

namespace Controllers.AnimationStates
{
    public enum MoveAnimationState
    {
        NoAction = 0,
        Move = 1,
        Run = 2,
        MoveLeft = 3,
        MoveRight = 4,
        MoveForwardLeft = 5,
        MoveForwardRight = 6,
        RunForwardLeft = 7,
        RunForwardRight = 8,
        MoveBackward = -1,
        RunBackward = -2,
        MoveBackwardLeft = -3,
        MoveBackwardRight = -4,
    }
}
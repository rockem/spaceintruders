namespace Intruders.logic
{
    public interface ISpriteLogic : ILogic
    {
        string GetImagePath();

        int XVelocity { get; }

        int YVelocity { get; }
    } 
}
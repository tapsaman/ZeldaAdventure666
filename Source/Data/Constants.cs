using TapsasEngine.Utilities;

namespace ZA6
{
    public enum MapCode
    {
        A1,
        A2,
        B1,
        B2,
        C1
    }

    public enum TransitionType
    {
        Pan,
        FadeToBlack,
        Doorway
    }

    public enum DataStoreType
    {
        Scene,
        Session,
        Game
    }

    public enum CollisionType : ushort
    {
        None,
        Full,
        NorthEast,
        SouthEast,
        SouthWest,
        NorthWest
    }
}
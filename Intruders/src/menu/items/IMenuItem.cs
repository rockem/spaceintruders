namespace Intruders.menu.items
{
    internal interface IMenuItem
    {
        string ItemText { get; }

        void Select();

        void RiseValue();

        void LowerValue();
    }
}
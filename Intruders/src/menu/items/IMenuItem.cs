namespace Intruders.menu.items
{
    interface IMenuItem
    {
        void Select();

        void RiseValue();

        void LowerValue();

        string ItemText { get; }
    }
}
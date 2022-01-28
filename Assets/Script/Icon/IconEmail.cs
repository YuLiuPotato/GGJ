namespace Script.Icon
{
    public class IconEmail : InteractiveIcon
    {
        public override void Activate()
        {
            UIManager.Instance.OpenEmailUI();
        }
    }
}
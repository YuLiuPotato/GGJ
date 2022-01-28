namespace Script.Icon
{
    public class IconFolder : InteractiveIcon
    {
        public override void Activate()
        {
            UIManager.Instance.OpenFolderUI();
        }
    }
}
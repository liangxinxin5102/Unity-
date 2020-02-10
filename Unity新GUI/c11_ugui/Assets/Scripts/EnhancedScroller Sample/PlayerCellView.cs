
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;

public class PlayerCellView : EnhancedScrollerCellView
{
    public Text nameText;
    public Text sexText;
    public void SetData(PlayerData data)
    {
        nameText.text = data.mName;
        sexText.text = data.mSex;
    }
}
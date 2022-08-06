using System.Collections.Generic;

public abstract class BoosterPack : IDescription
{
    public string Name { get; set; }
    public int Cost { get; set; }

    public List<BoosterBucket> buckets = new List<BoosterBucket>();
    public List<UnlockCondition> unlockConditions = new List<UnlockCondition>();

    public bool CanOpen()
    {
        if(Cost > ResourceManager.instance.Coins) return false;

        foreach(UnlockCondition unlockCondition in unlockConditions)
        {
            if(!unlockCondition.Unlocked) return false;
        }

        return true;
    }

    public List<Card> OpenPack()
    {
        List<Card> toReturn = new List<Card>();

        foreach(BoosterBucket bucket in buckets)
        {
            toReturn.AddRange(bucket.Open());
        }

        return toReturn;
    }

    public string GetDescription()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine(Name);
        sb.AppendLine(Cost + " Coins");

        return sb.ToString();
    }
}

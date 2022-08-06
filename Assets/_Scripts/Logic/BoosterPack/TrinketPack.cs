public class TrinketPack : BoosterPack
{
    public TrinketPack()
    {
        Name = "Gidgets and Gazmos";
        Cost = 10;

        BoosterBucket commonBucket = new BoosterBucket();

        commonBucket.openCount = 5;

        BoosterBucket rareBucket = new BoosterBucket();

        rareBucket.openCount = 1;


        buckets.Add(commonBucket);
        buckets.Add(rareBucket);
    }
}

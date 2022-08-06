using System.Collections.Generic;
using System.Text;

public class GameBoard : ITarget
{
    public int turn = 0;
    public int energy = 0;
    public int maxEnergy = 1;
    public int startingFields = 1;
    
    public List<Effect> effects = new List<Effect>();
    public List<Permanent> permanents = new List<Permanent>();
    public List<Resource> resources = new List<Resource>();
    public List<Field> fields = new List<Field>();
    public List<GameProperty> properties = new List<GameProperty>();

    public string Summary()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Turn: " + turn);

        foreach(Resource resource in resources)
        {
            sb.AppendLine(resource.resourceType.ToString() + ": " + resource.count);
        }

        sb.AppendLine("Effects: " + effects.Count);
        sb.AppendLine("Permanents: " + permanents.Count);
        
        return sb.ToString();
    }

    public int GetResource(ResourceType resourceType)
    {
        foreach(Resource resource in resources)
        {
            if(resource.resourceType == resourceType) 
                return resource.count;
            
        }

        return 0;
    }

    public void AddEnergy(int value)
    {
        energy += value;
    }

    public void SetEnergy(int value)
    {
        energy = value;
    }

    public void PayEnergy(int value)
    {
        energy -= value;
    }

    public void AddResource(Resource resource)
    {
        for(int i = 0; i < resources.Count; i++)
        {
            Resource current = resources[i];

            if(current.resourceType != resource.resourceType) continue;

            current.count += resource.count;

            resources[i] = current;
            return;
        }

        resources.Add(resource);
    }

    public void SetResource(Resource resource)
    {
        for(int i = 0; i < resources.Count; i++)
        {
            Resource current = resources[i];

            if(current.resourceType != resource.resourceType) continue;

            current = resource;

            resources[i] = current;
            return;
        }

        resources.Add(resource);

    }

    public void PayResource(Resource resource)
    {
        for(int i = 0; i < resources.Count; i++)
        {
            Resource current = resources[i];

            if(current.resourceType != resource.resourceType) continue;

            current.count -= resource.count;

            resources[i] = current;

            if(resources[i].count == 0) resources.RemoveAt(i);
            
            return;
        }
    }
}

public struct PlayPackage
{
    public GameBoard gameBoard;
    public GameBus gameBus;
    public Deck deck;
    public Hand hand;
    public DiscardPile discardPile;
}

public interface ITarget
{

}
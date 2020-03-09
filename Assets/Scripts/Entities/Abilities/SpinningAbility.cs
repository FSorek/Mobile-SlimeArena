using UnityEngine;

public class SpinningAbility : IAbility
{
    private readonly Transform owner;
    private readonly BoxCollider2D[] abilityWalls = new BoxCollider2D[4];
    private readonly Camera mainCamera;
    private readonly Attack attack;
    private Transform wallParent;
    public float TickTime { get; }
    public int Cost { get; }

    public SpinningAbility(Transform owner, float tickTime, int cost, int damage, Vector2 hitSize)
    {
        this.owner = owner;
        TickTime = tickTime;
        Cost = cost;
        mainCamera = Camera.main;
        attack = new MeleeSlash(damage, hitSize, owner.gameObject);
        
        InitializeAbilityWalls();
        SetWallsActive(false);
    }
    public void StartedCasting()
    {
        SetWallsActive(true);
    }

    public void Tick()
    {
        attack.Create(owner.transform.position, Vector2.zero);
    }

    public void FinishedCasting()
    {
        SetWallsActive(false);
    }

    private void SetWallsActive(bool active)
    {
        wallParent.SetParent( active ? null : owner, true);
        if (!active) wallParent.position = owner.position;
        
        foreach (var wall in abilityWalls)
        {
            wall.gameObject.SetActive(active);
        }
    }
    
    private void InitializeAbilityWalls()
    {
        var verticalOffset = mainCamera.orthographicSize;
        var horizontalOffset = mainCamera.orthographicSize * mainCamera.aspect;
        wallParent = new GameObject("WallParent").transform;
        wallParent.position = owner.transform.position;
        wallParent.SetParent(owner);
        for (int i = 0; i < 4; i++)
        {
            var wall = new GameObject("Wall");
            wall.transform.position = wallParent.position;
            wall.transform.SetParent(wallParent);
            abilityWalls[i] = wall.AddComponent<BoxCollider2D>();
            if(i % 2 == 0)
                abilityWalls[i].size = new Vector2(1, 2 * verticalOffset);
            else
                abilityWalls[i].size = new Vector2(2 * horizontalOffset, 1);
        }

        abilityWalls[0].transform.localPosition = new Vector3(horizontalOffset,0,0);
        abilityWalls[1].transform.localPosition = new Vector3(0,verticalOffset,0);
        abilityWalls[2].transform.localPosition = new Vector3(-horizontalOffset,0,0);
        abilityWalls[3].transform.localPosition = new Vector3(0,-verticalOffset,0);
    }
}
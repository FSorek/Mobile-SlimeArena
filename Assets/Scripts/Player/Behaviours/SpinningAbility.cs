using UnityEngine;

public class SpinningAbility : IAbility
{
    private readonly BoxCollider2D[] abilityWalls = new BoxCollider2D[4];
    private readonly Camera mainCamera;
    public float TickTime { get; }
    public float Cost { get; }

    public SpinningAbility(float tickTime, float cost)
    {
        TickTime = tickTime;
        Cost = cost;
        mainCamera = Camera.main;
        
        InitializeAbilityWalls();
    }
    public void StartedCasting()
    {
        SetWallsActive(true);
    }

    public void Tick()
    {
        
    }

    public void FinishedCasting()
    {
        SetWallsActive(false);
    }

    private void SetWallsActive(bool active)
    {
        foreach (var wall in abilityWalls)
        {
            wall.gameObject.SetActive(active);
        }
    }
    
    private void InitializeAbilityWalls()
    {
        var verticalOffset = mainCamera.orthographicSize;
        var horizontalOffset = mainCamera.orthographicSize * mainCamera.aspect;
        for (int i = 0; i < 4; i++)
        {
            var wall = new GameObject("Wall");
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
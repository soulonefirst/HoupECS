using Unity.Entities;


public class WaitingManager : SystemBase
{
    public static bool wait;
    protected override void OnUpdate()
    {

            Entities
                .WithoutBurst()
                .ForEach((ref Wait entitiesWait) =>
                {

                    entitiesWait.Value = wait;


                }).Run();
    }
}

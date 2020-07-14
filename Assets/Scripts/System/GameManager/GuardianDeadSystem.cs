
using Unity.Entities;


public class GuardianDeadSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .ForEach((in GuardianDeadData guardianDead) =>
            {
                if(guardianDead.Value == 2)
                {
                    WaitingManager.wait = true;
                    Enabled = false;
                }

            }).Run();
    }
        
}
  

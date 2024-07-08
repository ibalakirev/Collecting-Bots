using UnityEngine;

public class SpawnerFlags : Spawner<FlagsPool> 
{
    public Flag Create(Vector3 positoin)
    {
        Flag flag = ObjectsPool.GetObject(positoin, transform.localRotation);

        flag.Released += ReleaseFlag;

        return flag;
    }

    private void ReleaseFlag(Flag flag)
    {
        flag.Released -= ReleaseFlag;

        flag.transform.SetParent(null);

        ObjectsPool.ReturnObject(flag);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "BusPrefabs", menuName = "Bus/BusPrefabs")]
public class BusPrefabs : ScriptableObject
{
    public GameObject sixx3Prefab;
    public GameObject sixx4Prefab;
    public GameObject sevenx5Prefab;
    public GameObject eightx4Prefab;
    public GameObject eightx5Prefab;
    public GameObject tenx5Prefab;
    public GameObject tenx6Prefab;
    public GameObject thirdteenx7Prefab;
    public GameObject GetPrefab(BusType busType)
    {
        switch (busType)
        {
            case BusType.Sixx3:
                return sixx3Prefab;
            case BusType.Sixx4:
                return sixx4Prefab;
            case BusType.Sevenx5:
                return sevenx5Prefab;
            case BusType.Eightx4:
                return eightx4Prefab;
            case BusType.Eightx5:
                return eightx5Prefab;
            case BusType.Tenx5:
                return tenx5Prefab;
            case BusType.Tenx6:
                return tenx6Prefab;
            case BusType.Thirdteenx7:
                return thirdteenx7Prefab;

            default:
                return null;
        }
    }
}


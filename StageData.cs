using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage")]
public class StageData : ScriptableObject
{
    [SerializeField]
    private GameObject stagePrefab;

    [SerializeField]
    private int maxActions;

    [SerializeField]
    private Vector3 playerStartPosition;

    public GameObject StagePrefab => stagePrefab;
    public int MaxActions => maxActions;
    public Vector3 PlayerStartPosition => playerStartPosition;
}
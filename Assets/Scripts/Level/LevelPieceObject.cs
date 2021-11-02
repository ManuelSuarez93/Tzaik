using UnityEngine;

namespace Tzaik.Level
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelPieceObject", order = 1)]
    public class LevelPieceObject : ScriptableObject
    {
        public LevelPiece LevelPiece; 
    }

    public enum Area
    {
        Temple = 1,
        CrystalCave = 2,
        Jungle = 3
    }

    public enum LevelPieceType
    {
        Room = 1,
        Hallway = 2,
        Wall = 3
    }
    
}

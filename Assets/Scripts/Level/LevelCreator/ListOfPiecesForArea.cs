using System.Collections.Generic;
using UnityEngine;

namespace Tzaik.Level
{
    public class ListOfPiecesForArea : MonoBehaviour
    {
        [SerializeField] List<LevelPieceObject> rooms;
        [SerializeField] List<LevelPieceObject> hallways;
        [SerializeField] List<LevelPieceObject> walls;

        public List<LevelPieceObject> Rooms => rooms;
        public List<LevelPieceObject> Hallways => hallways; 
        public List<LevelPieceObject> Walls  => walls; 
    }
}

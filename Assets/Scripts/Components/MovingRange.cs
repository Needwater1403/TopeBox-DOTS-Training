using Unity.Entities;

namespace Components
{
    public partial struct MovingRange:IComponentData
    {
        public float minX;
        public float maxX;
        public float minY;  
    }
}
namespace Assets.Scripts.Logic
{
    public interface IWeapon : IItem
    {
        /// <summary> Подразумевается удар рукой. </summary>
        public bool IsMartial { get; }

        public bool IsProjectile { get; }

        public IItem AmmoType { get; }

        public int Damage { get; }

        public float MaxDistance { get; }
    }
}

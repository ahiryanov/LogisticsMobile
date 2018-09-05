namespace LogisticsMobile
{
    public partial class Equipment
    {
        public int IDEquipment { get; set; }
        public int IDModel { get; set; }
        public string HealthState { get; set; }
        public string PositionState { get; set; }
        public string AssignedPosition { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string ISNumber { get; set; }
        public int? IDRent { get; set; }

        public virtual Model Model { get; set; }
    }
}

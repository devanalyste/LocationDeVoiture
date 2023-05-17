namespace LocationDeVoiture.ClassesBD
{
    internal class TermesLocation
    {
        public int idTerme { get; set; }
        public int nbAnnee { get; set; }
        public decimal kmMax { get; set; }
        public decimal surprime { get; set; }

        public string termeString => $"{idTerme} {nbAnnee} {kmMax} {surprime}";
    }
}

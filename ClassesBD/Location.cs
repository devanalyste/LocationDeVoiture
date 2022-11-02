namespace LocationDeVoiture.ClassesBD
{
    public class Location
    {
        private string niv1;

        public int idLocation { get; set; }
        public DateTime debutLoc { get; set; }
        public int contratNbrMois { get; set; }
        public DateTime finContrat { get; set; }
        public DateTime premPaiement { get; set; }
        public int nbrPaieMens { get; set; }
        public decimal mntPaieMens { get; set; }
        public decimal nbrKmDebut { get; set; }
        public decimal? nbrKmFin { get; set; }
        public int? idClient { get; set; }
        public string niv { get => niv1; set => niv1 = value; }
        public int? idTerme { get; set; }


    }

}

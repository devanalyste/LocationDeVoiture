using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LocationDeVoiture
{

    public partial class MainForm : Form
    {
        //INITIALISATION DE SQLCOMMAND
        private SqlCommand cmd = new();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //MÉTHODE DE REMPLISSAGE DES 3 DATAGRIDS SUR LA 
            //PAGE D'ACCUEIL
            GridBind();
            //AFFICHER LE LISTVIEW EN LOCATION DE LA PAGE "AJOUT DE LOCATION"
            AfficheDonneListView();
            //AFFICHER LE LISTVIEW INVENTAIRE DE LA PAGE "AJOUT DE LOCATION"
            AfficherInventaire();
            //REMPLISSAGE DU COMBOBOX AVEC LES DONNÉES
            //DE LA TABLE "TERME DE LOCATION" AVEC UNE CONCATENATION
            //DE TEXTE
            remplirCmbTerme();
            //TIMER POUR AFFICHER L'HEURE AVEC LES SECONDES POUR UNE
            //FACILITÉ D'ACCES AUX EMPLOYÉS TRAVAILLANT AVEC L'APPLICATION
            timer1.Start();

        }
        //TIMER POUR AFFICHER L'HEURE AVEC LES SECONDES
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBoxDate.Text = DateTime.Now.ToString();
        }
        #region tab1
        //MÉTHODE DE REMPLISSAGE DES 3 DATAGRIDS SUR LA 
        //PAGE D'ACCUEIL
        private void GridBind()
        {
            //STRING DE CONNEXION À LA BASE DE DONNÉES
            string connect = ConfigurationManager.ConnectionStrings["connexion"].ConnectionString;
            using SqlConnection con = new(connect);

            //VIDAGE DES DATAGRIDVIEW POUR UNE MISE À JOUR
            //AVANT LE REMPLISSAGE DES POSSIBLES NOUVELLES DONNÉES
            dgv_location.Rows.Clear();
            dgv_Vehicule.Rows.Clear();
            dgv_Vehicule.Rows.Clear();

            //CONEXION
            con.Open();

            //COMMANDE D'INSERTION DATAGRID TABLE LOCATION
            SqlCommand cmdLocation = new(
                "SELECT Location.[idLocation] ID," +
                "Location.[debutLoc] 'Date de location'," +
                "Location.[contratNbrMois] 'Mois', " +
                "Location.[finContrat] 'Date fin', " +
                "Location.[premPaiement] 'Date premier paiement', " +
                "Location.[qte.paie/annee] 'paie/année', " +
                "Location.[mntPaieMens] 'Montant des paiements', " +
                "Location.[nbrKmDebut] 'Km départ', " +
                "Location.[nbrKmFin] 'Km retour', " +
                "Vehicule.[niv] 'Niv véhicule', " +
                "Client.[prenom] 'Prénom client', " +
                "Client.[nom] 'Nom Client', " +
                "Client.[telephone] 'Téléphone client'" +
                "FROM [dbo].[Location]" +
                "INNER JOIN [dbo].[Vehicule] ON [Vehicule].[niv]=[Location].[niv]" +
                "INNER JOIN [dbo].[Client] ON [Client].[idClient]=[Location].[idClient]",
                con);


            SqlDataReader dr = cmdLocation.ExecuteReader(); //LECTEUR DE LA COMMANDE POUR LA TABLE LOCATION
            DataTable dt = new();                   //CRÉATION DE LA DATATABLE
            dt.Load(dr);                              //REMPLISSAGE DE LA DATATABLE AVEC LA VARIABLE DU LECTEUR
            dgv_location.DataSource = dt;                   //AFFICHAGE DES DONNÉES DANS LE DATATABLE

            //COMMANDE D'INSERTION DATAGRID TABLE VÉHICULE
            SqlCommand cmdVehicule = new("SELECT DISTINCT Vehicule.[niv]," +
                "Vehicule.[annee]'Année'," +
                "Vehicule.[valeur]'Valeur'," +
                "Vehicule.[transmission]'Transmission'," +
                "Vehicule.[climatiseur]'Climatiseur'," +
                "Modele.[model]'Model'," +
                "Type.[type]'Type'," +
                "Couleur.[couleur]'Couleur'," +
                "Vehicule.[antiDemarreur]'Anti-démarreur'," +
                "Location.[idLocation]'Id location'" +
                "FROM [dbo].[Vehicule]" +
                "INNER JOIN [dbo].[Type] ON [Type].[idType]=[Vehicule].[idType]" +
                "INNER JOIN [dbo].[Couleur] ON [Couleur].[idCouleur]=[Vehicule].[idCouleur]" +
                "INNER JOIN [dbo].[Location] ON [Location].[niv]=[Vehicule].[niv]" +
                "INNER JOIN [dbo].[Modele] ON [Modele].[idModel]=[Vehicule].[idModel]," +
                "[dbo].[Client]", con);


            SqlDataReader dr2 = cmdVehicule.ExecuteReader();//LECTEUR DE LA COMMANDE POUR LA TABLE VEHICULE
            dt = new();                             //CRÉATION DE LA DATATABLE
            dt.Load(dr2);                            //REMPLISSAGE DE LA DATATABLE AVEC LA VARIABLE DU LECTEUR
            dgv_Vehicule.DataSource = dt;                   //AFFICHAGE DES DONNÉES DANS LE DATATABLE

            //COMMANDE D'INSERTION DATAGRID TABLE CLIENT
            SqlCommand cmdClient = new("SELECT Client.[idClient]," +
                "Client.[prenom]'Prénom'," +
                "Client.[nom]'Nom'," +
                "Client.[adresse]'Adresse'," +
                "Client.[ville]'Ville'," +
                "Client.[province]'Province(initiale)'," +
                "Client.[codePostal]'Code Postal'," +
                "Client.[telephone]'Téléphone'," +
                "Client.[date_naissance]'Date de naissance'," +
                "Paiement.[idPaiement]'Id paiement'," +
                "Paiement.[date]'Date Paiement'," +
                "Paiement.[montant]'Montant'," +
                "Paiement.[idLocation]'Id Location' " +
                "FROM [dbo].[Client]" +
                "INNER JOIN [dbo].[Paiement] " +
                "ON [Paiement].[idClient]=[Client].[idClient]", con);

            SqlDataReader dr3 = cmdClient.ExecuteReader();//LECTEUR DE LA COMMANDE POUR LA TABLE CLIENT
            dt = new();                           //CRÉATION DE LA DATATABLE
            dt.Load(dr3);                           //REMPLISSAGE DE LA DATATABLE AVEC LA VARIABLE DU LECTEUR
            dgv_client.DataSource = dt;                   //AFFICHAGE DES DONNÉES DANS LE DATATABLE

            //AFFECTATION DES FORMATS DE DONNÉES
            //DANS LES 3 DATAGRIDS
            FormatCellGrid(dr);
            FormatCellGrid(dr2);
            FormatCellGrid(dr3);

            //FERMETURE DE LA CONNEXION
            con.Close();
        }

        //METHODE POUR AFFICHER LES DONNÉES EN UN FORMAT SPÉCIFIQUE
        //COMME LES NUMÉROS DE TÉLÉPHONES ET LES MONTANTS...
        private void FormatCellGrid(object e)
        {
            //SI LES DONNÉES NE SONT PAS NULL...
            if (!e.Equals(null))
            {
                //FORMAT D'AFFICHAGE MONÉTAIRE ET DATE
                dgv_location.Columns["Date de location"].DefaultCellStyle.Format = "d";
                dgv_location.Columns["Date fin"].DefaultCellStyle.Format = "d";
                dgv_location.Columns["Montant des paiements"].DefaultCellStyle.Format = "c";
                dgv_Vehicule.Columns["Valeur"].DefaultCellStyle.Format = "c";
                dgv_client.Columns["Montant"].DefaultCellStyle.Format = "c";

                //FORMAT COLONNE DU NIV DU DATAGRID LOCATION
                dgv_location.Columns["Niv véhicule"].DefaultCellStyle.Format = dgv_Vehicule.Columns.ToString().Length
                    switch
                {
                    21 => "#####-#####-#####-######",
                    20 => "#####-#####-#####-#####",
                    _ => "####-#####-#####-#####",
                };
                //FORMAT COLONNE DU NIV DU DATAGRID VEHICULE
                dgv_Vehicule.Columns["niv"].DefaultCellStyle.Format = dgv_Vehicule.Columns.ToString().Length
                    switch
                {
                    21 => "#####-#####-#####-######",
                    20 => "#####-#####-#####-#####",
                    _ => "####-#####-#####-#####",
                };
                //FORMAT DES NUMÉROS DE TÉLÉPHONES DU DATAGRID CLIENT
                dgv_client.Columns["Téléphone"].DefaultCellStyle.Format = dgv_Vehicule.Columns.ToString().Length

                    switch
                {
                    7 => "###-####",
                    _ => "(###) ###-####",
                };
                //FORMAT DES NUMÉROS DE TÉLÉPHONES DU DATAGRID LOCATION
                dgv_location.Columns["Téléphone client"].DefaultCellStyle.Format = dgv_location.Columns.ToString().Length switch
                {
                    7 => "###-####",
                    _ => "(###) ###-####",
                };
            }
        }
        #endregion tab1

        #region tab2

        // TAB AJOUT LOCATION ********************************************************************************************
        private void AfficherInventaire()
        {
            SqlDataAdapter da;
            DataSet ds;
            DataTable dt;

            lst_inventaire.Columns.Add("Niv véhicule", -1, HorizontalAlignment.Center);
            lst_inventaire.Columns.Add("Année", -1, HorizontalAlignment.Center);
            lst_inventaire.Columns.Add("Valeur", -1, HorizontalAlignment.Center);
            lst_inventaire.Columns.Add("Transmission", -1, HorizontalAlignment.Center);
            lst_inventaire.Columns.Add("Climatiseur", -1, HorizontalAlignment.Center);
            lst_inventaire.Columns.Add("Anti-Démarreur", -1, HorizontalAlignment.Center);
            lst_inventaire.Columns.Add("Model", -1, HorizontalAlignment.Center);
            lst_inventaire.Columns.Add("Type", -1, HorizontalAlignment.Center);
            lst_inventaire.Columns.Add("Couleur", -1, HorizontalAlignment.Center);
            //lst_inventaire.Columns.Add("Niv véhicule", -1, HorizontalAlignment.Center);

            //STRING DE CONNEXION
            string connect = ConfigurationManager.ConnectionStrings["connexion"].ConnectionString;
            using SqlConnection con = new(connect);

            con.Open();
            cmd = new SqlCommand("SELECT * FROM PasEnLocation", con);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds, "PasEnLocation");
            dt = ds.Tables["PasEnLocation"];
            con.Close();
            //LISTEVIEW INVENTAIRE----------------------------------------------------------------------------------------
            int i;
            //...DANS CHAQUE COLONNES...
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {

                //INITIALISATION DU LISTVIEW AVEC LA STRING DE SÉLECTION
                //EN MENTIONNANT LE FORMAT TEXT DES DONNÉES DE LA STRING DE SÉLECTION 

                lst_inventaire.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                lst_inventaire.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                lst_inventaire.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                lst_inventaire.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                lst_inventaire.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                lst_inventaire.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                lst_inventaire.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                lst_inventaire.Items[i].SubItems.Add(dt.Rows[i].ItemArray[7].ToString());
                lst_inventaire.Items[i].SubItems.Add(dt.Rows[i].ItemArray[8].ToString());
                lst_inventaire.Items[i].SubItems.Add(dt.Rows[i].ItemArray[9].ToString());

                //lvi.SubItems.Add(lecteurInv[9].ToString());

                //lst_inventaire.Items.Add(lvi);
                lst_inventaire.View = View.Details;
            }


        }

        //METHODE D'AFFICHADE DE DONNÉES DANS UN LISTVIEW
        private void AfficheDonneListView()
        {


            //NETTOYAGE DES DONNÉES DANS LE LISTVIEW
            lstv_Location.Clear();
            //STRING DE CONNEXION
            string connect = ConfigurationManager.ConnectionStrings["connexion"].ConnectionString;
            using SqlConnection con = new(connect);

            //LISTVIEW LOCATION -------------------------------------------------------------------------------------------
            //AJOUT DES NOMS DE COLONNES PERSONNALISÉES LISTVIEW LOCATION
            //ALLIGNEMENT DU CONTENU AU CENTRE
            lstv_Location.Columns.Add("ID", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Début Location", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Nmb de mois", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Date fin", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Date Premier paiement", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Qte.Paiem/année", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("$ Paiem/mois", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Km au départ", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Km à au retour", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Id Client", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Niv du véhicule", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Id Terme", -1, HorizontalAlignment.Center);
            lstv_Location.Columns.Add("Id Paiement");

            //STRING DE SÉLECTION POUR LES DONNÉES
            string selectLocation = "SELECT DISTINCT * FROM Location";
            //INITIALISATION DU SQLCOMMAND AVEC PARAMETRES
            SqlCommand cmdLoc = new(selectLocation, con);
            //OUVERTURE DE LA CONNEXION
            con.Open();
            //CREATION DU LECTEUR
            SqlDataReader lecteurLoc = cmdLoc.ExecuteReader();

            //SI LE LECTEUR NE LIT AUCUNE DONNÉES...
            if (lecteurLoc == null)
            {
                MessageBox.Show("Il n'y a aucune données à afficher.\r\nConsultez un administrateur", "ERREUR");
            }
            else
            {
                //TANT QUE LE LECTEUR LIT DES DONNÉES...
                while (lecteurLoc.Read())
                {
                    //...DANS CHAQUE COLONNES...
                    for (int i = 0; i < 1; i++)
                    {

                        //INITIALISATION DU LISTVIEW AVEC LA STRING DE SÉLECTION
                        //EN MENTIONNANT LE FORMAT TEXT DES DONNÉES DE LA STRING DE SÉLECTION 
                        ListViewItem lvi = new()
                        {
                            Text = lecteurLoc[0].ToString()
                        };
                        //...AJOUTE DES DONNÉES DANS LES INDEX(COLONNES)
                        lvi.SubItems.Add(lecteurLoc[1].ToString());
                        lvi.SubItems.Add(lecteurLoc[2].ToString());
                        lvi.SubItems.Add(lecteurLoc[3].ToString());
                        lvi.SubItems.Add(lecteurLoc[4].ToString());
                        lvi.SubItems.Add(lecteurLoc[5].ToString());
                        lvi.SubItems.Add(lecteurLoc[6].ToString());
                        lvi.SubItems.Add(lecteurLoc[7].ToString());
                        lvi.SubItems.Add(lecteurLoc[8].ToString());
                        lvi.SubItems.Add(lecteurLoc[9].ToString());
                        lvi.SubItems.Add(lecteurLoc[10].ToString());
                        lvi.SubItems.Add(lecteurLoc[11].ToString());
                        lvi.SubItems.Add(lecteurLoc[12].ToString());

                        //AFFICHAGE(AJOUT) DES DONNÉES
                        lstv_Location.Items.Add(lvi);
                    }
                    //PARAMETRES DE 'RESIZE' AUTOMATIQUE SELON LES DONNÉES INSÉRÉES
                    //POUR LE 'HEADER' ET LES COLONNES
                    lstv_Location.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    lstv_Location.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            //FERMETURE DE LA CONNEXION
            con.Close();
        }


        //METHODE DE VÉRIFICATION SI LES CHAMPS NE SONT PAS VIDES
        private bool VerifierSaisie()
        {
            bool OK = !(Dt_dateDeLocation.Text.Trim() == string.Empty ||
                        Cmb_moisDeLocation.Text.Trim() == string.Empty ||
                        Dt_datePremierPaiement.Text.Trim() == string.Empty ||
                        Cmb_nbrPaiementParAnne.Text.Trim() == string.Empty ||
                        txt_mntPaieMens.Text.Trim() == string.Empty ||
                        txt_KmDebut.Text.Trim() == string.Empty ||
                        cmb_terme.Text.Trim() == string.Empty);

            return OK;
        }

        //METHODE DE REMPLISSAGE DU COMBOBOX POUR CHOISIR 
        //UN TERME DE LOCATION
        private void remplirCmbTerme()
        {
            //PARAMETRES DE CONNECTION
            string connect = ConfigurationManager.ConnectionStrings["connexion"].ConnectionString;
            using SqlConnection con = new(connect);

            //CONNECTION...
            con.Open();

            //COMMANDE SQL ET PARAMETRES
            SqlCommand cmdt = con.CreateCommand();
            cmdt.CommandType = CommandType.Text;
            cmdt.CommandText = "SELECT idTerme, " +
                               "CONCAT(nbAnnee,'  ans, ', kmMax, 'km Max, '," +
                                      " surprime, '$ de surprime ')" +
                               "AS 'ToutTermes' " +
                               "FROM dbo.TermesLocation " +
                               "ORDER BY ToutTermes";
            //EXÉCUTION...
            cmdt.ExecuteNonQuery();

            //INSERTION DES DONNÉES DANS UN DATATABLE
            DataTable dt = new();
            SqlDataAdapter da = new(cmdt);
            da.Fill(dt);

            //INSERTION DANS COMBOBOX CHACUNE DES LIGNES
            foreach (DataRow dr in dt.Rows)
            {
                cmb_terme.Items.Add(dr["ToutTermes"]);
            }
        }

        //METHODE POUR MENTIONNER QUEL ID DU
        //TERME DE LOCATION INSERER DANS LA TABLE
        //LOCATION SELON LE CHOIX D'INDEX DU COMBOBOX
        private int IdTerme()
        {
            var id = 0;
            var selectCmb = Convert.ToInt32(cmb_terme.SelectedValue?.ToString());

            if (selectCmb != -1)
            {
                switch (selectCmb)
                {
                    case 0:
                        id = 5;
                        break;
                    case 1:
                        id = 2;
                        break;
                    case 2:
                        id = 3;
                        break;
                    case 3:
                        id = 1;
                        break;
                    case 4:
                        id = 4;
                        break;

                }
            }
            else
                MessageBox.Show("Vous devez choisir un terme de location");

            return id;

        }

        //METHODE POUR INSÉRER LE TEXTE
        //CHOISIR DANS LA LISTE DU COMBOBOX
        private int NmbMoisLocation()
        {
            int choix = -1;
            if (Cmb_moisDeLocation != null)
            {
                choix = int.Parse(Cmb_moisDeLocation.SelectedItem.ToString());

            }
            return choix;
        }

        //DÈS QUE L'UTILISATEUR CHANGE LE FOCUS SUR UN
        //AUTRE CONTROLE ET QU'IL N'A PAS ENTRÉ QUELQUE CHOSE...
        //OU UN MAUVAIS FORMAT
        private void txt_mntPaieMens_Leave(object sender, EventArgs e)
        {
            try
            {
                //LE FORMAT À INSERER AUTOMATIQUEMENT 
                txt_mntPaieMens.Text = string.Format("{0:#,##0.00}", double.Parse(txt_mntPaieMens.Text));
            }
            catch
            {
                //SI CE N'EST PAS FAIT, AFFICHER UN MESSAGE D'AIDE
                var ui = txt_mntPaieMens.Text;
                MessageBox.Show($"{ui} Ce n'est pas un format valide." +
                                            $"\nFormat demandé avec une virgule :750,25" +
                                            $"\nVous devez entrer une entrée valide");
                txt_mntPaieMens.Clear();
            }
        }

        //ÉVENEMENT QUE DÈS QUE L'UTILISATEUR CHANGE LE FOCUS SUR UN
        //AUTRE CONTROLE
        //OU UN MAUVAIS FORMAT
        private void txt_KmDebut_Leave(object sender, EventArgs e)
        {
            try
            {
                //SI BIEN SAISIE : METTRE EN FORMAT AUTOMATIQUEMENT
                txt_KmDebut.Text = string.Format("{0:#,##0.00}", double.Parse(txt_KmDebut.Text));

            }
            catch
            {
                //S'IL N'A PAS ENTRÉ QUELQUE CHOSE...
                //OU UN MAUVAIS FORMAT
                var uis = txt_KmDebut.Text;
                MessageBox.Show($"{uis}Ceci n'est pas un format valide.\nFormat demandé : 150,10");
                txt_KmDebut.Clear();
            }
        }


        //MÉTHODE D'ENREGISTREMENT DANS LA BASE DE DONNÉES
        private void btn_Enregistrer_Click(object sender, EventArgs e)
        {
            //String DE CONNEXION
            string connect = ConfigurationManager.ConnectionStrings["connexion"].ConnectionString;
            using SqlConnection con = new(connect);

            //VÉRIFIER LA SAISIE ET SI C'EST OK...
            bool OK = VerifierSaisie();
            if (OK)
            {
                //REQUETE DE SELECTION
                string verification = $"SELECT niv FROM Location WHERE niv= '{txt_niv.Text}'";
                //REQUETE D'OBJET SQLCOMMAND
                cmd = new SqlCommand(verification, con);

                try
                {
                    //OUVERTURE DE LA CONNEXION
                    con.Open();
                    //OUVERTURE DU LECTEUR
                    SqlDataReader reader = cmd.ExecuteReader();

                    //SI LE VÉHICULE EST DÉJÀ EN LOCATION:
                    if (reader.Read())
                    {
                        MessageBox.Show("Ce NIV de véhicule est déjà en location.", "Attention!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        // Fermeture du lecteur.
                        reader.Close();
                        //COMMANDE D'INSERTION DE DONNÉES
                        string insertionLoc = $"INSERT INTO [dbo].[Location]" +
                                            $"(debutLoc,contratNbrMois,premPaiement,[qte.paie/annee],[mntPaieMens],nbrKmDebut,niv,idTerme)" +
                                            $"VALUES (@Dt_dateDeLocation, @contratNbrMois,@Dt_datePremierPaiement,@nmbPaiParAnnee,@mntPaieMens," +
                                            $"@txt_KmDebut,'{txt_niv.Text}',@idTerme)";

                        // Création de l'objet SqlCommand.
                        // La connexion est toujours ouverte.
                        cmd = new SqlCommand(insertionLoc, con);

                        //VARIABLES CRÉER POUR LES PARAMETRES
                        string dateLocation = Dt_dateDeLocation.Value.ToLongDateString();
                        string dateDePaiement = Dt_datePremierPaiement.Value.ToLongDateString();
                        int methodeId = IdTerme();
                        decimal paieParAnnee = decimal.Parse(Cmb_nbrPaiementParAnne.Text);
                        decimal mntPaieMens = decimal.Parse(txt_mntPaieMens.Text);
                        decimal kmDebut = decimal.Parse(txt_KmDebut.Text);

                        //AJOUT DES PARAMETRES ET DES VALEURS ASSOCIÉES
                        cmd.Parameters.AddWithValue("@Dt_dateDeLocation", dateLocation);
                        cmd.Parameters.AddWithValue("@Dt_datePremierPaiement", dateDePaiement);
                        cmd.Parameters.Add("@idTerme", SqlDbType.Int);
                        cmd.Parameters["@idTerme"].Value = methodeId;
                        cmd.Parameters.Add("@contratNbrMois", SqlDbType.Int);
                        cmd.Parameters["@contratNbrMois"].Value = NmbMoisLocation();
                        cmd.Parameters.Add("@nmbPaiParAnnee", SqlDbType.Decimal);
                        cmd.Parameters["@nmbPaiParAnnee"].Value = paieParAnnee;
                        cmd.Parameters.Add("@mntPaieMens", SqlDbType.Decimal);
                        cmd.Parameters["@mntPaieMens"].Value = mntPaieMens;
                        cmd.Parameters.Add("@txt_KmDebut", SqlDbType.Decimal);
                        cmd.Parameters["@txt_KmDebut"].Value = kmDebut;

                        MessageBox.Show(insertionLoc).ToString();
                        try
                        {
                            // Exécution de la requête.
                            int row = cmd.ExecuteNonQuery();

                            //INITIALIZATION DE LA LISTVIEW AVEC LES NOUVELLES DONNÉES 
                            lstv_Location.Items.Clear();
                            AfficheDonneListView();

                            // Si l'enregistrement a réussi, envoie un message à l'utilisateur
                            if (row != 0)
                            {
                                //...ET SI LA RÉPONSE EST NON : CLOSE
                                if (MessageBox.Show("Enregistrement de la nouvelle location réussi." + Environment.NewLine +
                                        "Désirez-vous enregistrer une autre Location ? ", "Enregistrement réussi",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    this.Close();
                                }
                                //...SINON MESSAGE D'INVTATION
                                else
                                {
                                    MessageBox.Show("Veuillez saisir une nouvelle location");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Ce véhicule n'est pas disponible dans l'inventaire.\n" +
                    "Veuillez saisir une autre voiture");
                        }
                        finally
                        {
                            // Fermeture de la connexion.
                            con.Close();
                        }
                    }
                }
                catch (Exception exe)
                {
                    MessageBox.Show(exe.Message);
                }
            }
            //SI UN CHAMPS EST VIDE 
            else
            {

                MessageBox.Show("Vous devez saisir toutes les informations requises",
                "Attention !", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }

        }

        //CHANGE LA DATE DE FIN SELON DATE DATE DE DÉBUT CHOISIS
        private void Cmb_moisDeLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexChoisis;
            indexChoisis = (int)Cmb_moisDeLocation.SelectedIndex;

            if (indexChoisis != -1)
            {
                switch (indexChoisis)
                {
                    case 0:
                        txtDateFin.Text = Dt_dateDeLocation.Value.AddYears(1).ToShortDateString();
                        break;
                    case 1:
                        txtDateFin.Text = Dt_dateDeLocation.Value.AddYears(2).ToShortDateString();
                        break;
                    case 2:
                        txtDateFin.Text = Dt_dateDeLocation.Value.AddYears(3).ToShortDateString();
                        break;
                    case 3:
                        txtDateFin.Text = Dt_dateDeLocation.Value.AddYears(4).ToShortDateString();
                        break;
                }
            }

        }
        //CHANGE LA DATE DE FIN SELON DATE DATE DE DÉBUT CHOISIS
        private void Dt_dateDeLocation_ValueChanged(object sender, EventArgs e)
        {
            int indexChoisis;
            indexChoisis = Cmb_moisDeLocation.SelectedIndex;
            var text = Cmb_moisDeLocation.Text;
            if (text != null)
            {
                switch (indexChoisis)
                {
                    case 0:
                        txtDateFin.Text = Dt_dateDeLocation.Value.AddYears(1).ToShortDateString();
                        break;
                    case 1:
                        txtDateFin.Text = Dt_dateDeLocation.Value.AddYears(2).ToShortDateString();
                        break;
                    case 2:
                        txtDateFin.Text = Dt_dateDeLocation.Value.AddYears(3).ToShortDateString();
                        break;
                    case 3:
                        txtDateFin.Text = Dt_dateDeLocation.Value.AddYears(4).ToShortDateString();
                        break;
                }
            }
        }



        //RENVOIE AU TAB AJOUT DE LOCATION
        private void Btn_ajoutVehicule_Click(object sender, EventArgs e)
        {
            var tab = tabControl1;
            tab.SelectedIndex = 1;
        }

        //METHODE DE MISE EN FORME QUAND 'UTILISATEUR QUITTE CE CONTROL
        private void txt_niv_Leave(object sender, EventArgs e)
        {
            try
            {
                //SI LE CONTROLE CONTIENT 20 CARACTERES
                if (txt_niv.Text.Length == 20)
                {
                    FormatTxtNiv();
                }
                //SI LE CONTROLE NE CONTIENT PAS 20 CARACTERES
                else
                {
                    MessageBox.Show("Vous devez entrer 20 lettres et/ou chiffres pour le Niv du véhicule\n" +
                        "Écrivez en format sans les tirets : q8tkdp4j28c0h89tidy7", "Information", MessageBoxButtons.OK);
                    txt_niv.Text = "";
                }
            }
            catch
            {
                throw;
            }
        }

        //METHODE DE FORMAT POUR REMPLIR LE TEXTNIV ONGLET AJOUR LOCATION
        private void FormatTxtNiv()
        {
            //SUPPRIMER ESPCAE BLANC
            string? text = txt_niv.Text.Trim();

            //FORMAT DE CHAINE. AJOUT D'UN TIRET DES EMPLACEMENTS PRÉCIS
            string? _1 = new(text.ToCharArray(0, 5));
            string? _2 = new(text.ToCharArray(5, 5));
            string? _3 = new(text.ToCharArray(10, 5));
            string? _4 = new(text.ToCharArray(15, 5));

            //CONSTRUCTION DE LA CHAINE
            txt_niv.Text = (_1 + "-" + _2 + "-" + _3 + "-" + _4).ToUpper();
        }
        #endregion tab2
    }
}




using System.Configuration;
using System.Data.SqlClient;

using LocationDeVoiture.ClassesBD;

namespace LocationDeVoiture
    {
    public partial class Loggins : Form
        {
        private const string MessageBoxTextQuitter = "Voulez-vous vraiment quitter ?";
        private readonly bool ouverture;
        public SqlConnection Connexion { get; }

        public Loggins()
            {
            InitializeComponent();

            Connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["connexion"].ConnectionString);
            nomUtilisateur.Focus();
            ouverture = true;
            }


        //BOUTON ANNULER. SI CLIQUÉ : DEMANDE SI L'UTILISATEUR EST CERTAIN DE BIEN VOULOIR QUITTER
        //SI OUI FERME LA CONNEXION
        private void BtnAnnuler_Click(object sender, EventArgs e)
            {
            DialogResult resultat = MessageBox.Show(MessageBoxTextQuitter, "ATTENTION !", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //SI OUI, FERME LA CONNEXION
            if (resultat == DialogResult.Yes)
                {
                Connexion.Close();
                this.Close();
                }
            }
        //LE BOUTON QUI PERMET DE SE CONNECTER APRES AVOIR BIEN REMPLI TOUT LES CHAMPS
        //ET QUE LA CONNEXION EST DANS LA BASE DE DONNÉES
        private void BtnConnexion_Click(object sender, EventArgs e)
            {
            try
                {
                //SELECTION DE L'UTILISATEUR DANS LA BASE DE DONNÉES
                string authentification = "SELECT * FROM Utilisateurs WHERE Courriel ='"
                                          + nomUtilisateur.Text.Trim() + "' AND MotPasse = '"
                                          + motDePasse.Text + "'";
                SqlCommand commande = new(authentification, Connexion);
                Connexion.Open();
                SqlDataReader lecteur = commande.ExecuteReader();

                // Si lecteur contient un enregistrement.
                if (lecteur.Read())
                    {
                    imgDisconnected.Visible = false;  //IMAGE DISPARAIT QUAND LA CONNECTION A RÉUSSIE
                    imgConnected.Location = imgDisconnected.Location;
                    imgConnected.Visible = true;

                    //CRÉATION DE L'UTILISATEUR
                    UtilisateurActif utilisateur = new()
                        {
                        // Récupération des informations de notre utilisateur.
                        IdUtilisateur = lecteur["idUtilisateur"].ToString()!,
                        Prenom = lecteur["Prenom"].ToString()!,
                        Nom = lecteur["Nom"].ToString()!,
                        Courriel = lecteur["Courriel"].ToString()!,
                        };

                    // Affichage d'un message de bienvenue.
                    MessageBox.Show("Bienvenue " + utilisateur.Prenom + " " + utilisateur.Nom);

                    Thread th = new(OuvirMainForm!); ;
                    this?.Close();
                    th.SetApartmentState(ApartmentState.STA);
                    th.Start();
                    }
                //SI LES CHAMPS NE SONT PAS REMPLIS
                else if (string.IsNullOrEmpty(nomUtilisateur.Text) || string.IsNullOrEmpty(motDePasse.Text))
                    {
                    MessageBox.Show("Vous ne pouvez pas laisser de case vide.\n" +
                                    "Veuillez entrer toutes les informations pour l'authentification");

                    }
                //SI LES INFORMATIONS SAISIES NE CONCORDENT PAS AVEC LA BASE DE DONNÉES
                else
                    {
                    MessageBox.Show("Les informations saisies ne me permet pas de vous authentifier.");
                    nomUtilisateur.Clear();
                    motDePasse.Clear();
                    nomUtilisateur.Focus();
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show(ex.Message);
                }
            finally { Connexion.Close(); }
            }

        //OUVRIR LE FORMULAIRE PRINCIPAL LORSQUE BIEN CONNECTÉ
        private void OuvirMainForm(object obj)
            {
            Application.Run(new MainForm());
            }
        }
    }

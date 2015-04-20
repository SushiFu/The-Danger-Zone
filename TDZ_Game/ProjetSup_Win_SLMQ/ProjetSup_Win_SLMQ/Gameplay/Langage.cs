using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public enum Langue
    {
        Francais,
        English
    }

    public static class Langage
    {
        public static string[,] stringArray = new string[2, 60];
        public static Langue langueactuelle;

        public static string getString(Langue langue, int numero)
        {
            return stringArray[(int)langue, numero];
        }

        public static void LoadLanguage()
        {
            stringArray[0, 0] = "solo";
            stringArray[0, 1] = "campagne";
            stringArray[0, 2] = "mode survie";
            stringArray[0, 3] = "ameliorations";
            stringArray[0, 4] = "Inventaire";
            stringArray[0, 5] = "Boutique";
            stringArray[0, 6] = "Charger Personage";
            stringArray[0, 7] = "Nom :  ";
            stringArray[0, 8] = "Points de competences :  ";
            stringArray[0, 9] = "Points d'experience :  ";
            stringArray[0, 10] = "Niveaux :  ";
            stringArray[0, 11] = "Argent :  ";
            stringArray[0, 12] = "Vies :  ";

            stringArray[0, 13] = "Attaque  : ";

            stringArray[0, 14] = "Defense  : ";
            stringArray[0, 15] = "Precision  : ";
            stringArray[0, 16] = "Vitesse  :  ";

            stringArray[0, 17] = "Points de vie : ";
            stringArray[0, 18] = "Nouveau Personnage";
            stringArray[0, 19] = "multijoueur";
            stringArray[0, 20] = "creer partie";
            stringArray[0, 21] = "rejoindre partie";
            stringArray[0, 22] = "editeur de cartes";
            stringArray[0, 23] = "options";
            stringArray[0, 24] = "son: oui/non ";
            stringArray[0, 25] = "quitter";
            stringArray[0, 26] = "plein ecran: ";
            stringArray[0, 27] = "langue: ";
            stringArray[0, 28] = "haut: ";
            stringArray[0, 29] = "gauche: ";
            stringArray[0, 30] = "droite: ";
            stringArray[0, 31] = "bas: ";
            stringArray[0, 32] = "ramasser: ";
            stringArray[0, 33] = "lacher: ";
            stringArray[0, 34] = "action: ";
            stringArray[0, 35] = "recharger:";
            stringArray[0, 36] = "touches";
            stringArray[0, 37] = "Charger Personnage";
            stringArray[0, 38] = " Munitions : ";
            stringArray[0, 39] = "J";
            stringArray[0, 40] = " Armes";
            stringArray[0, 41] = "Vies Restantes";
            stringArray[0, 42] = "Ennemis tues : ";
            stringArray[0, 43] = "Objet : ";
            stringArray[0, 44] = "ip: ";
            stringArray[0, 45] = "nom du serveur: ";
            stringArray[0, 46] = "selection:";
            stringArray[0, 47] = "selectionner un serveur: ";
            stringArray[0, 48] = "aucun serveur disponible..";
            stringArray[0, 49] = "match a mort";
            stringArray[0, 50] = "capture de drapeau";
            stringArray[0, 51] = "Selectionner votre personnage : ";
            stringArray[0, 52] = "Besoin d'une nouvelle arme ? A court de munitions ? \n Achetez ici !";
            stringArray[0, 53] = "Prix ";
            stringArray[0, 54] = "nom";
            stringArray[0, 55] = "carte";
            stringArray[0, 56] = "joueurs";
            stringArray[0, 57] = "mode";
            stringArray[0, 58] = "Equipe Rouge: ";
            stringArray[0, 59] = "Equipe Bleu: ";


            stringArray[1, 0] = "solo";
            stringArray[1, 1] = "story mode";
            stringArray[1, 2] = "survival mode";
            stringArray[1, 3] = "upgrades";
            stringArray[1, 4] = "Inventory";
            stringArray[1, 5] = "Shop";
            stringArray[1, 6] = "Load Player";
            stringArray[1, 7] = "Name :  ";
            stringArray[1, 8] = "Skill Points :  ";
            stringArray[1, 9] = "Experience :  ";
            stringArray[1, 10] = "Level :  ";
            stringArray[1, 11] = "Money :  ";
            stringArray[1, 12] = "Lives : ";
            stringArray[1, 13] = "Attack :  ";
            stringArray[1, 14] = "Defense :  ";
            stringArray[1, 15] = "Accuracy :  ";
            stringArray[1, 16] = "Speed :  ";
            stringArray[1, 17] = "Life Points :  ";
            stringArray[1, 18] = "New character";
            stringArray[1, 19] = "multiplayer";
            stringArray[1, 20] = "create game";
            stringArray[1, 21] = "join game";
            stringArray[1, 22] = "map editor";
            stringArray[1, 23] = "options";
            stringArray[1, 24] = "sound: yes/no";
            stringArray[1, 25] = "exit";
            stringArray[1, 26] = "fullscreen:  ";
            stringArray[1, 27] = "language:  ";
            stringArray[1, 28] = "up: ";
            stringArray[1, 29] = "left: ";
            stringArray[1, 30] = "right: ";
            stringArray[1, 31] = "down: ";
            stringArray[1, 32] = "pick up: ";
            stringArray[1, 33] = "drop: ";
            stringArray[1, 34] = "action: ";
            stringArray[1, 35] = "reload: ";
            stringArray[1, 36] = "keys";
            stringArray[1, 37] = "Load Character";
            stringArray[1, 38] = "Munitions : ";
            stringArray[1, 39] = "J";
            stringArray[1, 40] = "Weapons";
            stringArray[1, 41] = "Lives left : ";
            stringArray[1, 42] = "Killed : ";
            stringArray[1, 43] = "Item : ";
            stringArray[1, 44] = "ip: ";
            stringArray[1, 45] = "server name: ";
            stringArray[1, 46] = "selection: ";
            stringArray[1, 47] = "select server: ";
            stringArray[1, 48] = "no server available...";
            stringArray[1, 49] = "team deathmatch";
            stringArray[1, 50] = "capture the flag";
            stringArray[1, 51] = "Select your character";
            stringArray[1, 52] = "Need some weapons? Ran out of munitions? \n    Buy here!";
            stringArray[1, 53] = "Price ";
            stringArray[1, 54] = "name";
            stringArray[1, 55] = "map";
            stringArray[1, 56] = "players";
            stringArray[1, 57] = "mode";
            stringArray[1, 58] = "Red Team: ";
            stringArray[1, 59] = "Blue Team: ";
        }
    }
}
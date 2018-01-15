﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditorInternal;
using UnityEngine.UI;


public class GameScript : MonoBehaviour {

    // Tous les gameObject qu'on va Activer/Désactiver 
    #region

    public GameObject WarningPanel;
    public GameObject FinalPanel;

    public GameObject profilepic;
    public GameObject force;
    public GameObject age;
    public GameObject combos;
    public GameObject vitesse;
    public GameObject delete;

    public InputField Input_nom;
    public InputField Input_age;
    public InputField Input_force;
    public InputField Input_vitesse;

    public Button next;
    public Button prev;
    public Button OuiButton;
    public Button bt1;
    public Button bt2;
    public Button bt3;
    public Button bt4;
    public Button bt5;

    public Text Nomtxt;
    public Text Agetxt;
    public Text Forcetxt;
    public Text Vitessetxt;

    public Button ModifButton;
    public Button ChoisirButton;
    public Button NewCharButton;
    #endregion 

    public List<GameObject> desactivate_when_empty;

    private List<InputField> Input_fields;
    public List<Button> CharButtons;
    public List<PlayerClass> players;
    public int current_int;

    private bool under_modification;
    private bool under_deletion;
    private bool under_creation;
    private bool normal_mode;

    void Start ()
    {
        Initialisation();
        current_int = 0;
        setInfo();
    }
   
    void Update()
    {
        if (players.Count != 0)
        { // on met en surbrillance le bouton du joueur affiché
            for (int i = 0; i < players.Count; i++)
            {
                Image theButton = CharButtons[i].gameObject.GetComponent<Image>();
                if (i == current_int)
                {
                    theButton.color = new Color(0.2F, 0.3F, 0.4F, 0.5F);
                }
                else
                {
                    theButton.color = new Color(255, 255, 255);
                }
            }
        }

        // navigation au clavier:

        if (Input.GetButtonDown("Submit"))  
        {
            GreenButton();
        }
        
        if (Input.GetKeyDown("delete"))
        {
            if (normal_mode && players.Count!=0) { DeletingButton(); }
            else { 
                BackToNormal(); }
        }
        
        if (Input.GetKeyDown("right")) { 
            //todo : highlight buttons when key pressed
            if (normal_mode) { Switch(1); }
        }
        if (Input.GetKeyDown("left")) {
            if (normal_mode) { Switch(-1); }
        }
    }
   
    // Affiche le joueur précédent ou suivant de la liste
    public void Switch(int i) 
        //prend en paramètre +1 ou -1
    {
        current_int += i;
        current_int = modulo(current_int, players.Count);
        setInfo();
    }

    // aide pour la méthode Switch (besoin de revenir en début/fin de liste)
    int modulo(int current, int count)
    {
        int to_return = current;
        if (current == 0) { to_return = 0; }

        if (current > 0)
        {
            while ((to_return < 0) || (to_return >= count))
            {
                to_return = to_return - count;
            }
        }
        if (current < 0)
        {
            while ((to_return < 0) || (to_return >= count))
            {
                to_return = to_return + count;
            }
        }
        return to_return;
    }

    // Affichage des infos relatives au joueur à l'index current_int de la liste players
    void setInfo() 
    {
        if (players.Count != 0)
        {
            // S'il y a au moins un joueur à afficher, on active l'interface
            foreach (GameObject g in desactivate_when_empty)
            { g.SetActive(true); }

            // on désactive d'abord toutes les icones des personnages
            foreach (Button b in CharButtons)
            { b.gameObject.SetActive(false); }

            // pour les réactiver un à un ensuite
            for (int i = 0; i < players.Count; i++)
            {
                CharButtons[i].gameObject.SetActive(true);
                CharButtons[i].GetComponentInChildren<Text>().text = players[i].name;
            }

            // Et on set les informations du joueur
            Nomtxt.GetComponent<Text>().text = players[current_int].name;
            Agetxt.GetComponent<Text>().text = players[current_int].age.ToString();
            Forcetxt.GetComponent<Text>().text = players[current_int].strength.ToString();
            Vitessetxt.GetComponent<Text>().text = players[current_int].speed.ToString();

        }

        else // sinon, l'interface est vide (pas d'autre choix que d'ajouter un nouveau perso)
        {
            foreach (GameObject g in desactivate_when_empty)
            { g.SetActive(false); }

            Nomtxt.GetComponent<Text>().text = "";
            Agetxt.GetComponent<Text>().text = "";
            Forcetxt.GetComponent<Text>().text = "";
            Vitessetxt.GetComponent<Text>().text = "";
        }
    }

    // Retire le joueur supprimé de la liste, et si possible, affiche un autre
    public void ApproveDeletion()
    {
        players.RemoveAt(current_int);
        if (players.Count != 0) { Switch(+1);}
        BackToNormal();
    }

    // Effectue les modifications dans la liste players
    void DoTheModification()  
    { 
        if (Input_nom.text != "")
        {
            players[current_int].setName(Input_nom.text);
        }
        if (Input_age.text != "")
        {
            players[current_int].age = Convert.ToInt32(Input_age.text);
        }
        if (Input_force.text != "")
        {
            players[current_int].strength = Convert.ToInt32(Input_force.text);
        }
        if (Input_vitesse.text != "")
        {
            players[current_int].speed = Convert.ToInt32(Input_vitesse.text);
        }
    }

    // On affiche à nouveau ce qu'il y a à afficher dans les UI, et on enlève les Warning Panels
    public void BackToNormal()
    {
        setInfo();

        foreach (InputField inp in Input_fields)
        {   // reset input fields 
            inp.Select();
            inp.text = "";
        }

        CharButtons[current_int].Select();
        next.gameObject.SetActive(true);
        prev.gameObject.SetActive(true);

        WarningPanel.SetActive(false);
        FinalPanel.SetActive(false);

        Input_nom.gameObject.SetActive(false);
        Input_age.gameObject.SetActive(false);
        Input_force.gameObject.SetActive(false);
        Input_vitesse.gameObject.SetActive(false);

        Nomtxt.gameObject.SetActive(true);
        Agetxt.gameObject.SetActive(true);
        Forcetxt.gameObject.SetActive(true);
        Vitessetxt.gameObject.SetActive(true);

        under_modification = false;
        under_deletion = false;
        under_creation = false;
        normal_mode = true;
        
        ChoisirButton.gameObject.GetComponentInChildren<Text>().text = "Choisir";
    }


    void Initialisation()
    {
        //todo : fichier json --> players

        // Liste de joueurs 
        PlayerClass pl1 = new PlayerClass("Player1", 12, 3, 9);
        PlayerClass pl2 = new PlayerClass("Player2", 7, 31, 0);
        PlayerClass pl3 = new PlayerClass("Ariane", 21, 1000, 1000);
        players.Add(pl1);
        players.Add(pl2);
        players.Add(pl3);

        // Liste d'inputs (pour création/modification)
        Input_fields = new List<InputField>();
        Input_fields.Add(Input_nom);
        Input_fields.Add(Input_age);
        Input_fields.Add(Input_force);
        Input_fields.Add(Input_vitesse);

        // Liste d'objets à désactiver s'il n'y a aucun joueur à afficher
        desactivate_when_empty = new List<GameObject>();
        desactivate_when_empty.Add(ModifButton.gameObject);
        desactivate_when_empty.Add(ChoisirButton.gameObject);
        desactivate_when_empty.Add(age);
        desactivate_when_empty.Add(force);
        desactivate_when_empty.Add(vitesse);
        desactivate_when_empty.Add(combos);
        desactivate_when_empty.Add(profilepic);
        desactivate_when_empty.Add(delete);

        // Liste des 5 boutons pour les persos, en bas
        CharButtons = new List<Button>();
        CharButtons.Add(bt1);
        CharButtons.Add(bt2);
        CharButtons.Add(bt3);
        CharButtons.Add(bt4);
        CharButtons.Add(bt5);

        // Paramètres pour savoir où on en est lorsqu'un bouton est cliqué
        under_modification = false;
        under_deletion = false;
        under_creation = false;
        normal_mode = true;
        
    }

    // Fonctions des boutons
    #region
    // bouton NOUVEAU 
    public void NewCharacter()
    {
        // Comme je ne sais pas afficher de liste déroulante, limite du nombre de persos à 5
        if (players.Count < 5)
        {
            under_creation = true;
            normal_mode = false;
            under_deletion = false;
            under_modification = false;

            // Comme la liste n'est pas vide, on active les éléments de l'UI
            foreach (GameObject g in desactivate_when_empty)
            { g.SetActive(true); }


            // On désactive les champs de texte "nom, age, force"... et le bouton modifier
            Nomtxt.gameObject.SetActive(false);
            Agetxt.gameObject.SetActive(false);
            Forcetxt.gameObject.SetActive(false);
            Vitessetxt.gameObject.SetActive(false);
            ModifButton.gameObject.SetActive(false);

            ChoisirButton.gameObject.GetComponentInChildren<Text>().text = "Valider";

            // Les champs d'inputs prennent comme placeholder les noms des infos demandées
            Input_nom.gameObject.SetActive(true);
            Input_nom.placeholder.GetComponent<Text>().text = "Nom";
            Input_age.gameObject.SetActive(true);
            Input_age.placeholder.GetComponent<Text>().text = "Age";
            Input_force.gameObject.SetActive(true);
            Input_force.placeholder.GetComponent<Text>().text = "Force";
            Input_vitesse.gameObject.SetActive(true);
            Input_vitesse.placeholder.GetComponent<Text>().text = "Vitesse";
         }
    }

    // boutons PERSONNAGES
    public void CharButtonSelect(Text id)
    {
        current_int = int.Parse(id.text); // on récupère l'id (de 0 à 5) passé en paramètre pour afficher infos correspondantes
        setInfo();
    }

    // bouton DELETE
    public void DeletingButton()
    { // effectue actions différentes suivant où on en est
        if (under_modification||under_creation)
        { BackToNormal(); }
        else {
            if (under_deletion) { BackToNormal(); }
            else { 
                WarningPanel.SetActive(true);
                under_modification = false;
                under_creation = false;
                normal_mode = false;
                under_deletion = true;
            }
        }
        setInfo();
    }

    // bouton MODIFIER
    public void ButtonModif()
    {
        under_modification = true;
        under_creation = false;
        normal_mode = false;
        under_deletion = false;

        //on désactive la navigation entre les persos, le bouton modifier, et tous les textes "age, nom, force..."
        next.gameObject.SetActive(false);
        prev.gameObject.SetActive(false);
        ModifButton.gameObject.SetActive(false);
        Nomtxt.gameObject.SetActive(false);
        Agetxt.gameObject.SetActive(false);
        Forcetxt.gameObject.SetActive(false);
        Vitessetxt.gameObject.SetActive(false);

        // Le bouton "choix du perso" devient "valider les modifications"
        ChoisirButton.gameObject.GetComponentInChildren<Text>().text = "Valider"; 
        
        // On affiche les champs d'inputs, avec comme placeholder les infos déjà renseignées sur le perso
        Input_nom.gameObject.SetActive(true);
        Input_nom.placeholder.GetComponent<Text>().text = players[current_int].name;
        Input_age.gameObject.SetActive(true);
        Input_age.placeholder.GetComponent<Text>().text = players[current_int].age.ToString();
        Input_force.gameObject.SetActive(true);
        Input_force.placeholder.GetComponent<Text>().text = players[current_int].strength.ToString();
        Input_vitesse.gameObject.SetActive(true);
        Input_vitesse.placeholder.GetComponent<Text>().text = players[current_int].speed.ToString();
    }

    // bouton CHOISIR : suivant où on en est, effectue actions différentes
    public void GreenButton()
    {
        if (under_deletion) { ApproveDeletion(); BackToNormal(); }
        else {
            if (under_modification) { DoTheModification(); BackToNormal(); }
            else {
                if (under_creation)
                {
                    PlayerClass newPlayer = new PlayerClass("", 0, 0, 0);
                    players.Add(newPlayer);
                    current_int = players.IndexOf(newPlayer);
                    DoTheModification();
                    BackToNormal();
                }
                else //normal_mode
                {
                    FinalPanel.SetActive(true);
                    under_modification = true;
                }
            }
        }
    }

    // boutons GAUCHE et DROITE
    public void NextButton() { if (players.Count != 0) { Switch(1); } }
    public void PreviousButton() { if (players.Count != 0) { Switch(-1); } }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class logic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject menupage;
    public GameObject waitingpage;
    public GameObject rolepage;
    int people = 8;
    public TextMeshProUGUI peopleText;
    public TextMeshProUGUI roletext;
    public TextMeshProUGUI roleDescription;
    public RawImage roleimage;
    public List<Tuple<string,string>> list8 =new List<Tuple<string, string>> ();
    public List<Tuple<string, string>> list10 =new List<Tuple<string, string>>();
    public List<Tuple<string, string>> list12 = new List<Tuple<string, string>>();
    public List<Tuple<string, string>> mainlist = new List<Tuple<string, string>>();
    public Texture2D Godfather;
    public Texture2D Professional_Mafia;
    public Texture2D Mafia;
    public Texture2D Detective;
    public Texture2D Doctor;
    public Texture2D Sniper;
    public Texture2D Citizen;
    public Texture2D Mayor;
    public Texture2D Freemason;
    public Texture2D Savior;
    public Texture2D Assassin;
    public GameObject rolePopupPanel;

    RawImage tmp;
    public
    void Start()
    {
        List<Tuple<string, string>> temp = new List<Tuple<string, string>>();
        temp.Add(new Tuple<string, string>("Godfather", "The leader of the mafia who appears innocent when investigated by the detective"));
        temp.Add(new Tuple<string, string>("Professional Mafia", "A mafia member who appears as a citizen when investigated, making them harder to identify"));
        temp.Add(new Tuple<string, string>("Mafia", "A regular mafia member who appears guilty when investigated and collaborates with the Godfather to eliminate citizens"));
        temp.Add(new Tuple<string, string>("Detective", "A citizen who can investigate one player each night to determine if they are mafia or innocent"));
        temp.Add(new Tuple<string, string>("Doctor", "A citizen who can protect one player each night from being killed"));
        temp.Add(new Tuple<string, string>("Sniper", "A citizen who can kill a suspected mafia member at night but must choose carefully to avoid harming allies"));
        temp.Add(new Tuple<string, string>("Citizen", "An ordinary player with no special abilities, whose goal is to identify and eliminate the mafia through discussion and voting"));
        temp.Add(new Tuple<string, string>("Citizen", "An ordinary player with no special abilities, whose goal is to identify and eliminate the mafia through discussion and voting"));
        temp.Add(new Tuple<string, string>("Mayor", "A powerful citizen who can influence the game by having an additional vote or veto power in daily votest"));
        temp.Add(new Tuple<string, string>("Freemason", "A special citizen who knows the identity of another citizen and can exchange information with them at night"));
        temp.Add(new Tuple<string, string>("Savior", "A unique role that can prevent a player from being executed or killed once during the game"));
        temp.Add(new Tuple<string, string>("Assassin", "An aggressive role aligned with the mafia, with the power to kill independently, acting as an offensive force for the mafia"));
        for (int i = 0; i < 8; i++)
        {
            list8.Add(temp[i]);
        }
        for (int i = 0; i < 9; i++)
        {
            list10.Add(temp[i]);
        }
        list10.Add(temp[6]);
        for (int i = 0;i < 12; i++)
        {
            list12.Add(temp[i]);
        }
        people = 8;
        peopleText.text = "8 people";
        rolePopupPanel.SetActive(false);
        menupage.SetActive(true);
        waitingpage.SetActive(false);
        rolepage.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Increase()
    {
        if (people == 8)
        {
            people = 10;
        }
        else if (people == 10)
        {
            people = 12;
        }
        else if (people == 12)
        {
            people = 8;
        }
        peopleText.text = people.ToString() + " people";
    }
    public void Decrease()
    {
        if (people == 8)
        {
            people = 12;
        }
        else if (people == 10)
        {
            people = 8;
        }
        else if (people == 12)
        {
            people = 10;
        }
        peopleText.text = people.ToString() + " people";
    }
    public void start()
    {
        menupage.SetActive(false);
        waitingpage.SetActive(true);
        rolepage.SetActive(false);
        mainlist.Clear();
        if (people == 8) 
        {
            for (int i = 0; i < people; i++) 
            {
                mainlist.Add(list8[i]);
            } 
        }
        if (people == 10)
        {
            for (int i = 0; i < people; i++)
            {
                mainlist.Add(list10[i]);
            }
        }
        if (people == 12)
        {
            for (int i = 0; i < people; i++)
            {
                mainlist.Add(list12[i]);
            }
        }

    }
    public void OK()
    {
        rolePopupPanel.SetActive(false);
        menupage.SetActive(true);
        waitingpage.SetActive(false);
        rolepage.SetActive(false);
    }
    public void seen()
    {
        menupage.SetActive(false);
        waitingpage.SetActive(true);
        rolepage.SetActive(false);
    }
    public void restart()
    {
        people = 8;
        peopleText.text = "8 people";
        menupage.SetActive(true);
        waitingpage.SetActive(false);
        rolepage.SetActive(false);

    }
}

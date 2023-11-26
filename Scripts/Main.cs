using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
public partial class Main : Node2D
{
    public List<Panel> panelDefinitions = new List<Panel>();
    private PackedScene textPanel;
    private PackedScene choicePanel;
    private PackedScene endPanel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        string json = File.ReadAllText("Textfiles/testscript.txt");
        panelDefinitions = JsonSerializer.Deserialize<List<Panel>>(json);

        textPanel = (PackedScene)ResourceLoader.Load("res://Scenes/TextPanel.tscn");
        choicePanel = (PackedScene)ResourceLoader.Load("res://Scenes/ChoicePanel.tscn");
        endPanel = (PackedScene)ResourceLoader.Load("res://Scenes/EndPanel.tscn");

        var panels = new List<CanvasGroup>();
        foreach (var panel in panelDefinitions)
        {
            CanvasGroup newPanel;
            if(panel.type.Equals("text"))
            {
                newPanel = (CanvasGroup)textPanel.Instantiate();
                newPanel.GetChild<RichTextLabel>(0).Text = "[center]" + panel.mainText;

            }

            else if(panel.type.Equals("choice"))
            {
                ChoicePanel panelDefenition = (ChoicePanel)panel; 

                newPanel = (CanvasGroup)choicePanel.Instantiate();
                newPanel.GetChild<RichTextLabel>(0).Text = "[center]" + panel.mainText;
                
            }

            else
            {
                EndPanel panelDefinition = (EndPanel)panel;

                newPanel = (CanvasGroup)endPanel.Instantiate();
            }

            newPanel.GetChild<Sprite2D>(-3).Texture = ResourceLoader.Load<Texture2D>("res://Textures/Characters/" + panel.character + "/" + panel.expression + ".png");
            if(panel.characterNumber == 2)
            {
                newPanel.GetChild<Sprite2D>(-2).Texture = ResourceLoader.Load<Texture2D>("res://Textures/Characters/" + panel.secondCharacter + "/" + panel.secondExpression + ".png");
                newPanel.GetChild<Sprite2D>(-3).MoveLocalX(-100);
            }         
            newPanel.GetChild<Sprite2D>(-1).Texture = ResourceLoader.Load<Texture2D>("res://Textures/Backgrounds/" + panel.background + ".jpg");
            newPanel.Hide();
            panels.Add(newPanel);
            AddChild(newPanel);
        }


        GD.Print(panelDefinitions.ToArray()[0].character);
        PrintTree();
        panels[0].Show();
    }

    public void NextPanel()
    {
        
    }
}

public class Panel
{
    public int panelNumber { get; set; }
    public string mainText { get; set; }
    public int nextPanel { get; set; }
    public int characterNumber { get; set; }
    public string character { get; set; }
    public string secondCharacter { get; set; }
    public string expression { get; set; }
    public string secondExpression { get; set; }
    public string background { get; set; }
    public string type { get; set; }
}
public class TextPanel : Panel
{

    public TextPanel(string text,
                int nextPanel,
                int characterNumber,
                string character,
                string secondCharacter,
                string expression,
                string secondExpression,
                string background)
    {
        mainText = text;
        this.nextPanel = nextPanel;
        this.characterNumber = characterNumber;
        this.character = character;
        this.secondCharacter = secondCharacter;
        this.expression = expression;
        this.secondExpression = secondExpression;
        this.background = background;
        type = "text";
    }
}

public class ChoicePanel : Panel
{
    public string choiceText1;
    public int choice1NextPanel;
    public string choice1AffectedPerson;
    public int choice1Points;
    public string choiceText2;
    public int choice2NextPanel;
    public string choice2AffectedPerson;
    public int choice2Points;
    public string choiceText3;
    public int choice3NextPanel;
    public string choice3AffectedPerson;
    public int choice3Points;

    public ChoicePanel(string mainText,
                 int characterNumber,
                 string character,
                 string secondCharacter,
                 string expression,
                 string secondExpression,
                 string background,
                 string choiceText1,
                 int choice1NextPanel,
                 string choice1AffectedPerson,
                 int choice1Points,
                 string choiceText2,
                 int choice2NextPanel,
                 string choice2AffectedPerson,
                 int choice2Points,
                 string choiceText3,
                 int choice3NextPanel,
                 string choice3AffectedPerson,
                 int choice3Points)
    {
        this.mainText = mainText;
        this.characterNumber = characterNumber;
        this.character = character;
        this.secondCharacter = secondCharacter;
        this.expression = expression;
        this.secondExpression = secondExpression;
        this.background = background;
        this.choiceText1 = choiceText1;
        this.choice1NextPanel = choice1NextPanel;
        this.choice1AffectedPerson = choice1AffectedPerson;
        this.choice1Points = choice1Points;
        this.choiceText2 = choiceText2;
        this.choice2NextPanel = choice2NextPanel;
        this.choice2AffectedPerson = choice2AffectedPerson;
        this.choice2Points = choice2Points;
        this.choiceText3 = choiceText3;
        this.choice3NextPanel = choice3NextPanel;
        this.choice3AffectedPerson = choice3AffectedPerson;
        this.choice3Points = choice3Points;
        type = "choice";
    }
}

public class EndPanel : Panel
{
    public string nextChapter;

    public EndPanel(string text,
              string nextFile,
              int characterNumber,
              string character,
              string secondCharacter,
              string expression,
              string secondExpression,
              string background)
    {
        mainText = text;
        this.characterNumber = characterNumber;
        this.character = character;
        this.secondCharacter = secondCharacter;
        this.expression = expression;
        this.secondExpression = secondExpression;
        this.background = background;
        nextChapter = nextFile;
        type = "end";
    }
}
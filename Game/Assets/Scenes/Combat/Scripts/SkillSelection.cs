using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSelection : MonoBehaviour
{
    public Image selectBorder;
    public GameObject AbilityDescriptionPanel;
    private TextMeshProUGUI AbilityDescriptionText;
    public Button[] skillButtons = new Button[8]; // Array for 8 skill buttons

    private RectTransform[] buttonTransforms = new RectTransform[8];
    private Player player;
    private Skill[] skills; 

    private RectTransform imageRect;

    void Start()
    {
        SelectBordInit();
        DescriptionPanelInit();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        skills = player.skills;

        for (int i = 0; i < skills.Length; i++)
        {
            int skillIndex = i; // Capture index for lambda
            buttonTransforms[i] = skillButtons[i].GetComponent<RectTransform>();
            skillButtons[i].onClick.AddListener(() => SelectSkill(skillIndex));
        }

        UpdateSkillButtons();
    }

    void DescriptionPanelInit()
    {
        //AbilityDescriptionPanel.SetActive(false);
        AbilityDescriptionText = AbilityDescriptionPanel.GetComponentInChildren<TextMeshProUGUI>(false);
    }

    void SelectBordInit()
    {
        imageRect = selectBorder.GetComponent<RectTransform>();
        NotShowSelect();
    }
    public void UpdateSkillButtons()
    {
        for (int i = 0; i < skillButtons.Length; i++)
        {
            skillButtons[i].interactable = false;
            skillButtons[i].GetComponent<Image>().sprite = null;
            skillButtons[i].transform.GetChild(0).gameObject.SetActive(false);

            if(player.skills.Length > i){

                if(player.skills[i] != null){

                    skillButtons[i].interactable = true;
                    skillButtons[i].GetComponent<Image>().sprite = player.skills[i].Icon;

                    if(player.skills[i].cooldownCount <= 0)
                        continue;

                    skillButtons[i].transform.GetChild(0).gameObject.SetActive(true);
                    skillButtons[i].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text = $"{player.skills[i].cooldownCount}";

                }

            }
        }
    }

    public void SelectSkill(int skillIndex)
    {
        if(player.SelectSkill(skillIndex)){
            ShowSelect();
            UpdateAbilityText(skillIndex);
            imageRect.anchoredPosition = buttonTransforms[skillIndex].anchoredPosition;;
        }
    }

    void UpdateAbilityText(int skillIndex)
    {
        //AbilityDescriptionPanel.SetActive(true);
        AbilityDescriptionText.text = skills[skillIndex].Name + " Lvl. " + skills[skillIndex].SkillLevel + "\n" + skills[skillIndex].DescriptionPanel;
    }

    void ShowSelect()
    {
        selectBorder.enabled = true;
    }

    void NotShowSelect()
    {
        selectBorder.enabled = false;
    }
}
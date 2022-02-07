using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private List<GameObject> characterlist;
    private GameObject CharacterSelectorParent;
    private int listIndex = 0;

    private void Awake()
    {
        characterlist.Clear();
    }

    private void Start()
    {
        foreach ( Transform child in transform)
        {
            characterlist.Add(child.gameObject);
        }
        characterlist[0].SetActive(true);
        SelectOneCharacter();
    }

    public void NextCharacterButton()
    {
        if (listIndex != characterlist.Count -1)
        {
            characterlist[listIndex].SetActive(false);
            listIndex++;
            characterlist[listIndex].SetActive(true);
        }
        else if (listIndex == characterlist.Count -1)
        {
            characterlist[listIndex].SetActive(false);
            listIndex = 0;
            characterlist[listIndex].SetActive(true);
        }

        SelectOneCharacter();
    }

    public void PreviousCharacterButton()
    {
        if (listIndex != 0)
        {
            characterlist[listIndex].SetActive(false);
            listIndex--;
            characterlist[listIndex].SetActive(true);
        }
        else if (listIndex == 0)
        {
            characterlist[listIndex].SetActive(false);
            listIndex = characterlist.Count - 1;
            characterlist[listIndex].SetActive(true);
        }

        SelectOneCharacter();
    }
    private void SelectOneCharacter()
    {
        GameManager.instance.SelectCharacterToPplay(characterlist[listIndex].name);
    }

    public void AddNewCharacterToList(GameObject character)
    {
        characterlist.Add (character);
    }
}

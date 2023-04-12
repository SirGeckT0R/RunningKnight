using UnityEditor.U2D.Animation;
using UnityEngine.UIElements;

public class CharacterListEntryController
{
    Label NameLabel;

    //This function retrieves a reference to the 
    //character name label inside the UI element.

    public void SetVisualElement(VisualElement visualElement)
    {
        NameLabel = visualElement.Q<Label>("character-name");
    }

    //This function receives the character whose name this list 
    //element displays. Since the elements listed 
    //in a `ListView` are pooled and reused, it's necessary to 
    //have a `Set` function to change which character's data to display.

    public void SetCharacterData(CollectibleTypes bonus)
    {
        NameLabel.text = bonus.ToString();
    }
}
// Jeremy Fischer
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHieght = 0;
        
        foreach(Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            // Adddes a event call bck when you click the button
            // Instead of needing to manually adding it in unity it does it automatically
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));

            responseBoxHieght += responseButtonTemplate.sizeDelta.y;
        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHieght);
        responseBox.gameObject.SetActive(true);

    }
    private void OnPickedResponse(Response response)
    {

    }
}

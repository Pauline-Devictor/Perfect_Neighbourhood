using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HighlightIndividual : MonoBehaviour
{
    public void OnClickText(GameObject target)
    {
        TextMeshProUGUI scrollContent = target.GetComponent<TextMeshProUGUI>();
        Camera arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        var lineIndex = TMP_TextUtilities.FindIntersectingLine(scrollContent, Input.mousePosition, arCamera);
        if (lineIndex != -1) {
            string fullName = scrollContent.text.Split('\n')[lineIndex];
            scrollContent.text = scrollContent.text.Replace("<color=red><b>", "");
            scrollContent.text = scrollContent.text.Replace("</b></color>", "");
            scrollContent.text = scrollContent.text.Replace(fullName, "<color=red><b>" + fullName + "</b></color>");
            GameObject map = GameObject.Find("MapMenu");
            GameObject found = null;
            foreach (Transform child in map.transform) {
                if(child.gameObject.name != "Image"){
                    if(child.gameObject.name == fullName){
                        child.gameObject.GetComponent<RawImage>().color = Color.red;
                        child.gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        found = child.gameObject;
                    } else if(scrollContent.text.Contains(child.gameObject.name)){
                        child.gameObject.GetComponent<RawImage>().color = Color.cyan;
                        child.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                }
            }
            if(fullName == "pan"){
                found = map.transform.Find("Image").transform.Find("pan").gameObject;
                found.GetComponent<RawImage>().color = Color.red;
                
            } else {
                map.transform.Find("Image").transform.Find("pan").gameObject.GetComponent<RawImage>().color = Color.cyan;
            }
            if(found != null) found.transform.SetAsLastSibling();
        }
    }
}

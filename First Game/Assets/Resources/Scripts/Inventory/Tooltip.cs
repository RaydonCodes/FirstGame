using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Tooltip : MonoBehaviour
{

    TextMeshProUGUI tooltipText;

    // Start is called before the first frame update
    void Start()
    {
        tooltipText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void GenerateTooltip(Item item)
    {
        string statText = "";
        if (item.stats.Count > 0)
        {
            foreach (var stat in item.stats)
            {
                statText += stat.Key.ToString() + ": " + stat.Value.ToString() + "\n";
            }
        }
        tooltipText.text = string.Format("<b>{0}</b>\n{1}\n\n<b>{2}</b>", item.title, item.description, statText);
        gameObject.SetActive(true);
    }

}

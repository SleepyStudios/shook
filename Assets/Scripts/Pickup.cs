using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour {
    public int cost;
    public GameObject prefab;
    public float bounce = 3f, speed = 2f, mouseOverScale = 1.5f;
    public TextMeshProUGUI text;
    public bool bought;
    public bool isRepair;
    public int repairExtraCost = 20;

    Vector2 initialPos, target;
    Vector2 targetScale = Vector2.one;
    bool mouseOver;
    float initialFontSize;

    void Start() {
        initialPos = transform.position;
        target = initialPos + new Vector2(0, bounce);

        initialFontSize = text.fontSize;

        SetCostText();
    }

    void Update() {
        transform.position = Vector2.Lerp(transform.position, target, Time.deltaTime * speed);
        if (Vector2.Distance(transform.position, target) <= 0.1f) {
            if (target.y > initialPos.y) {
                target = initialPos;
            } else {
                if (!mouseOver) target = initialPos + new Vector2(0, bounce);
            }
        }

        transform.localScale = Vector2.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    void OnMouseUp() {
        GameObject gun = GameObject.FindGameObjectWithTag("Gun");

        if (bought) {
            if (gun.transform.childCount > 0) Destroy(gun.transform.GetChild(0).gameObject);
            Instantiate(prefab, gun.transform);
            text.text = "Equipped";

            GetComponent<AudioSource>().Play();

            return;
        }

        if (Score.instance.score >= cost) {
            if (isRepair) {
                Rope r = FindObjectOfType<Rope>();
                if (r.CanRepair()) {
                    Score.instance.AddPoints(-cost);

                    text.text = "Repaired";
                    cost += repairExtraCost;
                    r.OnRepair();
                    GetComponent<AudioSource>().Play();
                }
            } else {
                Score.instance.AddPoints(-cost);

                if (gun.transform.childCount > 0) Destroy(gun.transform.GetChild(0).gameObject);

                Instantiate(prefab, gun.transform);
                bought = true;
                text.text = "Equipped";
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnMouseEnter() {
        if (bought) {
            SetEquipText();
            mouseOver = true;
            targetScale = new Vector2(mouseOverScale, mouseOverScale);
            return;
        }

        if (Score.instance.score >= cost) {
            SetBuyText();
            mouseOver = true;
            targetScale = new Vector2(mouseOverScale, mouseOverScale);
        }
    }

    private void OnMouseExit() {
        mouseOver = false;
        targetScale = Vector2.one;
        SetCostText();
    }

    void SetCostText() {
        if (!bought) {
            text.text = cost > 0 ? $"${cost}" : "Free";
        }
        if (bought) text.text = "Owned";
        text.fontSize = initialFontSize;
    }

    void SetBuyText() {
        text.text = isRepair ? "<color=\"black\">LMB</color> Repair" : "<color=\"black\">LMB</color> Buy";
        text.fontSize = 0.25f;
    }

    void SetEquipText() {
        text.text = "<color=\"black\">LMB</color> Equip";
        text.fontSize = 0.25f;
    }
}

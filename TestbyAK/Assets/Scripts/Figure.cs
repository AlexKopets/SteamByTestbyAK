using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour {
    public FigureData data;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown() {
        if (GameManager.Instance.IsGameActive)
            GameManager.Instance.OnFigureClicked(this);
    }
}

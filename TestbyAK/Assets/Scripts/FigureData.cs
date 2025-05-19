using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shape { Circle, Square, Triangle }
public enum AnimalType { dog, owl, panda }

[System.Serializable]
public class FigureData {
    public Shape shape;
    public Color borderColor;
    public AnimalType animal;

    public bool IsSame(FigureData other) {
        return shape == other.shape &&
               borderColor == other.borderColor &&
               animal == other.animal;
    }
}

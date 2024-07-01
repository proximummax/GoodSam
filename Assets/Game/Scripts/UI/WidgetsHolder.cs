using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetsHolder : MonoBehaviour
{
    [SerializeField] private AmmoWidget _ammoWidget;
    public AmmoWidget AmmoWidget {  get { return _ammoWidget; } }

}
